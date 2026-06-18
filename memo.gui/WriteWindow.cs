using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using memo.app;

namespace memo.gui;

public class WriteWindow : UserControl, IShortCut
{
    public event Action? RequestFindWindow;

    Label _labelIndex;
    TextBox _tbIndex;
    Label _labelDesc;
    TextBox _tbDescription;
    TextBox _tbTexts;

    Memo _memo;


    // コンストラクタ
    public WriteWindow(Memo memo)
    {
        this._memo = memo;
        // Create
        _labelIndex = Ui.CreateLabel("Index:");
        _tbIndex = Ui.CreateTextBox();
        _labelDesc = Ui.CreateLabel("Description:");
        _tbDescription = Ui.CreateTextBox();
        _tbTexts = Ui.CreateTextBoxWithScrollBar();

        // Layout
        Grid mainGrid = new Grid
        {
            ColumnDefinitions = ColumnDefinitions.Parse("auto,*"),
            RowDefinitions = RowDefinitions.Parse("auto,auto,*")
        };

        Grid.SetColumn(_labelIndex, 0);
        Grid.SetRow(_labelIndex, 0);

        Grid.SetColumn(_tbIndex, 1);
        Grid.SetRow(_tbIndex, 0);

        Grid.SetColumn(_labelDesc, 0);
        Grid.SetRow(_labelDesc, 1);

        Grid.SetColumn(_tbDescription, 1);
        Grid.SetRow(_tbDescription, 1);

        Grid.SetColumn(_tbTexts, 0);
        Grid.SetRow(_tbTexts, 2);
        Grid.SetColumnSpan(_tbTexts, 2);

        mainGrid.Children.Add(_labelIndex);
        mainGrid.Children.Add(_tbIndex);
        mainGrid.Children.Add(_labelDesc);
        mainGrid.Children.Add(_tbDescription);
        mainGrid.Children.Add(_tbTexts);

        Content = mainGrid;
    }

    // インターフェース実装
    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod)
    {
        switch (e.Key)
        {
            // ファインドウィンドウに遷移
            case Key.F:
                Console.WriteLine("Key F");
                RequestFindWindow?.Invoke();
                return true;

        }


        return false;
    }
}
