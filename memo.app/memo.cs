namespace memo.app;

using System.Text.Json;
using System.Text.RegularExpressions;

public class Memo
{

    // フィールド
    List<MemoEntity> _memoLst = new();
    string _memoDirPath;
    string _memoFilePath;


    // コンストラクタ
    public Memo()
    {
        // ディレクトリパスを作成
        _memoDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Memo");

        // ディレクトリがなければディレクトリを作成
        if (!Directory.Exists(_memoDirPath))
            Directory.CreateDirectory(_memoDirPath);

        // ファイルパスを作成
        _memoFilePath = Path.Combine(_memoDirPath, "MemoData.json");
    }

    // データの書き出し
    public void Write(string? index, string? description, string? document)
    {
        // ヌル処理
        if (index == null) return;
        if (description == null) description = "";
        if (document == null) return;

        // 重複排他処理
        if (_memoLst.Any(ent => ent.Index == index)) return;

        // メモリストに追加
        _memoLst.Add(item: new MemoEntity
        {
            Index = index,
            Desc = description,
            Doc = document
        });

        // JSONにシリアライズ
        var json = JsonSerializer.Serialize(_memoLst, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        // ファイルを保存
        File.WriteAllText(_memoFilePath, json);
    }

    // 正規表現で全文検索して整合したものをリストにして返す
    public List<MemoEntity> Find(string seekString)
    {
        Regex re = new Regex(seekString, RegexOptions.IgnoreCase | RegexOptions.Multiline);

        return _memoLst.Where(
                ent =>
               (
                    re.IsMatch(ent.Index) ||
                    re.IsMatch(ent.Desc) ||
                    re.IsMatch(ent.Doc)
               )).ToList();
    }
}
