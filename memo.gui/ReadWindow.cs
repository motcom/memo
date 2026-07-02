
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
    TextBlock _textBlock;


    // コンストラクタ
    public ReadWindow(Memo memo)
    {
        this._memo = memo;

        // 生成
        _textBlock = Ui.CreateTextBlock();

        // Debug用仮
        _textBlock.Text = "";

        // テキストブロックの登録
        Content = _textBlock;
    }

    public void SetMemoEntity(MemoEntity memoEnt)
    {
        _textBlock.Text = memoEnt.Doc;
    }

    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod)
    {
        switch (e.Key)
        {
            case Key.F when mod.HasFlag(KeyModifiers.Control):
                Console.WriteLine("Key F");
                RequestFindWindow?.Invoke();
                return true;
            case Key.W when mod.HasFlag(KeyModifiers.Control):
                Console.WriteLine("Key W");
                RequestWriteWindow?.Invoke();
                return true;

        }

        return false;
    }
}
