using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainAction : MonoBehaviour
{
    private List<Joke> jokes = new();
    [SerializeField] private Transform firstJokeSpawn;
    [SerializeField] private Transform secondJokeSpawn;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Transform canvas;
    [SerializeField] private int wavesCount = 5;
    private int currentWave = 1;
    [SerializeField] private int waveTime = 60;
    [SerializeField] private JokeTable jokePrefab;
    private JokeTable firstJoke;
    private JokeTable secondJoke;
    [SerializeField] private Material mainLightMaterial;
    [ColorUsage(true, true)]
    [SerializeField] private Color winColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color defeatColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color startColor;
    [SerializeField] private float pause;
    private PlayerMovement playerMovement;
    [Header("Checkpoints")]
    [SerializeField] private Checkpoint startTrigger;
    [SerializeField] private Checkpoint goHomeTrigger;
    [SerializeField] private List<int> rewards = new();
    [SerializeField] private List<int> funLvlsPerWave = new();
    [SerializeField] private int defeatPurchase = 200;
    private void Start()
    {
        mainLightMaterial.SetColor("_EmissionColor", startColor);
        startTrigger.OnTrigger += OnStartTriggerEnter;
        goHomeTrigger.OnTrigger += OnGoHomeTrigger;
        goHomeTrigger.gameObject.SetActive(false);
        timerText.text = $"0:{waveTime}";
        jokes = Game.Instance.Jokes?.SelectedJokes ?? new()
        {
            new(JokeRarity.Rare, "joka joka joka", "Joke tutel!"),
            new(JokeRarity.Rare, "joka joka joka", "Joke tutelki!"),
            new(JokeRarity.Rare, "joka joka joka", "Joke tutturu!"),
        };
        if (jokes == null)
        {
            jokes = new();
            for (int i = 0; i < 20; i++)
            {
                jokes.Add(new(JokeRarity.Default, "joke joke joke", "Joke title!"));
            }
        }
        /*jokes.Add(new(5, "joke joke joke", "Joke title!"));
        jokes.Add(new(10, "joka joka joka", "Joke tutel!"));
        jokes.Add(new(3, "joka joka joka", "Joke tutelki!"));
        jokes.Add(new(5, "joka joka joka", "Joke tutturu!"));
        jokes.Add(new(15, "joka joka joka", "Joke tutellya!"));*/
    }
    #region Triggers
    private void OnStartTriggerEnter(Collider other, Action callback)
    {
        if (currentWave > 1) return;
        var plMovement = other.gameObject.GetComponent<PlayerMovement>();
        if (plMovement == null) return;
        SetPlayerMovement(plMovement);
        plMovement.BanWalking();
        plMovement.MoveTo(new Vector3(0, plMovement.transform.position.y, 0));
        Wave();
        callback();
    }
    private void OnGoHomeTrigger(Collider other, Action callback)
    {
        var plMovement = other.gameObject.GetComponent<PlayerMovement>();
        if (plMovement == null) return;
        Game.Instance.Buttons.LoadHomeScene();
        callback();
    }
    #endregion
    public void SetPlayerMovement(PlayerMovement pm)
    {
        if (pm != null) playerMovement = pm;
    }
    public void WaveAfterComedianLaugh()
    {
        StopAllCoroutines();
        mainLightMaterial.SetColor("_EmissionColor", winColor);
        if (currentWave <= wavesCount)
        {
            StartCoroutine(WaveAfterWin());
        }
        else
        {
            Game.Instance.Progress.AddMoney(rewards[currentWave - 2]);
            EndGame();
        }
    }
    private IEnumerator WaveAfterWin()
    {
        yield return new WaitForSeconds(pause);
        mainLightMaterial.SetColor("_EmissionColor", startColor);
        Wave();
    }
    public void Wave()
    {
        Game.Instance.Comedians.SetComedianFunLvls(funLvlsPerWave[currentWave - 1]);
        StartCoroutine(Timer());
        UpdateJokes();
        currentWave++;
    }
    private void EndGame()
    {
        playerMovement.AllowWalking();
        goHomeTrigger.gameObject.SetActive(true);
        if (firstJoke)
            Destroy(firstJoke.gameObject);
        if (secondJoke)
            Destroy(secondJoke.gameObject);
    }
    public void UpdateOneJoke(JokeTable previous)
    {
        if (firstJoke == previous && jokes.Count > 0)
            firstJoke = SpawnJoke(GetNextJoke(), firstJokeSpawn.position);
        else if (jokes.Count > 0)
            secondJoke = SpawnJoke(GetNextJoke(), secondJokeSpawn.position);
        Destroy(previous.gameObject);
    }
    private JokeTable SpawnJoke(Joke joke, Vector3 position)
    {
        var jokeObj = Instantiate(jokePrefab, canvas);
        jokeObj.transform.position = position;
        jokeObj.transform.LookAt(new Vector3(0, 4f, 0));
        jokeObj.SetInfo(joke);
        return jokeObj;
    }
    private void UpdateJokes()
    {
        if (jokes.Count > 0 && firstJoke == null)
            firstJoke = SpawnJoke(GetNextJoke(), firstJokeSpawn.position);
        if (jokes.Count > 0 && secondJoke == null)
            secondJoke = SpawnJoke(GetNextJoke(), secondJokeSpawn.position);
    }
    private Joke GetNextJoke()
    {
        var joke = jokes[UnityEngine.Random.Range(0, jokes.Count)];
        jokes.Remove(joke);
        return joke;
    }
    private IEnumerator Timer()
    {
        int timer = waveTime;
        while (true)
        {
            if (timer < 0) break;
            if (timer == 60)
                timerText.text = "1:00";
            else if (timer > 9)
                timerText.text = $"0:{timer}";
            else
                timerText.text = $"0:0{timer}";
            timer--;
            yield return new WaitForSeconds(1);
        }
        mainLightMaterial.SetColor("_EmissionColor", defeatColor);
        Game.Instance.Progress.PurchaseMoney(defeatPurchase, true);
        EndGame();
    }
}
