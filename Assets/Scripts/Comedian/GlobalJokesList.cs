using System.Collections.Generic;

public static class GlobalJokesList
{
    private static readonly List<Joke> _jokes = new()
    {
        new(JokeRarity.Default, "joke joke joke", "Joke title!"),
        new(JokeRarity.Default, "joka joka joka", "Joke tutellya!"),
        new(JokeRarity.Default, "jaba jaba jaba", "Jose tutellya!"),
        new(JokeRarity.Default, "joka jaba joka", "Jobe tutellya!"),
        new(JokeRarity.Default, "joka joka jaba", "Tuta tutellya!"),
        new(JokeRarity.Default, "jaba joka joka", "Jabab tutellya!"),
        new(JokeRarity.Default, "joka jaba jaba", "Joba tutellya!"),
        new(JokeRarity.Legendary, "joka joka joka", "Joke tutel!"),
        new(JokeRarity.Rare, "joka joka joka", "Joke tutelki!"),
        new(JokeRarity.Epic, "joka joka joka", "Joke tutturu!"),
    };
    public static List<Joke> Jokes => _jokes;
    public static void RemoveJokes(List<Joke> jokes)
    {
        foreach (Joke joke in jokes)
            _jokes.Remove(joke);
    }
    public static List<Joke> GetInitialJokes() =>
        new()
        {
            _jokes[0],
            _jokes[1],
            _jokes[2],
            _jokes[3],
            _jokes[4],
            _jokes[5],
        };
}
