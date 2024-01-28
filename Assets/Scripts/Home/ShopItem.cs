using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private float price = 1;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI priceOnButton;
    [SerializeField] private Button button;
    [SerializeField] private string engTitle;
    [SerializeField] private string uaTitle;
    [SerializeField] private string engDescription;
    [SerializeField] private string uaDescription;
    [SerializeField] private Sprite icon;
    [SerializeField] private Image image;
    [SerializeField] private Upgrades product;
    private void Start()
    {
        title.text = Game.Instance.Settings.CorrectLanguageString(engTitle, uaTitle);
        description.text = Game.Instance.Settings.CorrectLanguageString(engDescription, uaDescription);
        if (product == Upgrades.Jokes && Game.Instance.Progress.Has(Upgrades.Monitor))
            price *= 0.7f;
        else if (product != Upgrades.Jokes && Game.Instance.Progress.Has(Upgrades.Headphones))
            price *= 0.9f;
        priceOnButton.text = $"${price}k";
        image.sprite = icon;
        if (product != Upgrades.Jokes && Game.Instance.Progress.Has(product))
        {
            button.interactable = false;
            priceOnButton.text = Game.Instance.Settings.CorrectLanguageString("Already bought", "Вже куплено");
        }
        if (product == Upgrades.Jokes)
        {
            UpdateCanBuyJokes();
        }
    }
    private void UpdateCanBuyJokes()
    {
        if (Game.Instance.Jokes.UserJokes.Count >= GlobalJokesList.TotalJokesCount())
        {
            button.interactable = false;
            priceOnButton.text = Game.Instance.Settings.CorrectLanguageString("The jokes are over", "Жарти скінчились");
        }
    }
    public void Buy()
    {
        if (Game.Instance.Progress.Balance < price * 1000) return;
        Game.Instance.Progress.Buy(product, price);
        if (product != Upgrades.Jokes)
        {
            button.interactable = false;
            priceOnButton.text = Game.Instance.Settings.CorrectLanguageString("Already bought", "Вже куплено");
        }
        else
        {
            UpdateCanBuyJokes();
        }
    }
}
