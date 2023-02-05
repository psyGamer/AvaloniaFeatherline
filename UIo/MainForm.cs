using Modern.Forms;

namespace Featherline;
public class MainForm : Form
{
    public const string version = "0.3.3";

    public static Settings settings = new Settings();

    // not actually unused
    //NumMenuItem population, genSurvivors, mutMagnitude, maxMutations, threadCount;

    //public Icon? appIcon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);

    public MainForm()
    {
        Text = "Featherline " + version;

        // Toolbar menu
        var menu = new Menu();

        // var help  = menu.Items.Add("Help");
        // var setup = menu.Items.Add("Setup");
        // var debug = menu.Items.Add("Debug");
        // var extra = menu.Items.Add("Extra");

        // setup.Items.Add("Auto Set Info Template");
        // setup.Items.Add("Copy Info Template");
        // setup.Items.Add("Copy Info Loggin Snippet");

        // debug.Items.Add("Log flight of initial inputs");

        // var geneticAlgorithm = extra.Items.Add("Genetic Algorithm");
        // var conputation = extra.Items.Add("Computation");
        // var algorithmMode = extra.Items.Add("Algorithm Mode");
        
        var sub = menu.Items.Add("Menu");

        sub.Items.Add("A");
        sub.Items.Add("B");
        sub.Items.Add("C");

        foreach (var item in sub.Items)
            item.Click += (o, e) => Console.WriteLine($"hi {item.Text}");

        Controls.Add(menu);
        
        // InitializeComponent();

        // Text = "Featherline " + version;
        // Icon = appIcon;

        // population = new NumMenuItem(populationToolStripMenuItem, 2m, 999999m, 0) {
        //     onValueUpdate = vl => {
        //         genSurvivors.Value = Math.Min(genSurvivors.Value, vl - 1);
        //         return vl;
        //     }
        // };
        // genSurvivors = new NumMenuItem(generationSurvivorsToolStripMenuItem, 1m, 999999m, 0, () => (1m, population.Value - 1));
        // mutMagnitude = new NumMenuItem(mutationMagnitudeToolStripMenuItem, 0m, 180m, 2);
        // maxMutations = new NumMenuItem(maxMutationCountToolStripMenuItem, 1m, 999999m, 0);
        // threadCount =  new NumMenuItem(simulationThreadCountToolStripMenuItem, -1m, 100m, 0) { onValueUpdate = vl => vl <= 0m ? -1m : vl };

        // LoadSettings();

        // FormClosed += (s, e) => {
        //     SaveSettings();
        //     configFile.Dispose();
        // };

        // if (!settings.KnowsHelpTxt) {
        //     new Thread(() => {
        //         bool toggle = false;
        //         var yellow = FromArgb(240, 255, 50);
        //         var normal = FromArgb(245, 245, 245);
        //         try {
        //             while (true) {
        //                 if (settings.KnowsHelpTxt) {
        //                     Invoke((Action)(() => helpToolStripMenuItem.BackColor = normal));
        //                     return;
        //                 }
        //                 Invoke((Action)(() => helpToolStripMenuItem.BackColor = toggle ? normal : yellow));
        //                 toggle = !toggle;
        //                 Thread.Sleep(500);
        //             }
        //         }
        //         catch { }
        //     }).Start();
        // }

        // void ResetPress(object? sender, KeyEventArgs e)
        // {
        //     if (ModifierKeys == Keys.Control && e.KeyCode == Keys.U) {
        //         txt_checkpoints.Text = "";
        //         txt_initSolution.Text = "";
        //         txt_customHitboxes.Text = "";
        //     }
        // }

        // txt_checkpoints.KeyDown += ResetPress;
        // txt_initSolution.KeyDown += ResetPress;
        // txt_customHitboxes.KeyDown += ResetPress;
    }
}