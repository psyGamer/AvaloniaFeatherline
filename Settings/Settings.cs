namespace Featherline;

public class Settings
{
    public string InfoFile = "";

    // Main input boxes
    public string[] Checkpoints = {};
    public string Favorite = "";
    public string[] ManualHitboxes = {};

    // General algorithm settings
    public int Generations = 2000;
    public int Framecount = 120;
    public int GensPerTiming = 150;
    public int ShuffleCount = 6;
    public bool TimingTestFavDirectly = false;

    // Genetic Algorithm
    public int Population = 50;
    public int SurvivorCount = 20;
    public float MutationMagnitude = 8;
    public int MaxMutChangeCount = 5;

    // Computation
    public bool IgnoreHazards = false;
    public bool IgnoreCollision = false;

    // Algorithm Mode
    public bool FrameBasedOnly = false;
    public bool AvoidWalls = false;

    public int MaxThreadCount = Environment.ProcessorCount;

    // Other
    public string? CustomSpinnerNames;
    public string? OldInfoTemplate;
    public bool KnowsHelpTxt;

    // Debug
    public bool LogResults = true;

    public Settings Copy() => (Settings)MemberwiseClone();
}
