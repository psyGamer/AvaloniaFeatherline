using Avalonia.ReactiveUI;
using ReactiveUI;
using Featherline.UI.ViewModels;

namespace Featherline.UI.Views;

public partial class InputDialogWindow : ReactiveWindow<InputDialogWindowViewModel>
{
    public InputDialogWindow()
    {
        InitializeComponent();

        this.WhenActivated(d => 
        {
            d(ViewModel!.ConfirmCommand.Subscribe(this.Close));
            d(ViewModel!.CancelCommand.Subscribe(this.Close));
        });
    }
}