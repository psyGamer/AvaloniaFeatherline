using System.Net;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Reactive;
using System.Reactive.Linq;
using System.Xml.Serialization;
using ReactiveUI;
using Avalonia;
using Featherline.UI.Services;

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

    private readonly ObservableAsPropertyHelper<bool> _usesCustomInfoTemplate;
    public bool UsesCustomInfoTemplate { get => _usesCustomInfoTemplate.Value; }

    public Interaction<InputDialogWindowViewModel, object?> ShowInputDialog { get; }
    public ReactiveCommand<string, Unit> OpenInputDialogCommand { get; }

    private FileStream settingsFile = new FileStream(Constants.SETTINGS_FILE, FileMode.OpenOrCreate);

    public MainWindowViewModel()
    {
        this._settings = new Settings();
        LoadSettings();

        this.Settings = new SettingsViewModel(_settings);

        this.ShowInputDialog = new Interaction<InputDialogWindowViewModel, object?>();
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

        this.WhenAnyValue(x => x.Settings.OldInfoTemplate)
            .Select(_ => Settings.OldInfoTemplate != null)
            .ToProperty(this, x => x.UsesCustomInfoTemplate, out _usesCustomInfoTemplate);
    }

    public void OnClose()
    {
        SaveSettings();
        settingsFile.Dispose();
    }

    public async void CopyInfoTemplate()
    {
        await Application.Current!.Clipboard!.SetTextAsync(Constants.CUSTOM_INFO_TEMPLATE);
    }

    public async void CopyInfoLoggingSnippet()
    {
        await Application.Current!.Clipboard!.SetTextAsync(Constants.INFO_LOGGING_SNIPPET);
    }

    public async void AutoSetInfoTemplate()
    {
        try {
            using var client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(500) };
            var oldTemplateRequest = await client.GetAsync("http://localhost:32270/tas/custominfo");
            var oldTemplate = Regex.Match(await oldTemplateRequest.Content.ReadAsStringAsync(), @"<pre>([\S\s]*)</pre>").Groups[1].Value;

            await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, $"http://localhost:32270/tas/custominfo?template={Constants.CUSTOM_INFO_TEMPLATE}"));

            if (oldTemplate != Constants.CUSTOM_INFO_TEMPLATE)
                Settings.OldInfoTemplate = oldTemplate;
        }
        catch {
            // TODO: Messageboxes
            // var msgBoxRes = MessageBox.Show(this, "Failed to set the custom info template.\nCopy it to clipboard instead?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            // if (msgBoxRes == DialogResult.Yes)
            //     infoTemplateToolStripMenuItem_Click(null, null);
            Console.Error.WriteLine("Failed to set the custom info template");
        }
    }

    public async void RestoreOriginalInfoTemplate()
    {
        try {
            using var client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(500)};
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, $"http://localhost:32270/tas/custominfo?template={Settings.OldInfoTemplate}"));

            if (response.StatusCode == HttpStatusCode.OK)
                Settings.OldInfoTemplate = null;
        }
        catch {
            // TODO: Messageboxes
            // var msgBoxRes = MessageBox.Show(this, "Failed to return to old template.\nCopy it to clipboard instead?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            // if (msgBoxRes == DialogResult.Yes)
            //     Clipboard.SetText(settings.OldInfoTemplate);
            Console.Error.WriteLine("Failed to return to old template");
        }
    }

    public async void SelectInfodumpFile()
    {
        var path = await DialogService.ShowOpenFileDialogAsync("Select infodump.txt", "Info Dump (infodump.txt)", "txt");
        if (path != null)
            Settings.InfoFile = path;
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

    private void LoadSettings()
    {
        try {
            this._settings = (new XmlSerializer(typeof(Settings)).Deserialize(settingsFile) as Settings) ?? _settings;
        }
        catch
        { }
    }

    private void SaveSettings()
    {
        settingsFile.SetLength(0);
        new XmlSerializer(typeof(Settings)).Serialize(settingsFile, _settings);
    }
}