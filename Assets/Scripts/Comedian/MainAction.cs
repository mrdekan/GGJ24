using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainAction : MonoBehaviour
{
    private List<Joke> jokes = new();
    public int JokesCount => jokes.Count + (firstJoke != null ? 1 : 0) + (secondJoke != null ? 1 : 0);
    [SerializeField] private Transform firstJokeSpawn;
    [SerializeField] private Transform secondJokeSpawn;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Transform canvas;
    [SerializeField] private int wavesCount = 5;
    private int currentWave = 1;
    [SerializeField] private int waveTime = 60;
    [SerializeField] private JokeOnPaper jokePrefab;
    private JokeOnPaper firstJoke;
    private JokeOnPaper secondJoke;
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
    [SerializeField] private GameObject exitCollider;
    [SerializeField] private Checkpoint goHomeTrigger;
    [SerializeField] private List<int> rewards = new();
    [SerializeField] private List<int> funLvlsPerWave = new();
    [SerializeField] private int defeatPurchase = 200;
    [SerializeField] private Animator _jokesPaperAnim;
    private void Start()
    {
        mainLightMaterial.SetColor("_EmissionColor", startColor);
        startTrigger.OnTrigger += OnStartTriggerEnter;
        goHomeTrigger.OnTrigger += OnGoHomeTrigger;
        goHomeTrigger.gameObject.SetActive(false);
        timerText.text = $"0:{waveTime}";
        jokes = Game.Instance.Jokes?.SelectedJokes ?? new()
        {
            new(JokeRarity.Default, "joka joka joka", "Joke tutel!"),
            new(JokeRarity.Legendary, "joka joka joka", "Joke tutelki!"),
            new(JokeRarity.Legendary, "joka joka joka", "Joke tutturu!"),
            new(JokeRarity.Legendary, "jaaaaaaaaaaa", "Joke tutturu!"),
            new(JokeRarity.Legendary, "jssa joka joka", "Joke tutturu!"),
            new(JokeRarity.Legendary, "joka josa joka", "Joke tutturu!"),
            new(JokeRarity.Legendary, "jusa joka joka", "Joke tutturu!")

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
        plMovement.BanAllMovement();
        plMovement.RotateTo(new Vector3(0, 0, 0));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        plMovement.MoveTo(new Vector3(0, plMovement.transform.position.y, 0), ShowJokesPaper);
        //Wave();
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
    private void ShowJokesPaper()
    {
        _jokesPaperAnim.SetTrigger("Show");
    }
    public void SetPlayerMovement(PlayerMovement pm)
    {
        if (pm != null) playerMovement = pm;
    }
    public void WaveAfterComedianLaugh()
    {
        StopAllCoroutines();
        mainLightMaterial.SetColor("_EmissionColor", winColor);
        if (currentWave <= wavesCount && JokesCount > 0)
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
    public void StartWaves(GameObject button)
    {
        Wave();
        Destroy(button);
    }
    public void Wave()
    {
        Game.Instance.Comedians.SetComedianFunLvls(funLvlsPerWave[currentWave - 1]);
        StartCoroutine(Timer());
        UpdateJokes();
        currentWave++;
    }
    public void JokesEnded()
    {
        StopAllCoroutines();
        mainLightMaterial.SetColor("_EmissionColor", defeatColor);
        Game.Instance.Progress.PurchaseMoney(defeatPurchase, true);
        EndGame();
    }
    private void EndGame()
    {
        _jokesPaperAnim.SetTrigger("Hide");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.AllowAllMovement();
        goHomeTrigger.gameObject.SetActive(true);
        exitCollider.SetActive(false);
        if (firstJoke)
            Destroy(firstJoke.gameObject);
        if (secondJoke)
            Destroy(secondJoke.gameObject);
    }
    public void UpdateOneJoke(JokeOnPaper previous)
    {
        if (firstJoke == previous && jokes.Count > 0)
            firstJoke = SpawnJoke(GetNextJoke(), firstJokeSpawn.position);
        else if (jokes.Count > 0)
            secondJoke = SpawnJoke(GetNextJoke(), secondJokeSpawn.position);
        Destroy(previous.gameObject);
    }
    private JokeOnPaper SpawnJoke(Joke joke, Vector3 position)
    {
        var jokeObj = Instantiate(jokePrefab, canvas);
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
        var joke = jokes.First();
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
        if (Game.Instance.Comedians.IsJokeTelling)
        {
            if (Game.Instance.Comedians.TellJoke())
            {
                WaveAfterComedianLaugh();
                StopAllCoroutines();
            }
        }
        mainLightMaterial.SetColor("_EmissionColor", defeatColor);
        Game.Instance.Progress.PurchaseMoney(defeatPurchase, true);
        EndGame();
    }
}
