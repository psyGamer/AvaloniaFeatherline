using Avalonia;
using Avalonia.ReactiveUI;
using Featherline.UI;

namespace Featherline
{
    public static class Program
    {
        //public static Form1? mainForm;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
            
            // Settings s = new Settings();
            // s.InfoFile = "/media/Storage/SteamLibrary/steamapps/common/Celeste/infodump.txt";
            // s.Checkpoints = new string[] {
            // 	"28516, -8014, 28523, -8007",
            // 	"28428, -7982, 28439, -7971",
            // 	"28356, -7894, 28367, -7883",
            // 	"28472, -7736, 28535, -7729",
            // };
            // s.Generations = 1000;
            // s.Framecount = 120;
            // s.GensPerTiming = 150;
            // s.ShuffleCount = 6;
            // s.Favorite = "";
            // GAManager.RunAlgorithm(s, false);

            // MainForm form = new MainForm();
            // Application.Run(form);
            // Application.SetHighDpiMode(HighDpiMode.SystemAware);
            // Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //mainForm = new Form1();
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UseReactiveUI()
                .UsePlatformDetect()
                .LogToTrace();
    }
}
