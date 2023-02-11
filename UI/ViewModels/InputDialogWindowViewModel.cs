using System.Reflection;
using System.Reactive;
using ReactiveUI;

namespace Featherline.UI.ViewModels;

public partial class InputDialogWindowViewModel : ReactiveObject
{
    public enum InputType
    {
        Int,
        Float,
    }

    public InputType Type { get; }

    public int MinValue { get; }
    public int MaxValue { get; }

    public int IntValue { get; private set; }
    public float FloatValue { get; private set; }

    private bool IsInt { get => Type == InputType.Int; }
    private bool IsFloat { get => Type == InputType.Float; }

    public ReactiveCommand<Unit, object?> ConfirmCommand { get; }
    public ReactiveCommand<Unit, object?> CancelCommand { get; }

    public InputDialogWindowViewModel(object currentValue, int minValue, int maxValue)
    {
        if (currentValue is int i)
        {
            this.IntValue = i;
            this.Type = InputType.Int;
        }
        else if (currentValue is float f)
        {
            this.FloatValue = f;
            this.Type = InputType.Float;
        }

        this.MinValue = minValue;
        this.MaxValue = maxValue;

        this.ConfirmCommand = ReactiveCommand.Create(() =>
        {
            switch (Type)
            {
            case InputType.Int:
                return (object)IntValue;
            case InputType.Float:
                return (object)FloatValue;
            }
            return null;
        });
        this.CancelCommand = ReactiveCommand.Create(() => (object?)null);
    }
}