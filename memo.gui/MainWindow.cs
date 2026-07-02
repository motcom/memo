
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using memo.app;

namespace memo.gui;

public class MainWindow : Window
{
    // App保持
    Memo _memo;
    // コンポネント保持
    FindWindow _findWnd;
    ReadWindow _readWnd;
    WriteWindow _writeWnd;


    // Windowデフォルトプロパティ
    Size _windowSize = new Size(640, 640);

    // コンストラクタ
    public MainWindow()
    {
        // メモAppの生成
        _memo = new();
        // コンポーネント生成
        _findWnd = new FindWindow(_memo);
        _readWnd = new ReadWindow(_memo);
        _writeWnd = new WriteWindow(_memo);

        // ウィンドウプロパティ
        Width = _windowSize.Width;
        Height = _windowSize.Height;

        Topmost = true;

        _findWnd.RequestReadWindow += memoEnt =>
        {
            _readWnd.SetMemoEntity(memoEnt);
            SetWindowMode(WindowMode.Read);
        };

        _findWnd.RequestWriteWindow += () => SetWindowMode(WindowMode.Write);
        _readWnd.RequestFindWindow += () => SetWindowMode(WindowMode.Find);
        _readWnd.RequestWriteWindow += () => SetWindowMode(WindowMode.Write);
        _writeWnd.RequestFindWindow += () => SetWindowMode(WindowMode.Find);


        // 初期のウィンドウ
        Content = _findWnd;

        // ハンドラー追加
        AddHandler(KeyDownEvent, OnKeyDown, Avalonia.Interactivity.RoutingStrategies.Tunnel);

    }
    void OnKeyDown(object? obj, KeyEventArgs e)
    {
        if (Content is IShortCut shortCut)
        {
            if (shortCut.OnShortCut(e, e.KeyModifiers))
            {
                e.Handled = true;
            }
        }
    }

    public void SetWindowMode(WindowMode windowMode)
    {
        switch (windowMode)
        {
            case WindowMode.Read:
                Content = _readWnd;
                break;
            case WindowMode.Find:
                Content = _findWnd;
                break;
            case WindowMode.Write:
                Content = _writeWnd;
                break;
        }
    }

}
