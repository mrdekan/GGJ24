using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    }
    private void SetUserJokes(List<Joke> jokes)
    {
        _userJokes = jokes;
        foreach (Joke joke in jokes)
        {
            if (SelectedJokes != null && !SelectedJokes.Contains(joke))
                Instantiate(jokePrefab, userJokesList).SetInfo(joke, false, this);
        }
    }
    private void UpdateBalance()
    {
        balance.text = $"${Math.Round(Game.Instance.Progress.Balance / 1000f, 1)}k";
    }
    private void UpdateTitles()
    {
        userJokesTitle.text = $"All jokes ({_userJokes.Count - SelectedJokes.Count})";
        selectedJokesTitle.text = $"Selected jokes ({SelectedJokes.Count}/20)";
    }
    public void MoveToInventory(JokePanel joke)
    {
        joke.transform.SetParent(userJokesList);
        SelectedJokes.Remove(joke.JokeInfo);
        UpdateTitles();
    }
    public void MoveToSelected(JokePanel joke)
    {
        joke.transform.SetParent(selectedJokesList);
        SelectedJokes.Add(joke.JokeInfo);
        UpdateTitles();
    }
    private void SetSelectedJokes(List<Joke> jokes)
    {
        SelectedJokes = jokes;
        foreach (Joke joke in jokes)
            Instantiate(jokePrefab, selectedJokesList).SetInfo(joke, true, this);
    }
    public void SetJokes(List<Joke> userJokes, List<Joke> selectedJokes)
    {
        foreach (Transform child in selectedJokesList)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in userJokesList)
        {
            Destroy(child.gameObject);
        }
        SetSelectedJokes(selectedJokes);
        SetUserJokes(userJokes);
        UpdateTitles();
    }
    public void OpenShop()
    {
        shop.SetActive(true);
        textEditor.SetActive(false);
    }
    public void OpenTextEditor()
    {
        textEditor.SetActive(true);
        shop.SetActive(false);
    }
}
