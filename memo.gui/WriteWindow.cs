using Avalonia.Controls;
using Avalonia.Input;
using System;
using memo.app;
using Avalonia.Interactivity;

namespace memo.gui;

public class WriteWindow : UserControl, IShortCut
{
    public event Action? RequestFindWindow;

    Label _labelIndex;
    TextBox _tbIndex;
    Label _labelDesc;
    TextBox _tbDescription;
    TextBox _tbTexts;
    Button _btnExe;

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
        _btnExe = Ui.CreateButton("exe");

        // Layout
        Grid mainGrid = new Grid
        {
            ColumnDefinitions = ColumnDefinitions.Parse("auto,*"),
            RowDefinitions = RowDefinitions.Parse("auto,auto,*,*")
        };

        Grid.SetColumn(_labelIndex, 0);
        Grid.SetRow(_labelIndex, 0);

        Grid.SetColumn(_tbIndex, 1);
        Grid.SetRow(_tbIndex, 0);

        Grid.SetColumn(_labelDesc, 0);
        Grid.SetRow(_labelDesc, 1);

        Grid.SetColumn(_tbDescription, 1);
        Grid.SetRow(_tbDescription, 1);

        Grid.SetColumn(_btnExe, 0);
        Grid.SetRow(_btnExe, 2);
        Grid.SetColumnSpan(_btnExe, 2);


        Grid.SetColumn(_tbTexts, 0);
        Grid.SetRow(_tbTexts, 3);
        Grid.SetColumnSpan(_tbTexts, 2);

        mainGrid.Children.Add(_labelIndex);
        mainGrid.Children.Add(_tbIndex);
        mainGrid.Children.Add(_labelDesc);
        mainGrid.Children.Add(_tbDescription);
        mainGrid.Children.Add(_btnExe);
        mainGrid.Children.Add(_tbTexts);

        Content = mainGrid;

        // イベント登録
        _btnExe.Click += OnExeBtn;
    }

    private void OnExeBtn(object? sender, RoutedEventArgs e)
    {
        var index = _tbIndex.Text;
        var desc = _tbDescription.Text;
        var text = _tbTexts.Text;
        _memo.Write(index, desc, text);
    }

    // インターフェース実装
    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod)
    {
        switch (e.Key)
        {
            // ファインドウィンドウに遷移
            case Key.F when mod.HasFlag(KeyModifiers.Control):
                Console.WriteLine("Key F");
                RequestFindWindow?.Invoke();
                return true;

            case Key.S when mod.HasFlag(KeyModifiers.Control):
                if (_tbIndex.Text is null) return false;
                if (_tbTexts.Text is null) return false;

                string index = _tbIndex.Text;
                string desc = _tbDescription?.Text ?? "";
                string doc = _tbTexts.Text;
                _memo.Write(index, desc, doc);

                return true;
        }

        return false;
    }
}
