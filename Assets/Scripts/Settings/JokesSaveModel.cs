using System;
using System.Collections.Generic;

namespace Assets.Scripts.Settings
{
    [Serializable]
    public class JokesSaveModel
    {
        public List<Joke> Jokes { get; set; }
        public JokesSaveModel(List<Joke> jokes)
        {
            Jokes = jokes;
        }
    }
}
