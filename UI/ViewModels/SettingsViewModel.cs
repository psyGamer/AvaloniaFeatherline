using ReactiveUI;

namespace Featherline.UI.ViewModels;

public partial class SettingsViewModel : ReactiveObject
{
    public string InfoFile
    {
        get => settings.InfoFile ?? "";
        set => this.RaiseAndSetIfChanged(ref settings.InfoFile, value);
    }

    // Main input boxes
    private string Checkpoints
    {
        get => settings.Checkpoints == null ? "" : string.Join(System.Environment.NewLine, settings.Checkpoints);
        set => this.RaiseAndSetIfChanged(ref settings.Checkpoints, value.Split(System.Environment.NewLine));
    }
    public string Favorite
    {
        get => settings.Favorite ?? "";
        set => this.RaiseAndSetIfChanged(ref settings.Favorite, value);
    }
    private string ManualHitboxes
    {
        get => settings.ManualHitboxes == null ? "" : string.Join(System.Environment.NewLine, settings.ManualHitboxes);
        set => this.RaiseAndSetIfChanged(ref settings.ManualHitboxes, value.Split(System.Environment.NewLine));
    }

    // General algorithm settings
    public int Generations
    {
        get => settings.Generations; 
        set => this.RaiseAndSetIfChanged(ref settings.Generations, value);
    }
    public int Framecount
    {
        get => settings.Framecount; 
        set => this.RaiseAndSetIfChanged(ref settings.Framecount, value);
    }
    public int GensPerTiming
    {
        get => settings.GensPerTiming; 
        set => this.RaiseAndSetIfChanged(ref settings.GensPerTiming, value);
    }
    public int ShuffleCount
    {
        get => settings.ShuffleCount; 
        set => this.RaiseAndSetIfChanged(ref settings.ShuffleCount, value);
    }
    public bool TimingTestFavDirectly
    {
        get => settings.TimingTestFavDirectly; 
        set => this.RaiseAndSetIfChanged(ref settings.TimingTestFavDirectly, value);
    }

    // Genetic Algorithm
    public int Population
    {
        get => settings.Population; 
        set => this.RaiseAndSetIfChanged(ref settings.Population, value);
    }
    public int SurvivorCount
    {
        get => settings.SurvivorCount; 
        set => this.RaiseAndSetIfChanged(ref settings.SurvivorCount, value);
    }
    public float MutationMagnitude
    {
        get => settings.MutationMagnitude; 
        set => this.RaiseAndSetIfChanged(ref settings.MutationMagnitude, value);
    }
    public int MaxMutChangeCount
    {
        get => settings.MaxMutChangeCount; 
        set => this.RaiseAndSetIfChanged(ref settings.MaxMutChangeCount, value);
    }

    // Computation
    public bool IgnoreHazards
    {
        get => settings.IgnoreHazards; 
        set => this.RaiseAndSetIfChanged(ref settings.IgnoreHazards, value);
    }
    public bool IgnoreCollision
    {
        get => settings.IgnoreCollision; 
        set => this.RaiseAndSetIfChanged(ref settings.IgnoreCollision, value);
    }

    // Algorithm Mode
    public bool FrameBasedOnly
    {
        get => settings.FrameBasedOnly; 
        set => this.RaiseAndSetIfChanged(ref settings.FrameBasedOnly, value);
    }
    public bool AvoidWalls
    {
        get => settings.AvoidWalls; 
        set => this.RaiseAndSetIfChanged(ref settings.AvoidWalls, value);
    }

    public int MaxThreadCount
    {
        get => settings.MaxThreadCount; 
        set => this.RaiseAndSetIfChanged(ref settings.MaxThreadCount, value);
    }

    // Other
    public string? CustomSpinnerNames
    {
        get => settings.CustomSpinnerNames; 
        set => this.RaiseAndSetIfChanged(ref settings.CustomSpinnerNames, value);
    }
    public string? OldInfoTemplate
    {
        get => settings.OldInfoTemplate; 
        set => this.RaiseAndSetIfChanged(ref settings.OldInfoTemplate, value);
    }
    public bool KnowsHelpTxt
    {
        get => settings.KnowsHelpTxt; 
        set => this.RaiseAndSetIfChanged(ref settings.KnowsHelpTxt, value);
    }

    // Debug
    public bool LogResults
    {
        get => settings.LogResults; 
        set => this.RaiseAndSetIfChanged(ref settings.LogResults, value);
    }

    private readonly Settings settings;

    public SettingsViewModel(Settings settings)
    {
        this.settings = settings;
    }
}