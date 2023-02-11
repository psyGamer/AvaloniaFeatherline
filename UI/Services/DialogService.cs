using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Featherline.UI.Services;

public static class DialogService {
    private static MainWindow mainWindow => (MainWindow)((Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.MainWindow);

    public static async Task<string?> ShowOpenFileDialogAsync(string title, string name, params string[] extensions) {
        var dialog = new OpenFileDialog();
        dialog.Title = title;
        dialog.Filters!.Add(new FileDialogFilter { Name = name, Extensions = extensions.ToList() });
        dialog.AllowMultiple = false;

        var paths = await dialog.ShowAsync(mainWindow).ConfigureAwait(true);
        if (paths == null) return null;
        if (paths.Length == 0) return "";
        return paths[0];
    }

    public static async Task<string[]?> ShowOpenMultipleFilesDialogAsync(string title, string name, params string[] extensions) {
        var dialog = new OpenFileDialog();
        dialog.Title = title;
        dialog.Filters!.Add(new FileDialogFilter { Name = name, Extensions = extensions.ToList() });
        dialog.AllowMultiple = true;

        return await dialog.ShowAsync(mainWindow).ConfigureAwait(true);
    }

    public static async Task<string?> ShowSaveFileDialogAsync(string title, string name, params string[] extensions) {
        var dialog = new SaveFileDialog();
        dialog.Title = title;
        dialog.Filters!.Add(new FileDialogFilter { Name = name, Extensions = extensions.ToList() });

        return await dialog.ShowAsync(mainWindow!).ConfigureAwait(true);
    }
}
