namespace Featherline;

public static class Constants
{
    public const string SETTINGS_FILE = "settings.xml";

    public const string INFO_LOGGING_SNIPPET = "StartExportGameInfo infodump.txt\n   1\nFinishExportGameInfo";

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
}