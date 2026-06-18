namespace memo.app;

public class MainClass
{
    public static void Main(string[] arg)
    {
        Memo memo = new Memo();

        // テストデータ追加
        memo.Write("001", "Maya メモ", "Maya Python の ls フルパス取得");
        memo.Write("002", "C# メモ", "List と Dictionary の使い分け");
        memo.Write("003", "Avalonia", "TextBox と Button のテスト");
        memo.Write("004", "Regex", "正規表現で全文検索する");
        memo.Write("005", "JSON", "System.Text.Json で保存する");
        memo.Write("006", "LINQ", "Where と ToList の使い方");
        memo.Write("007", "Neovim", "LazyVim の LSP 設定");
        memo.Write("008", "Memo App", "Index Desc Doc の設計");
        memo.Write("009", "PlantUML", "クラス図とシーケンス図");
        memo.Write("010", "Search Test", "ignorecase multiline regex");

        // 検索テスト
        TestFind(memo, "maya");
        TestFind(memo, "C#");
        TestFind(memo, "TextBox");
        TestFind(memo, "json");
        TestFind(memo, "LINQ");
        TestFind(memo, "lsp");
        TestFind(memo, "Index");
        TestFind(memo, "正規表現");
        TestFind(memo, "regex");
        TestFind(memo, "存在しない文字列");
    }

    static void TestFind(Memo memo, string seekString)
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"検索文字列: {seekString}");

        var results = memo.Find(seekString);

        Console.WriteLine($"ヒット数: {results.Count}");

        foreach (var ent in results)
        {
            Console.WriteLine($"Index: {ent.Index}");
            Console.WriteLine($"Desc : {ent.Desc}");
            Console.WriteLine($"Doc  : {ent.Doc}");
            Console.WriteLine();
        }
    }
}
