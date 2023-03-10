using System.Reactive;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Featherline.UI.Views;
using Featherline.UI.ViewModels;

namespace Featherline.UI;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(d => 
        {
            d(ViewModel!.ShowInputDialog.RegisterHandler(DoShowInputDialogAsync));
            d(ViewModel!.ShowHelpDialog.RegisterHandler(DoShowHelpDialogAsync));
        });
        this.Closing += (_, _) => ViewModel!.OnClose();
    }

    private async Task DoShowInputDialogAsync(InteractionContext<InputDialogWindowViewModel, object?> interaction)
    {
        var dialog = new InputDialogWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<object?>(this);
        interaction.SetOutput(result);
    }

    private async Task DoShowHelpDialogAsync(InteractionContext<Unit, Unit> interaction)
    {
        var dialog = new HelpWindow();
        await dialog.ShowDialog<object?>(this);
        interaction.SetOutput(Unit.Default);
    }
}