using System.Reflection;
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

    public MainWindowViewModel(Settings settings)
    {
        this._settings = settings;
        this.Settings = new SettingsViewModel(settings);
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