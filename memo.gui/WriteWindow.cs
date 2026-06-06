using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;

namespace memo.gui;
public class WriteWindow : UserControl, IShortCut
{
    public event Action? RequestFindWindow;

    private Label labelIndex;
    private TextBox tbIndex;
    private Label labelDesc;
    private TextBox tbDescription;
    private TextBox tbTexts;

    // コンストラクタ
    public WriteWindow()
    {
        // Create
        labelIndex = Ui.CreateLabel("Index:");
        tbIndex = Ui.CreateTextBox();
        labelDesc = Ui.CreateLabel("Description:");
        tbDescription = Ui.CreateTextBox();
        tbTexts = Ui.CreateTextBoxWithScrollBar();

        // Layout
        Grid mainGrid = new Grid
        {
            ColumnDefinitions = ColumnDefinitions.Parse("auto,*"),
            RowDefinitions = RowDefinitions.Parse("auto,auto,*")
        };

        Grid.SetColumn(labelIndex, 0);
        Grid.SetRow(labelIndex, 0);

        Grid.SetColumn(tbIndex, 1);
        Grid.SetRow(tbIndex, 0);

        Grid.SetColumn(labelDesc, 0);
        Grid.SetRow(labelDesc, 1);

        Grid.SetColumn(tbDescription, 1);
        Grid.SetRow(tbDescription, 1);

        Grid.SetColumn(tbTexts, 0);
        Grid.SetRow(tbTexts, 2);
        Grid.SetColumnSpan(tbTexts, 2);

        mainGrid.Children.Add(labelIndex);
        mainGrid.Children.Add(tbIndex);
        mainGrid.Children.Add(labelDesc);
        mainGrid.Children.Add(tbDescription);
        mainGrid.Children.Add(tbTexts);

        Content = mainGrid;
    }

    // インターフェース実装
    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod)
    {
        switch (e.Key)
        {
            case Key.F:
                Console.WriteLine("Key F");
                RequestFindWindow?.Invoke();
                return true;
        }

        return false;
    }
}
