using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainAction : MonoBehaviour
{
    private List<Joke> jokes = new();
    private AudioSource _audio;
    public int JokesCount => jokes.Count + (firstJoke != null ? 1 : 0) + (secondJoke != null ? 1 : 0);
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
    [SerializeField] private List<GameObject> _decisionUI;
    [SerializeField] private TextMeshProUGUI _getMoneyText;
    [SerializeField] private TextMeshProUGUI _balance;
    [SerializeField] private TextMeshProUGUI _jokesLeft;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private PlayRandomSound _defeatSound;
    private bool isTraining = false;
    public delegate void BaseEvent();
    public BaseEvent OnTimerEnd;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        mainLightMaterial.SetColor("_EmissionColor", startColor);
        startTrigger.OnTrigger += OnStartTriggerEnter;
        goHomeTrigger.OnTrigger += OnGoHomeTrigger;
        goHomeTrigger.gameObject.SetActive(false);
        timerText.text = $"0:{waveTime}";
        jokes = Game.Instance.Jokes?.SelectedJokes ?? new()
        {
            /*new(JokeRarity.Default, "joka joka joka", "Joke tutel!"),
            new(JokeRarity.Legendary, "joka joka joka", "Joke tutelki!"),
            new(JokeRarity.Legendary, "joka joka joka", "Joke tutturu!"),
            new(JokeRarity.Legendary, "jaaaaaaaaaaa", "Joke tutturu!"),
            new(JokeRarity.Legendary, "jssa joka joka", "Joke tutturu!"),
            new(JokeRarity.Legendary, "joka josa joka", "Joke tutturu!"),*/
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
        UpdateStats();
    }
    public void UpdateStats()
    {
        _balance.text = $"{Game.Instance.Settings.CorrectLanguageString("Balance", "Баланс")}: ${Math.Round((Game.Instance.Progress.Balance) / 1000f, 2)}k";
        _jokesLeft.text = $"{Game.Instance.Settings.CorrectLanguageString("Jokes left", "Жартів залишилося")}: {JokesCount}";
    }
    public void SetIsOnTraining()
    {
        isTraining = true;
    }
    #region Triggers
    private void OnStartTriggerEnter(Collider other, Action callback)
    {
        if (currentWave > 1) return;
        var plMovement = other.gameObject.GetComponent<PlayerMovement>();
        if (plMovement == null) return;
        playerMovement = plMovement;
        plMovement.BanAllMovement();
        plMovement.RotateTo(new Vector3(0, 0, 0));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        plMovement.MoveTo(new Vector3(0, plMovement.transform.position.y, 0), ShowJokesPaper);
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
    public void StartTellingJoke()
    {
        firstJoke?.SetInteractable(false);
        secondJoke?.SetInteractable(false);
    }
    public void WaveAfterComedianLaugh()
    {
        StopAllCoroutines();
        mainLightMaterial.SetColor("_EmissionColor", winColor);
        if (currentWave <= wavesCount && JokesCount > 0)
        {
            try
            {
                firstJoke?.gameObject?.SetActive(false);
            }
            catch { }
            try
            {
                secondJoke?.gameObject?.SetActive(false);
            }
            catch { }
            _getMoneyText.text = Game.Instance.Settings.CorrectLanguageString($"Take ${rewards[currentWave - 2]}", $"Забрати {rewards[currentWave - 2]}$");
            foreach (var obj in _decisionUI)
                obj.SetActive(true);
            if (isTraining || JokesCount <= 1)
                _acceptButton.interactable = false;
        }
        else
        {
            GetMoneyClicked();
        }
    }
    public void NextWaveClicked()
    {
        try
        {
            firstJoke?.gameObject?.SetActive(true);
        }
        catch { }
        try
        {
            secondJoke?.gameObject?.SetActive(true);
        }
        catch { }
        foreach (var obj in _decisionUI)
            obj.SetActive(false);
        Wave();
        mainLightMaterial.SetColor("_EmissionColor", startColor);
    }
    public void GetMoneyClicked()
    {
        _audio.Play();
        Game.Instance.Progress.AddMoney(rewards[currentWave - 2]);
        EndGame();
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
        _defeatSound.Play();
        EndGame();
    }
    private void EndGame()
    {
        _jokesPaperAnim.SetTrigger("Hide");
        UpdateStats();
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
            firstJoke = SpawnJoke(GetNextJoke());
        else if (jokes.Count > 0)
            secondJoke = SpawnJoke(GetNextJoke());
        Destroy(previous.gameObject);
        firstJoke?.SetInteractable(true);
        secondJoke?.SetInteractable(true);
        UpdateStats();
    }
    private JokeOnPaper SpawnJoke(Joke joke)
    {
        var jokeObj = Instantiate(jokePrefab, canvas);
        jokeObj.SetInfo(joke);
        return jokeObj;
    }
    private void UpdateJokes()
    {
        if (jokes.Count > 0 && firstJoke == null)
            firstJoke = SpawnJoke(GetNextJoke());
        if (jokes.Count > 0 && secondJoke == null)
            secondJoke = SpawnJoke(GetNextJoke());
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
                yield break;
            }
        }
        if (!isTraining)
        {
            mainLightMaterial.SetColor("_EmissionColor", defeatColor);
            Game.Instance.Progress.PurchaseMoney(defeatPurchase, true);
            _defeatSound.Play();
            EndGame();
        }
        else
        {
            OnTimerEnd?.Invoke();
        }
    }
}
