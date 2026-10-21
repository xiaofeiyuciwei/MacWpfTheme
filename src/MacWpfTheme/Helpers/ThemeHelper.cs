using System.Windows;

namespace MacWpfTheme.Helpers;

/// <summary>
/// 浅色 / 深色主题切换。在 MacTheme.xaml 的 MergedDictionaries 中
/// 交换 Colors.Light / Colors.Dark 两份色板字典，所有 DynamicResource 自动刷新。
/// </summary>
public static class ThemeHelper
{
    private const string PackLight =
        "pack://application:,,,/MacWpfTheme;component/Themes/Colors.Light.xaml";
    private const string PackDark =
        "pack://application:,,,/MacWpfTheme;component/Themes/Colors.Dark.xaml";

    private static bool _isDark;

    /// <summary>当前是否为深色主题。</summary>
    public static bool IsDark => _isDark;

    /// <summary>切换为浅色主题。</summary>
    public static void SwitchLight()
    {
        Swap(PackDark, PackLight);
        _isDark = false;
    }

    /// <summary>切换为深色主题。</summary>
    public static void SwitchDark()
    {
        Swap(PackLight, PackDark);
        _isDark = true;
    }

    /// <summary>按当前状态反切换。</summary>
    public static void Toggle()
    {
        if (_isDark) SwitchLight();
        else SwitchDark();
    }

    private static void Swap(string fromSource, string toSource)
    {
        var macTheme = FindMacTheme();
        if (macTheme is null) return;

        var remove = macTheme.MergedDictionaries
            .FirstOrDefault(d => d.Source?.OriginalString.Contains("Colors.") == true
                                 && d.Source.OriginalString.Contains(fromSource, StringComparison.OrdinalIgnoreCase));

        if (remove is not null)
            macTheme.MergedDictionaries.Remove(remove);

        macTheme.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(toSource, UriKind.Absolute) });
    }

    private static ResourceDictionary? FindMacTheme()
    {
        // 优先在 App 级资源找 MacTheme.xaml
        var app = Application.Current;
        if (app is null) return null;

        foreach (var dict in app.Resources.MergedDictionaries)
        {
            if (dict.Source?.OriginalString.Contains("MacTheme.xaml") == true)
                return dict;
            // 若 MacTheme 本身又被包在别的字典里，向下找一层
            foreach (var inner in dict.MergedDictionaries)
            {
                if (inner.Source?.OriginalString.Contains("MacTheme.xaml") == true)
                    return inner;
            }
        }
        return null;
    }
}
