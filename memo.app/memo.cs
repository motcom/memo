namespace memo.app;

using System.Text.Json;
using System.Text.RegularExpressions;

public class Memo
{

    // フィールド
    readonly List<MemoEntity> _memoLst = [];
    readonly string _memoFilePath;
    private readonly JsonSerializerOptions _jsonSerialOpt;



    // コンストラクタ
    public Memo()
    {
        // ディレクトリパスを作成
        string _memoDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Memo");

        // ディレクトリがなければディレクトリを作成
        if (!Directory.Exists(_memoDirPath))
            Directory.CreateDirectory(_memoDirPath);


        // ファイルパスを作成
        _memoFilePath = Path.Combine(_memoDirPath, "MemoData.json");
        _jsonSerialOpt = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // メモリリストがあればそれをロードする
        if (File.Exists(_memoFilePath))
        {
            var allText = File.ReadAllText(_memoFilePath);
            _memoLst = JsonSerializer.Deserialize<List<MemoEntity>>(allText, _jsonSerialOpt) ?? [];
        }

    }

    // データの書き出し
    public MemoResult Write(string? index, string? description, string? document)
    {
        // ヌル処理
        if (index == null) return MemoResult.INDEX_NOTFOUND;
        description ??= "";
        if (document == null) return MemoResult.DOCUMENT_NOTFOUND;

        // 重複排他処理
        if (_memoLst.Any(ent => ent.Index == index)) return MemoResult.DUPLICATE_INDEX;

        // メモリストに追加
        _memoLst.Add(item: new MemoEntity
        {
            Index = index,
            Desc = description,
            Doc = document
        });

        // JSONにシリアライズ
        var json = JsonSerializer.Serialize(_memoLst, _jsonSerialOpt);

        // ファイルを保存
        File.WriteAllText(_memoFilePath, json);

        return MemoResult.OK;
    }

    public string GetDocumentWithIndex(string index)
    {
        return _memoLst.Find(ent => ent.Index == index)?.Doc ?? "";
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
