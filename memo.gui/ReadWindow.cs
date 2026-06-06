
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
namespace memo.gui;

public class ReadWindow : UserControl, IShortCut
{
    public event Action? RequestFindWindow;
    public event Action? RequestWriteWindow;

    // コンストラクタ
    public ReadWindow()
    {
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
