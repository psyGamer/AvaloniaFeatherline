using System.Reflection;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace Featherline.UI.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{

    private Settings _settings;
    public SettingsViewModel Settings { get; }

    private bool _algorithmRunning = false;
    public bool AlgorithmRunning
    {
        get => _algorithmRunning; 
        private set => this.RaiseAndSetIfChanged(ref _algorithmRunning, value);
    }

    private bool _algorithmClosing = false;
    public bool AlgorithmClosing
    {
        get => _algorithmClosing; 
        private set => this.RaiseAndSetIfChanged(ref _algorithmClosing, value);
    }

    public Interaction<InputDialogWindowViewModel, object?> ShowInputDialog { get; }
    // public Interaction<InputDialogWindowViewModel, int> ShowIntInputDialog;
    // public Interaction<InputDialogWindowViewModel, float> ShowFloatInputDialog;
    public ReactiveCommand<string, Unit> OpenInputDialogCommand { get; }

    public MainWindowViewModel(Settings settings)
    {
        this._settings = settings;
        this.Settings = new SettingsViewModel(settings);

        this.ShowInputDialog = new Interaction<InputDialogWindowViewModel, object?>();
        // this.ShowIntInputDialog = new Interaction<InputDialogWindowViewModel, int>();
        // this.ShowFloatInputDialog = new Interaction<InputDialogWindowViewModel, float>();
        this.OpenInputDialogCommand = ReactiveCommand.CreateFromTask(async (string settingNameMinMax) =>
        {
            string settingName = settingNameMinMax.Split(" ")[0];
            int minValue = int.Parse(settingNameMinMax.Split(" ")[1]);
            int maxValue = int.Parse(settingNameMinMax.Split(" ")[2]);

            // This is probably jank, but I don't really care...
            if (settingName == nameof(Settings.SurvivorCount))
            {
                maxValue = Settings.Population - 1;
            }

            var property = Settings.GetType().GetProperty(settingName);
            if (property == null) return;

            var currentValue = property.GetValue(Settings);
            if (currentValue == null) return;

            var input = new InputDialogWindowViewModel(currentValue, minValue, maxValue);
            var newValue = await ShowInputDialog.Handle(input);

            if (newValue == null) return; // It was canceled

            // This is janky too...
            if (settingName == nameof(Settings.Population))
            {
                Settings.SurvivorCount = Math.Min(Settings.SurvivorCount, (int)newValue - 1);
            }

            property.SetValue(Settings, newValue);
        });
    }

    public void ToggleAlgorithm()
    {
        if (!AlgorithmRunning)
        {
            AlgorithmRunning = true;
            new Thread(() =>
            {
                bool runEnd = GAManager.RunAlgorithm(_settings, false);
                if (runEnd)
                    GAManager.EndAlgorithm();
                GAManager.ClearAlgorithmData();

                AlgorithmRunning = false;
                AlgorithmClosing = false;
            }).Start();
        }
        else
        {
            AlgorithmRunning = false;
            AlgorithmClosing = true;
            GAManager.abortAlgorithm = true;
        }
    }

    public void ToggleBoolSetting(string propertyName)
    {
        PropertyInfo? property = Settings.GetType().GetProperty(propertyName);
        property?.SetValue(Settings, !(bool)(property?.GetValue(Settings) ?? false));
    }
}