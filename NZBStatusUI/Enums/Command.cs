using System.ComponentModel;

namespace NZBStatusUI.Enums
{
    public enum Command
    {
        [Description("resume")]
        Resume,
        [Description("pause")]
        Pause,
        [Description("queue")]
        Queue
    }
}