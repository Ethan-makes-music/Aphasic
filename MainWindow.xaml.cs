using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace micMute;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// DEV STARTED: 7/6/26
/// </summary>
public partial class MainWindow : Window
{
    bool muted = false;
    private const int HOTKEY_ID = 1;

    [DllImport("user32.dll")]
    static extern bool RegisterHotKey(IntPtr hWnd, int id, uint modifiers, uint key);

    [DllImport("user32.dll")]
    static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    public MainWindow()
    {
        InitializeComponent();

        Topmost = true;
        AllowsTransparency = true;
        WindowStyle = WindowStyle.None;
        Background = Brushes.Transparent;

        this.Focusable = true;
        this.Focus();

        Canvas.SetLeft(NotMutedImage, 0);
        Canvas.SetTop(NotMutedImage, 0);
        Canvas.SetLeft(MutedImage, 0);
        Canvas.SetTop(MutedImage, 0);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        var helper = new WindowInteropHelper(this);
        var source = HwndSource.FromHwnd(helper.Handle);

        source.AddHook(HwndHook);

        RegisterHotKey(
            helper.Handle,
            HOTKEY_ID,
            0,
            0xDD // ]
        );
    }

    protected override void OnClosed(EventArgs e)
    {
        var helper = new WindowInteropHelper(this);
        UnregisterHotKey(helper.Handle, HOTKEY_ID);

        base.OnClosed(e);
    }

    private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        const int WM_HOTKEY = 0x0312;

        if (msg == WM_HOTKEY && wParam.ToInt32() == HOTKEY_ID)
        {
            ToggleMute();
            handled = true;
        }

        return IntPtr.Zero;
    }

    private void ToggleMute()
    {
        muted = !muted;

        if (muted)
        {
            MutedImage.Visibility = Visibility.Visible;
            NotMutedImage.Visibility = Visibility.Hidden;
        }
        else
        {
            MutedImage.Visibility = Visibility.Hidden;
            NotMutedImage.Visibility = Visibility.Visible;
        }
    }

    private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}