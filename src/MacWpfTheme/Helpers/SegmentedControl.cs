using System.Windows;
using System.Windows.Controls;

namespace MacWpfTheme.Helpers;

/// <summary>
/// macOS 分段选择器。继承 ListBox，横向排列胶囊项。
/// 配合 Themes/SegmentedControl.xaml 使用。
/// </summary>
public class SegmentedControl : ListBox
{
    public SegmentedControl()
    {
        SelectionMode = SelectionMode.Single;
        HorizontalContentAlignment = HorizontalAlignment.Stretch;
    }

    static SegmentedControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SegmentedControl),
            new FrameworkPropertyMetadata(typeof(SegmentedControl)));
    }
}
