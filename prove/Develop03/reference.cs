class Reference
{
    private string _book;
    private int _chapter;
    private List<int> _verses;

    public Reference(string book, int chapter, List<int> verses)
    {
        _book = book;
        _chapter = chapter;
        _verses = verses;
    }

    public string GetDisplayText()
    {
        if (_verses.Count == 1)
        {
            return $"{_book} {_chapter}:{_verses[0]}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verses[0]}-{_verses[1]}";
        }
    }
}