using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    public Joke JokeInfo { get; private set; }
    private PCScreen _screen;
    private bool _isSelected;
    public void SetInfo(Joke joke, bool isSelected, PCScreen screen)
    {
        JokeInfo = joke;
        title.text = joke.Title;
        content.text = joke.Text;
        buttonText.text = isSelected ? "Remove" : "Add";
        _screen = screen;
        _isSelected = isSelected;
        button.onClick.AddListener(delegate { HandleClick(); });
    }
    private void HandleClick()
    {
        if (_isSelected)
            _screen.MoveToInventory(this);
        else
            _screen.MoveToSelected(this);
        _isSelected = !_isSelected;
        buttonText.text = _isSelected ? "Remove" : "Add";
    }
}
