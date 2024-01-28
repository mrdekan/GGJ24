using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    [SerializeField] private List<Image> funLvlImages;
    [SerializeField] private List<Color> funLvlColors;
    public Joke JokeInfo { get; private set; }
    private PCScreen _screen;
    private bool _isSelected;
    public void SetInfo(Joke joke, bool isSelected, PCScreen screen, bool hideButton = false)
    {
        JokeInfo = joke;
        title.text = joke.Title;
        content.text = joke.Text;
        buttonText.text = isSelected ? Game.Instance.Settings.CorrectLanguageString("Remove", "Видалити") : Game.Instance.Settings.CorrectLanguageString("Add", "Додати");
        _screen = screen;
        _isSelected = isSelected;
        button.onClick.AddListener(delegate { HandleClick(); });
        if (hideButton) button.gameObject.SetActive(false);
        int lvl = 1;
        switch (joke.Rarity)
        {
            case JokeRarity.Default: lvl = 1; break;
            case JokeRarity.Rare: lvl = 2; break;
            case JokeRarity.Epic: lvl = 3; break;
            case JokeRarity.Legendary: lvl = 4; break;
        }
        for (int i = 0; i < lvl; i++)
            funLvlImages[i].color = funLvlColors[i];
    }
    private void HandleClick()
    {
        if (_isSelected)
            _screen.MoveToInventory(this);
        else
            if (!_screen.MoveToSelected(this)) return;
        _isSelected = !_isSelected;
        buttonText.text = _isSelected ? Game.Instance.Settings.CorrectLanguageString("Remove", "Видалити") : Game.Instance.Settings.CorrectLanguageString("Add", "Додати");
    }
}
