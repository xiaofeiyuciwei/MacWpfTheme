# MacWpfTheme

WPF 复刻 **macOS Human Interface Guidelines (HIG)** 风格的 UI 主题库。

面向**视觉检测 / 工控上位机 / 桌面工具**等场景：不引入实时毛玻璃，不含任何 `BlurEffect`，在保证 macOS 质感的同时守住高帧率相机画面的性能红线。

## 特性

- 🎨 **macOS 原生色系**：低饱和中性底（浅 `#F5F5F7` / 深 `#1D1D1F`）、Apple Blue 主色（`#007AFF`）、完整状态色（成功 / 警告 / 危险）
- 🪟 **无边框窗口**（`WindowChrome`）：大圆角、mac 红绿灯按钮、自定义标题栏、双击标题栏最大化、最大化自动取消圆角
- 🧱 **分级圆角体系**：窗口 / 卡片 / 按钮 / 胶囊各不同，非统一圆角
- ⚪ **弱阴影**：极低透明度，无硬阴影
- 🌗 **浅色 / 深色双主题**：`ThemeHelper` 运行时一键切换，全部 `DynamicResource` 自动刷新
- 🚫 **无 BlurEffect / 无 BitmapEffect**：相机采集、图像预览高帧率场景不掉帧
- 🧩 **mac 分段选择器** `SegmentedControl`：用于模式 / 视图快速切换
- 🎚️ **覆盖常用控件模板**：Button / TextBox / CheckBox / RadioButton / Slider / ComboBox / Tooltip
- ✅ MIT 许可，`net8.0-windows`，MVVM 友好

## 目录结构

```
MacWpfTheme/
├── src/
│   ├── MacWpfTheme/                 # 可复用类库
│   │   ├── Themes/
│   │   │   ├── MacTheme.xaml        # 主题入口（合并全部字典）
│   │   │   ├── Colors.Light.xaml    # 浅色色板
│   │   │   ├── Colors.Dark.xaml     # 深色色板
│   │   │   ├── Typography.xaml      # 字体体系
│   │   │   ├── Core.xaml            # 间距 / 圆角 / 阴影 / 卡片 / 标签
│   │   │   ├── Controls.xaml        # 常用控件模板
│   │   │   ├── SegmentedControl.xaml# 分段选择器
│   │   │   └── Window.xaml          # 无边框窗口模板
│   │   └── Helpers/
│   │       ├── ThemeHelper.cs       # 浅 / 深主题切换
│   │       ├── MacWindowHelper.cs   # 标题栏按钮绑定
│   │       └── SegmentedControl.cs  # 分段选择器控件
│   └── MacWpfTheme.Demo/            # 可运行示例（PNP-VMS 视觉软件界面）
├── docs/
└── LICENSE
```

## 快速开始

### 1. 引用主题

在 `App.xaml` 中合并主题入口：

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/MacWpfTheme;component/Themes/MacTheme.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### 2. 应用控件样式

```xml
<Button Style="{StaticResource Button.Primary}" Content="开始采集"/>
<TextBox Style="{StaticResource TextBox.Mac}" Text="5000"/>
<Slider Style="{StaticResource Slider.Mac}" Minimum="0" Maximum="20" Value="8"/>
```

### 3. 无边框窗口

```xml
<Window Style="{StaticResource Window.Mac}" Title="我的应用" ...>
    <!-- 内容 -->
</Window>
```

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => MacWindowHelper.Bind(this); // 绑定红绿灯 / 主题切换 / 双击最大化
    }
}
```

### 4. 主题切换

```csharp
ThemeHelper.SwitchDark();  // 切换深色
ThemeHelper.SwitchLight(); // 切换浅色
ThemeHelper.Toggle();      // 反切换
ThemeHelper.IsDark         // 当前是否深色
```

### 5. 分段选择器

```xml
<mac:SegmentedControl Style="{StaticResource SegmentedControl.Mac}" SelectedIndex="0">
    <ListBoxItem Content="采集"/>
    <ListBoxItem Content="标定"/>
    <ListBoxItem Content="检测"/>
</mac:SegmentedControl>
```

## 资源 Key 速查

| 分类 | Key |
|------|-----|
| 背景 | `Brush.Bg.Primary` / `.Secondary` / `.Tertiary` |
| 文字 | `Brush.Text.Primary` / `.Secondary` / `.Tertiary` |
| 主色 | `Brush.Accent` / `.Light` / `.Hover` |
| 状态 | `Brush.Success` / `.Warning` / `.Danger` |
| 边框 | `Brush.Border` / `.Focus` |
| 间距 | `Space.4` / `8` / `12` / `16` / `20` / `24` |
| 圆角 | `Radius.Sm` / `Md` / `Lg` / `Window` / `Pill` |
| 阴影 | `Shadow.Soft` / `Shadow.Card` |
| 卡片 | `Card.Mac` |
| 标签 | `Label.Body` / `Label.Caption` / `Label.SectionTitle` |
| 按钮 | `Button.Primary` / `Button.Outline` / `Button.Ghost` |
| 控件 | `TextBox.Mac` / `CheckBox.Mac` / `RadioButton.Mac` / `Slider.Mac` / `ComboBox.Mac` / `Tooltip.Mac` |
| 窗口 | `Window.Mac` |

## 性能说明（面向视觉 / 工控场景）

- 全部控件模板 **不含** `BlurEffect`、`BitmapEffect`，相机预览高帧率不掉帧；
- 阴影均为低透明度静态阴影，不在热路径上叠加实时渲染；
- 建议相机画布区域不套 `Shadow.Card`，仅用纯色背景。

## 字体

Windows 无 SF Pro。默认优先 **Inter**（免费商用，可自行到 [rsms.me/inter](https://rsms.me/inter) 下载并安装），回退 `Segoe UI Variable` / `Microsoft YaHei`。未安装 Inter 时自动回退，无需额外处理。

## 构建

```bash
dotnet build -c Release
# 运行示例
dotnet run --project src/MacWpfTheme.Demo
```

需要 .NET 8 SDK 或更高。

## 许可

[MIT](./LICENSE)

本仓库仅复刻 macOS 的视觉风格（设计语言），不含任何 Apple 的商标、图标或专有素材。
