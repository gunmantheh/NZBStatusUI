using System.ComponentModel;

namespace NZBStatusUI.Enums
{
    public enum Name
    {
        [Description("")]
        None,
        [Description("resume")]
        Resume,
        [Description("pause")]
        Pause,
        [Description("delete")]
        Delete,
        [Description("speedlimit")]
        SpeedLimit
    }
}