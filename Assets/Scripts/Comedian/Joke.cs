public class Joke
{
    public int FunLvl { get; private set; }
    public string Text { get; private set; }
    public string Title { get; private set; }
    public Joke(int funLvl, string text, string title)
    {
        FunLvl = funLvl;
        Text = text;
        Title = title;
    }
}
