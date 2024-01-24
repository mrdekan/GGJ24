using System.Linq;
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
        if (product == Upgrades.Jokes && Game.Instance.Progress.UnlockedUpgrades.Contains(Upgrades.Monitor))
            price *= 0.7f;
        priceOnButton.text = $"${price}k";
        if (product != Upgrades.Jokes && Game.Instance.Progress.UnlockedUpgrades.Contains(product))
        {
            button.interactable = false;
            priceOnButton.text = Game.Instance.Settings.CorrectLanguageString("Already bought", "Вже куплено");
        }
    }
    public void Buy()
    {
        Game.Instance.Progress.Buy(product, price);
        if (product != Upgrades.Jokes)
        {
            button.interactable = false;
            priceOnButton.text = Game.Instance.Settings.CorrectLanguageString("Already bought", "Вже куплено");
        }
    }
}
