using Avalonia.Input;

namespace memo.gui;
public interface IShortCut
{
    public bool OnShortCut(KeyEventArgs e, KeyModifiers mod);
}
