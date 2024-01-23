using System;

public enum JokeRarity
{
    Default = 5,
    Rare = 7,
    Epic = 12,
    Legendary = 20
}
[Serializable]
public class Joke
{
    public JokeRarity Rarity { get; private set; }
    public string Text { get; private set; }
    public string Title { get; private set; }
    public Joke(JokeRarity rarity, string text, string title)
    {
        Rarity = rarity;
        Text = text;
        Title = title;
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        Joke other = (Joke)obj;
        return (Rarity == other.Rarity) && (Text == other.Text) && (Title == other.Title);
    }
}
