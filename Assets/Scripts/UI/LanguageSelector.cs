using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private Color defaultColor = Color.gray;
    [SerializeField] private Image border;
    [SerializeField] private Button button;
    [SerializeField] private OnFirstLoad manager;
    private void Start()
    {
        button.onClick.AddListener(delegate { Handler(); });
    }
    private void Handler()
    {
        SetSelect(true);
        manager.SetSelected(this);
    }
    public void SetSelect(bool state)
    {
        border.color = state ? selectedColor : defaultColor;
    }
}
