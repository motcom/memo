using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia;
using System;
using System.Collections.ObjectModel;
using memo.app;

namespace memo.gui;

public class FindWindow : UserControl, IShortCut
{
    public event Action<MemoEntity>? RequestReadWindow;
    public event Action? RequestWriteWindow;

    // コンポネント
    readonly Memo _memo;

    // WidgetItem
    private readonly Label _labelFindn;
    readonly TextBox _textBox;
    readonly ListBox _listBox;
    readonly ObservableCollection<MemoEntity> _lstItems;


    // コンストラクタ
    public FindWindow(Memo memo)
    {
        // コンポネントの参照
        _memo = memo;

        // 検索テキストボックスの生成
        _labelFindn = Ui.CreateLabel("検索:");
        _textBox = Ui.CreateTextBox();

        // リストボックスの生成
        _listBox = Ui.CreateListBox();
        _listBox.ItemTemplate = new FuncDataTemplate<MemoEntity>((memoEnt, _) =>
            new TextBlock
            {
                Text = memoEnt.Index + "\n" + memoEnt.Desc,
                FontSize = 10,
                Margin = new Thickness(2),
                Padding = new Thickness(3),
            });

        // ObservableCollectionの生成
        _lstItems = [];
        // ObservableCollectionの登録
        _listBox.ItemsSource = _lstItems;



        // Layout
        Grid mainGrid = new()

        {
            ColumnDefinitions = ColumnDefinitions.Parse("auto,*"),
            RowDefinitions = RowDefinitions.Parse("auto,*")
        };

        Grid.SetColumn(_labelFindn, 0);
        Grid.SetRow(_labelFindn, 0);

        Grid.SetColumn(_textBox, 1);
        Grid.SetRow(_textBox, 0);

        Grid.SetColumnSpan(_listBox, 2);
        Grid.SetColumn(_listBox, 1);
        Grid.SetRow(_listBox, 1);

        mainGrid.Children.Add(_labelFindn);
        mainGrid.Children.Add(_textBox);
        mainGrid.Children.Add(_listBox);

        // リストボックスの登録
        Content = mainGrid;

        // イベント処理
        _textBox.TextChanged += OnTextBoxTextChange;


    }

    // イベントハンドラ------------------------------------------------------------
    private void OnTextBoxTextChange(object? sender, TextChangedEventArgs e)
    {
        var searchText = _textBox.Text ??= ".*";

        _lstItems.Clear();
        var findResult = _memo.Find(searchText);
        foreach (var memoEnt in findResult)
        {
            _lstItems.Add(memoEnt);
        }
    }


    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod)
    {
        switch (e.Key)
        {

            case Key.W when mod.HasFlag(KeyModifiers.Control):
                Console.WriteLine("Key W");
                RequestWriteWindow?.Invoke();
                return true;

            case Key.J when mod.HasFlag(KeyModifiers.Control):
                OnMoveListFocusn(1);
                break;

            case Key.K when mod.HasFlag(KeyModifiers.Control):
                OnMoveListFocusn(-1);
                break;

            case Key.Enter:
                Console.WriteLine("Key Enter");
                return OnSelectIndex();
        }
        return false;
    }

    // イベントハンドラ
    private bool OnSelectIndex()
    {
        if (_listBox.SelectedItem is not MemoEntity memoEnt) return false;

        RequestReadWindow?.Invoke(memoEnt);
        return true;
    }

    // リストボックスを選択するutility
    private void OnMoveListFocusn(int delta)
    {
        if (_listBox.ItemCount == 0) return;

        var next = _listBox.SelectedIndex + delta;

        if (next >= _listBox.ItemCount) next = 0;
        if (next < 0) next = _listBox.ItemCount - 1;

        _listBox.SelectedIndex = next;
        _listBox.ScrollIntoView(_listBox.SelectedItem!);
        _listBox.Focus();
    }
}
