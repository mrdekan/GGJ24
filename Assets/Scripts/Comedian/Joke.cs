public class Joke
{
    public int FunLvl { get; private set; }
    public string Text { get; private set; }
    public Joke(int funLvl, string text)
    {
        FunLvl = funLvl;
        Text = text;
    }
}
