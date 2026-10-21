using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MacWpfTheme.Helpers;

/// <summary>
/// 绑定 Window.Mac 模板中的标题栏控件：红绿灯按钮、双击标题栏最大化、
/// 主题切换按钮。请在窗口 Loaded 事件中调用 <see cref="Bind"/>。
/// </summary>
public static class MacWindowHelper
{
    public static void Bind(Window window)
    {
        if (window.Template.FindName("BtnClose", window) is Button close)
            close.Click += (_, _) => window.Close();

        if (window.Template.FindName("BtnMin", window) is Button min)
            min.Click += (_, _) => window.WindowState = WindowState.Minimized;

        if (window.Template.FindName("BtnMax", window) is Button max)
            max.Click += (_, _) => window.WindowState =
                window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;

        if (window.Template.FindName("BtnThemeToggle", window) is Button theme)
            theme.Click += (_, _) => ThemeHelper.Toggle();

        // 双击标题栏区域（y <= 38）最大化 / 还原
        window.MouseDoubleClick += (_, e) =>
        {
            if (e.ChangedButton == MouseButton.Left
                && e.GetPosition(window).Y <= 38)
            {
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        };
    }
}
