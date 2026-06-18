using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Collections.ObjectModel;
using memo.app;

namespace memo.gui;

public class FindWindow : UserControl, IShortCut
{
    public event Action? RequestReadWindow;
    public event Action? RequestWriteWindow;
    int _listForcusIndex;
    Memo _memo;

    // WidgetItem
    Label labelFind;
    TextBox textBox;
    ListBox listBox;
    ObservableCollection<string> lstItems;

    // コンストラクタ
    public FindWindow(Memo memo)
    {
        this._memo = memo;

        // 検索テキストボックスの生成
        labelFind = Ui.CreateLabel("検索:");
        textBox = Ui.CreateTextBox();

        // リストボックスの生成
        listBox = Ui.CreateListBox();

        // ObservableCollectionの生成
        lstItems = new ObservableCollection<string>();
        // ObservableCollectionの登録
        listBox.ItemsSource = lstItems;

        // lstItemsの更新 -----------------------------Dummy -----------------------SetColumn
        var strs = DummyStrings.GetDummyStrings();
        foreach (var str in strs)
        {
            lstItems.Add(str);
        }
        Console.WriteLine(lstItems.Count);


        // Layout
        Grid mainGrid = new Grid
        {
            ColumnDefinitions = ColumnDefinitions.Parse("auto,*"),
            RowDefinitions = RowDefinitions.Parse("auto,*")
        };

        Grid.SetColumn(labelFind, 0);
        Grid.SetRow(labelFind, 0);

        Grid.SetColumn(textBox, 1);
        Grid.SetRow(textBox, 0);

        Grid.SetColumnSpan(listBox, 2);
        Grid.SetColumn(listBox, 1);
        Grid.SetRow(listBox, 1);

        mainGrid.Children.Add(labelFind);
        mainGrid.Children.Add(textBox);
        mainGrid.Children.Add(listBox);

        // リストボックスの登録
        Content = mainGrid;


    }

    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod)
    {
        switch (e.Key)
        {
            case Key.R:
                Console.WriteLine("Key R");
                RequestReadWindow?.Invoke();
                return true;

            case Key.W:
                Console.WriteLine("Key W");
                RequestWriteWindow?.Invoke();
                return true;

            case Key.J:
                this._listForcusIndex++;
                if (this._listForcusIndex >= this.listBox.ItemCount)
                {
                    _listForcusIndex = 0;
                }

                break;


        }
        return false;
    }
}
