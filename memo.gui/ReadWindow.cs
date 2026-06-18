
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using memo.app;
namespace memo.gui;

public class ReadWindow : UserControl, IShortCut
{
    public event Action? RequestFindWindow;
    public event Action? RequestWriteWindow;
    Memo _memo;


    // コンストラクタ
    public ReadWindow(Memo memo)
    {
        this._memo = memo;

        // 生成
        TextBlock textBlock = Ui.CreateTextBlock();

        // Debug用仮
        textBlock.Text = "hello";

        // テキストブロックの登録
        Content = textBlock;
    }

    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod)
    {
        switch (e.Key)
        {
            case Key.F:
                Console.WriteLine("Key F");
                RequestFindWindow?.Invoke();
                return true;
            case Key.W:
                Console.WriteLine("Key W");
                RequestWriteWindow?.Invoke();
                return true;

        }

        return false;
    }
}
