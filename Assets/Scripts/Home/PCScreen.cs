using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PCScreen : MonoBehaviour
{
    [SerializeField] private Transform userJokesList;
    [SerializeField] private GameObject textEditor;
    [SerializeField] private GameObject shop;
    [SerializeField] private Transform selectedJokesList;
    [SerializeField] private JokePanel jokePrefab;
    [SerializeField] private TextMeshProUGUI userJokesTitle;
    [SerializeField] private TextMeshProUGUI selectedJokesTitle;
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private TextMeshProUGUI balance;
    [SerializeField] private GameObject newJokesPanel;
    [SerializeField] private Transform newJokesParent;
    [SerializeField] private Button printBtn;
    public delegate void BaseEvent();
    public BaseEvent OnShopOpen;
    public List<Joke> SelectedJokes { get; private set; }
    private List<Joke> _userJokes;
    private void Awake()
    {
        try
        {
            userName.text = Environment.UserName;
            UpdateBalance();
        }
        catch
        {
            userName.text = "Professional";
        }
        if (userName.text == "") userName.text = "Professional";
        Game.Instance.Jokes.AddedJokes += HandleNewJokes;
        Game.Instance.Progress.OnBalanceUpdate += HandleBalance;
    }
    private void HandleNewJokes(List<Joke> newJokes)
    {
        foreach (Transform child in newJokesParent)
            Destroy(child.gameObject);
        foreach (Joke joke in newJokes)
            Instantiate(jokePrefab, newJokesParent).SetInfo(joke, false, this, true);
        newJokesPanel.SetActive(true);
    }
    private void HandleBalance()
    {
        UpdateBalance();
    }
    private void Start()
    {
        OpenTextEditor();
    }
    private void SetUserJokes(List<Joke> jokes)
    {
        _userJokes = new(jokes);
        _userJokes = _userJokes.OrderByDescending(el => el.Rarity).ToList();
        foreach (Joke joke in _userJokes)
        {
            if (SelectedJokes != null && !SelectedJokes.Contains(joke))
                Instantiate(jokePrefab, userJokesList).SetInfo(joke, false, this);
        }
    }
    private void UpdatePrintBtn() =>
        printBtn.interactable = SelectedJokes.Count > 0;
    private void UpdateBalance() =>
        balance.text = $"${Math.Round(Game.Instance.Progress.Balance / 1000f, 2)}k";
    private void UpdateTitles()
    {
        UpdatePrintBtn();
        string temp = Game.Instance.Settings.CorrectLanguageString("All jokes", "Усі жарти");
        userJokesTitle.text = $"{temp} ({_userJokes.Count - SelectedJokes.Count})";
        temp = Game.Instance.Settings.CorrectLanguageString("Selected jokes", "Вибрані жарти");
        selectedJokesTitle.text = $"{temp} ({SelectedJokes.Count}/{(Game.Instance.Progress.Has(Upgrades.PC) ? "15" : "10")})";
    }
    public void MoveToInventory(JokePanel joke)
    {
        joke.transform.SetParent(userJokesList);
        SelectedJokes.Remove(joke.JokeInfo);
        UpdateTitles();
    }
    public bool MoveToSelected(JokePanel joke)
    {
        if (SelectedJokes.Count >= (Game.Instance.Progress.Has(Upgrades.PC) ? 15 : 10)) return false;
        joke.transform.SetParent(selectedJokesList);
        SelectedJokes.Add(joke.JokeInfo);
        UpdateTitles();
        return true;
    }
    private void SetSelectedJokes(List<Joke> jokes)
    {
        SelectedJokes = new(jokes);
        foreach (Joke joke in jokes)
            Instantiate(jokePrefab, selectedJokesList).SetInfo(joke, true, this);
    }
    public void SetJokes(List<Joke> userJokes, List<Joke> selectedJokes, bool redraw = true)
    {
        if (redraw)
        {
            foreach (Transform child in selectedJokesList)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Transform child in userJokesList)
        {
            Destroy(child.gameObject);
        }
        if (redraw)
            SetSelectedJokes(selectedJokes);
        SetUserJokes(userJokes);
        UpdateTitles();
    }
    public void OpenShop()
    {
        OnShopOpen?.Invoke();
        shop.SetActive(true);
        textEditor.SetActive(false);
    }
    public void OpenTextEditor()
    {
        if (_userJokes.Count != Game.Instance.Jokes.UserJokes.Count)
            SetJokes(Game.Instance.Jokes.UserJokes, Game.Instance.Jokes.SelectedJokes, false);
        textEditor.SetActive(true);
        shop.SetActive(false);
    }
}
