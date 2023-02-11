namespace Featherline;

public static class Constants
{
    public const string SETTINGS_FILE = "settings.xml";

    public const string CUSTOM_INFO_TEMPLATE =
        "PosRemainder: {Player.PositionRemainder} " +
        "Lerp: {Player.starFlySpeedLerp} " +

        "{CrystalStaticSpinner.Position}{DustStaticSpinner.Position}{FrostHelper.CustomSpinner@FrostTempleHelper.Position}{VivHelper.Entities.CustomSpinner@VivHelper.Position}{Celeste.Mod.XaphanHelper.Entities.CustomSpinner@XaphanHelper.Position} " +
        
        "LightningUL: {Lightning.TopLeft} " +
        "LightningDR: {Lightning.BottomRight} " +

        "SpikeUL: {Spikes.TopLeft} " +
        "SpikeDR: {Spikes.BottomRight} " +
        "SpikeDir: {Spikes.Direction} " +

        "Wind: {Level.Wind} " +
        "WTPos: {WindTrigger.Position} " +
        "WTPattern: {WindTrigger.Pattern} " +
        "WTWidth: {WindTrigger.Width} " +
        "WTHeight: {WindTrigger.Height} " +

        "StarJumpUL: {StarJumpBlock.TopLeft} " +
        "StarJumpDR: {StarJumpBlock.BottomRight} " +
        "StarJumpSinks: {StarJumpBlock.sinks} " +

        "JThruUL: {JumpthruPlatform.TopLeft} " +
        "JThruDR: {JumpthruPlatform.BottomRight} " +
        "SideJTUL: {SidewaysJumpThru.TopLeft} " +
        "SideJTDR: {SidewaysJumpThru.BottomRight} " +
        "SideJTIsRight: {SidewaysJumpThru.AllowLeftToRight} " +
        "SideJTPushes: {SidewaysJumpThru.pushPlayer} " +
        "UpsDJTUL: {UpsideDownJumpThru.TopLeft} " +
        "UpsDJTDR: {UpsideDownJumpThru.BottomRight} " +
        "UpsDJTPushes: {UpsideDownJumpThru.pushPlayer} " +

        "Bounds: {Level.Bounds} " +
        "Solids: {Level.Session.LevelData.Solids}";

    public const string HELP_TEXT = @"
To get started with Featherline, follow these steps:

1:  Click on Setup -> Auto Set Info Template when you have celeste open to automatically
    apply the custom info template for extracting game information.
    If this doesn't work, click on Setup -> Info Template to copy the template to your clipboard then
    right click on the info panel of celeste studio and click 'Set Custom Info Template From Clipboard'.
    The automatic way only works if you had debug mode enabled when launching celeste.

2:  Click on Setup -> Copy Info Logging Snippet to copy the info file creation script to your clipboard,
    then paste it into your TAS. Then make sure that the last frame before the simulated inputs is in between the commands.
    Example:

      13,R,U,X
      26
    StartExportGameInfo infodump.txt
       1,R,U
    FinishExportGameInfo

    Then run the TAS over those commands to create/update infodump.txt in your Celeste folder.
    This same method works even if the start frame is in an already going feather.
    Enabling custom info in celesteTAS is not needed. It works regardless.
    The 1,R,U input is for a featherboost. If you don't know what that is, you can ask in #tas_general on the celeste discord.

3:  Click on the Select Information Source File button and select the infodump.txt file in your celeste folder.

4:  Define a checkpoint at every turn or branching point of the path you want to TAS.
    Checkpoints are further explained later in this file.

5:  Click the Run Algorithm button.


--- Checkpoints ---

- To define a checkpoint, hold your info HUD hotkey and drag while holding right click to draw a rectangle hitbox.
  Doing that will copy the selected area to your clipboard, where you can paste it to a line on the checkpoints text box.

- Checkpoint collision is based on your hurtbox. To define a touch switch or feather as a checkpoint, select
  an area that perfectly overlaps with its hitbox. Remember to use the pink, bigger hitbox for touch switches.

- The genetic algorithm primarily flies directly towards the next checkpoint.
  If the next checkpoint is behind a wall of spinners, it will simply fly towards that
  wall of spinners and try to get as close to the checkpoint as it can that way.
  That means you should define a checkpoint at every major turn so it knows where to go.
  If the algorithm messes up at any of the points where progress is reversed, it has to be able to
  fix itself by simply attempting to fly toward the next checkpoint the entire time.

- Making checkpoints really small is not recommended. Making them big does not make the result worse
  and it only cares about whether you touched the checkpoint or not.
  When the algorithm at some moment has not reached a checkpoint, it tries to get to it by aiming for its center
  (the final checkpoint is an exception to this). You can use this to guide the algorithm better by
  making the checkpoints bigger.


--- Custom Hitboxes ---

- Defined the same way as checkpoints but in the text box on the right.
- A defined hitbox is a killbox by default, based on the green hurtbox.
- To define a collider (based on the red collision box) instead of a killbox, place 'c' after the definition.
  Example: '218, -104, 234, -72 c'
- Fully static tile entities will automatically be added as colliders behind the scenes,
  but kevins, falling blocks and others that have the potential to move in some way,
  you will have to define manually.


--- Algorithm Facts ---

- Sometimes the final results of the algorithm will die by an extremely small amount, like 0.0002 pixels.
  When this happens, the solution is to change one of the angles before that point by 0.001 manually
  or by a little bit more if needed.
- Each checkpoint collected adds 10000 to fitness. You can use that knowledge to track how the algorithm is doing.


--- Supported Gameplay Mechanics ---

- anything with a static spinner hitbox, spikes and lightning
- Wind and wind triggers
- Dodging or bouncing off walls. Tile entities explained in Custom Hitboxes section.
- Jumpthroughs
- Correct physics with room bounds


If you have questions that aren't explained anywhere in this guide, feel free to ping TheRoboMan on the celeste discord.
";
}