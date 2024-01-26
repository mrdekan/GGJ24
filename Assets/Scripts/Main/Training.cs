using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Training : MonoBehaviour
{
    private string engInput = "W,A,S,D to move, LMB to interact";
    private string uaInput = "W,A,S,D для переміщення, ЛКМ для взаємодії";
    private string engFirstTask = "Print the list of jokes using the computer";
    private string uaFirstTask = "Роздрукуйте список жартів за допомогою комп'ютера";
    private string engBlankJokes = "It seems empty here, order new jokes from the store";
    private string uaBlankJokes = "Тут ніби порожньо, замовте в магазині нові жарти";
    private string engStore = "In the shop you can buy new jokes and improve your equipment";
    private string uaStore = "В магазині Ви можете купити нові жарти та покращити своє спорядження";
    private string engPrint = "Now add the jokes to the printable list";
    private string uaPrint = "Тепер додайте жарти до списку для друку";
    private string engTakePaper = "Take the printed list and you can go";
    private string uaTakePaper = "Беріть надрукований список і можете вирушати";
    private string engStart = "Go to the middle of the stage and start your performance there";
    private string uaStart = "Вийдіть на середину сцени там розпочніть свій виступ";
    private string engNotLaugh = "They don't think it's funny, try again";
    private string uaNotLaugh = "Здається, що їм не смішно... Спробуйте ще раз";
    private string engTakeMoney = "There aren't many jokes left, it's better to take the money";
    private string uaTakeMoney = "Жартів залишилося небагато, краще забрати кошти";
    private string engFinal = "Now you can upgrade your equipment or order new jokes";
    private string uaFinal = "Тепер можете оновити спорядження чи замовити нові жарти";
    private Monitor _monitor;
    private PCScreen _screen;
    public void Init(Monitor monitor, PCScreen screen)
    {
        if (Game.Instance.Progress.CompletedTraining) return;
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID == 2)
        {
            if (Game.Instance.Progress.Balance <= 1000)
            {
                Subtitle(engInput, uaInput);
                StartCoroutine(First());
                _monitor = monitor;
                if (_monitor != null)
                {
                    _monitor.OnOpen += MonitorOpen;
                    _screen = screen;
                    _screen.OnShopOpen += ShopOpen;
                    Game.Instance.Progress.OnJokesBought += JokesBought;
                    _monitor.OnPrint += Print;
                }
            }
            else
            {
                Subtitle(engFinal, uaFinal);
                Game.Instance.Progress.TrainingComplete();

            }
        }
        else if (sceneID == 1)
        {
            Subtitle(engStart, uaStart);
            Game.Instance.Comedians.SetIsOnTraining();
            Game.Instance.Main.SetIsOnTraining();
            Game.Instance.Comedians.OnJoke += OnJoke;
        }
    }
    private void OnJoke(int jokeNum)
    {
        if (jokeNum == 0)
        {
            Subtitle(engNotLaugh, uaNotLaugh);
        }
        else
        {
            Subtitle(engTakeMoney, uaTakeMoney);
            Game.Instance.Comedians.OnJoke -= OnJoke;
            Game.Instance.Progress.AddMoney(1000);
        }
    }
    private void Print()
    {
        Subtitle(engTakePaper, uaTakePaper);
        _monitor.OnPrint -= Print;
    }
    private void JokesBought()
    {
        Subtitle(engPrint, uaPrint);
        Game.Instance.Progress.OnJokesBought -= JokesBought;
    }
    private void MonitorOpen()
    {
        StopAllCoroutines();
        Subtitle(engBlankJokes, uaBlankJokes);
        _monitor.OnOpen -= MonitorOpen;
    }
    private void ShopOpen()
    {
        Subtitle(engStore, uaStore);
        _screen.OnShopOpen -= ShopOpen;
    }
    private IEnumerator First()
    {
        yield return new WaitForSeconds(5);
        Subtitle(engFirstTask, uaFirstTask);
    }
    private void Subtitle(string eng, string ua)
    {
        Game.Instance.UI.ShowSubtitle(eng, ua);
    }
}
