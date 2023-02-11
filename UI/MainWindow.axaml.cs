using Avalonia.Controls;
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

        this.WhenActivated(d => d(ViewModel.ShowInputDialog.RegisterHandler(DoShowInputDialogAsync)));
        this.Closing += (_, _) => ViewModel.OnClose();
    }

    private async Task DoShowInputDialogAsync(InteractionContext<InputDialogWindowViewModel, object?> interaction)
    {
        var dialog = new InputDialogWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<object?>(this);
        interaction.SetOutput(result);
    }
}