using System.Collections.Generic;
using UnityEngine;

public static class GlobalJokesList
{
    private static int legendChance = 3;
    private static int increasedLegendChance = 10;
    private static int epicChance = 20;
    private static int rareChance = 30;
    private static int newJokesCount = 5;
    private static readonly List<Joke> _jokes = new()
    {
        new(JokeRarity.Default, "111111111", "222222222222!")
    };
    private static readonly List<Joke> _defJokes = new()
    {
        new(JokeRarity.Default, "joke\n joke\n joke", "Joke title!"),
        new(JokeRarity.Default, "joka\n joka\n joka", "Joke tutellya!"),
        new(JokeRarity.Default, "jaba\n jaba\n jaba", "Jose tutellya!"),
        new(JokeRarity.Default, "joka\n jaba\n joka", "Jobe tutellya!"),
        new(JokeRarity.Default, "joka\n joka\n jaba", "Tuta tutellya!"),
        new(JokeRarity.Default, "jaba\n joka\n joka", "Jabab tutellya!"),
        new(JokeRarity.Default, "joka\n jaba\n jaba", "Joba tutellya!"),
    };
    private static readonly List<Joke> _rareJokes = new()
    {
        new(JokeRarity.Rare, "joka joka joka", "Joke tutelko!"),
        new(JokeRarity.Rare, "joka joka joka", "Joke tutelka!"),
        new(JokeRarity.Rare, "joka joka joka", "Joke tutelku!"),
        new(JokeRarity.Rare, "joka joka joka", "Joke tutelbiba!"),
        new(JokeRarity.Rare, "joka joka joka", "BOBUR!"),
        new(JokeRarity.Rare, "joka joka joka", "BOBURA!"),
        new(JokeRarity.Rare, "joka joka joka", "BOBURO!"),
        new(JokeRarity.Rare, "joka joka joka", "BOBRUHO!"),
    };
    private static readonly List<Joke> _epicJokes = new()
    {
        new(JokeRarity.Epic, "joka joka joka", "Bobra!"),
        new(JokeRarity.Epic, "joka joka joka", "Bebra!"),
        new(JokeRarity.Epic, "joka joka joka", "bubiru!"),
        new(JokeRarity.Epic, "joka joka joka", "chipi chipi lorem ipsum"),
        new(JokeRarity.Epic, "joka joka joka", "chinazez"),
        new(JokeRarity.Epic, "joka joka joka", "chinaaa"),
    };
    private static readonly List<Joke> _legendJokes = new()
    {
        new(JokeRarity.Legendary, "joka joka joka", "Joke tutrel!"),
        new(JokeRarity.Legendary, "joka joka joka", "Joke turtel!"),
        new(JokeRarity.Legendary, "joka joka joka", "Joke trutel!"),
        new(JokeRarity.Legendary, "joka joka joka", "Joke turkel!"),
        new(JokeRarity.Legendary, "joka joka joka", "Joke titel!"),
    };
    public static List<Joke> Jokes => _jokes;
    public static void RemoveJokes(List<Joke> jokes)
    {
        foreach (Joke joke in jokes)
            _jokes.Remove(joke);
    }
    public static List<Joke> GenerateNewJokes(List<Joke> current, bool increaseLegendChance, bool extraJoke)
    {
        var def = RemoveListFromList(_defJokes, current);
        var rare = RemoveListFromList(_rareJokes, current);
        var epic = RemoveListFromList(_epicJokes, current);
        var leg = RemoveListFromList(_legendJokes, current);
        int jokesCnt = newJokesCount;
        if (extraJoke) jokesCnt++;
        List<Joke> jokes = new List<Joke>();
        int tempLegChance = increaseLegendChance ? increasedLegendChance : legendChance;

        for (int i = 0; i < jokesCnt; i++)
        {
            int rand = Random.Range(0, 100);

            if (rand < tempLegChance)
            {
                Joke jokeToAdd = PopRandom(leg) ?? PopRandom(epic) ?? PopRandom(rare) ?? PopRandom(def);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
            else if (rand < tempLegChance + epicChance)
            {
                Joke jokeToAdd = PopRandom(epic) ?? PopRandom(rare) ?? PopRandom(def) ?? PopRandom(leg);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
            else if (rand < tempLegChance + epicChance + rareChance)
            {
                Joke jokeToAdd = PopRandom(rare) ?? PopRandom(def) ?? PopRandom(epic) ?? PopRandom(leg);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
            else
            {
                Joke jokeToAdd = PopRandom(def) ?? PopRandom(rare) ?? PopRandom(epic) ?? PopRandom(leg);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
        }
        return jokes;
    }
    private static Joke PopRandom(List<Joke> jokes)
    {
        if (jokes == null || jokes.Count == 0)
            return null;

        int index = Random.Range(0, jokes.Count);
        Joke poppedJoke = jokes[index];
        jokes.RemoveAt(index);
        return poppedJoke;
    }
    private static List<Joke> RemoveListFromList(List<Joke> a, List<Joke> b)
    {
        List<Joke> res = new List<Joke>(a);
        foreach (Joke joke in b)
            res.Remove(joke);
        return res;
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
