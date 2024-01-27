using Assets.Scripts.Settings;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalJokesList
{
    private static int legendChance = 3;
    private static int increasedLegendChance = 10;
    private static int epicChance = 20;
    private static int rareChance = 30;
    private static int newJokesCount = 5;

    private static readonly List<Joke> _defJokes = new()
    {
        new(JokeRarity.Default,"-What's brown and sticky? -A stick!","Sticky stick!"),
        new(JokeRarity.Default,"-What does James Bond do before bed? -What? -He goes undercover!","James Bond before bed"),
        new(JokeRarity.Default,"I'd tell a joke about pizza as well, but it's a little cheesy.","Cheesy pizza"),
        new(JokeRarity.Default,"To whoever stole my antidepressants, I hope you're happy.","Antidepressants"),
        new(JokeRarity.Default,"-What do you call an angry carrot? -A steamed veggie.","Angry carrot"),
        new(JokeRarity.Default,"-Where do polar bears keep their money? -In a snowbank.","Snowbank"),
        new(JokeRarity.Default,"Time flies like an arrow. Fruit flies like a banana.","Time flies like an arrow"),
        new(JokeRarity.Default,"-What do you call a fish without an eye? -Fsh.","Fish without an eye"),
    };
    private static readonly List<Joke> _rareJokes = new()
    {
        new(JokeRarity.Rare,"Yes, I've decided to sell my vacuum cleaner, because it was just gathering dust!","Vacuum cleaner and dust"),
        new(JokeRarity.Rare,"-uh Here's a question, why can't you hear a psychiatrist using the bathroom? -Why not.. -Because the 'P' is Silent!","'P' is Silent!"),
        new(JokeRarity.Rare,"-What would bears be without bees? -Ears.","Bears be without bees"),
        new(JokeRarity.Rare,"-Why do cows wear bells? -Because their horns don’t work.","Cows wear bells"),
        new(JokeRarity.Rare,"-Why was the fish’s grades bad? -They were below sea level.","Fish’s grades"),
    };
    private static readonly List<Joke> _epicJokes = new()
    {
        new(JokeRarity.Epic,"A police officer pulled me over and said \"papers\". I said scissors and drove off","Police officer and papers"),
        new(JokeRarity.Epic,"-Can February March? -I don't know, can it? -No, but April May! Ahahahaha!","Can February March?"),
        new(JokeRarity.Epic,"-What did the triangle say to the circle? -You’re pointless.","Pointless circle"),
        new(JokeRarity.Epic,"-What did one toilet say to another? -You look flushed.","Flushed toilet"),
    };
    private static readonly List<Joke> _legendJokes = new()
    {
        new(JokeRarity.Legendary,"-Did you hear about the guy who invented the knock-knock joke? -No, what about him? -He won the NO-BELL prize! Ahahahaha!","Knock-knock joke"),
    };
    private static readonly List<Joke> _uaDefJokes = new()
    {
        new(JokeRarity.Default,"– Отче, чоловік зраджує. Порадьте, що робити? – Скропи сковорідку святою водою і наверни його на шлях праведний.","Поради від отця"),
        new(JokeRarity.Default,"Чим жінка схожа на автомобіль? Чим вона старша, тим більше грошей потрібно на ремонт!","Про жінок і автомобілі"),
        new(JokeRarity.Default,"Що спільне між міні-спідницею і паранджею? І те, й інше допомагає негарним дівчатам ховати своє обличчя!","Міні-спідниця і паранджа"),
        new(JokeRarity.Default,"Директор турбази – це людина, яку поважають тільки з травня по вересень.","Директор турбази"),
        new(JokeRarity.Default,"Гей! Руки за голову, ноги на ширині плечей! Це пограбування?! Ні – урок фізкультури!","Урок фізкультури чи пограбування?"),
        new(JokeRarity.Default,"Дружина – дивовижна людина! Вона знаходить речі там, де їх РЕАЛЬНО НЕ БУЛО, коли шукав я!","Дивовижна здатність дружини"),
        new(JokeRarity.Default,"Молитва для матусь школярів: Господи, дай мені сили зробити уроки з дитиною! І при цьому залишитися люблячою матусею, культурною жінкою і адекватною сусідкою.","Молитва для матусь школярів"),
        new(JokeRarity.Default,"За всю відпустку дружина отримала з дому від чоловіка тільки 1 смску: “Де штопор?”.","За всю відпустку"),
    };
    private static readonly List<Joke> _uaRareJokes = new()
    {
        new(JokeRarity.Rare,"Купив курс, по якому англійську мові вивчають уві сні. Але дружина вигнала викладачку із нашого ліжка.","Уроки англійської уві сні"),
        new(JokeRarity.Rare,"Ніколи не влаштовуйте істерику, лежачи на спині. Сльози затікають в вуха і стає лоскотно і смішно. Я так вже три істерики зірвала.","Істерики"),
        new(JokeRarity.Rare,"Дорога, давай обнулимо наші відносини! Забудемо всі образи і минуле? Забудемо, що знаємо один одного.","Обнулення відносин"),
        new(JokeRarity.Rare,"З одного боку, в гості без пляшки не підеш. А з іншого – якщо в тебе є пляшка, та на холєру в гості пертись?","Гості з пляшкою"),
        new(JokeRarity.Rare,"Алло, це лінія допомоги алкоголікам? Так. Підкажіть як зробити мохіто!","Лінія допомоги алкоголікам"),
        new(JokeRarity.Rare,"Різдво 2022: Христос родився! Маю три вакцини, сертифікат вакцинації, негативний тест, оброблений антисептиком! Колядувати можна?","Різдво 2022"),
        new(JokeRarity.Rare,"Мам, а це правда, що курячий супчик допомагає при простуді? Так, доню. А чому? Тому що в курочці багато антибіотиків.","Курячий супчик"),
        new(JokeRarity.Rare,"-Привіт, як справи? -Так, ось, у відпустку збираюся. -З дружиною чи на відпочинок?","Відпустка та відпочинок"),
        new(JokeRarity.Rare,"Подружня пара гуляє по парку. Чоловік постійно озирається на дівчат – трохи слина з рота не капає. Дружина довго терпить, а потім каже: Нагулюй апетит, дорогий, нагулюй… Жерти все одно вдома будеш.","Захоплення чоловіка"),
    };
    private static readonly List<Joke> _uaEpicJokes = new()
    {
        new(JokeRarity.Epic,"Є люди, які несуть в собі щастя. Коли ці люди поруч, все ніби стає яскравим і барвистим. Але моя дружина їх називає алкашами!","Щастя в алкашах"),
        new(JokeRarity.Epic,"Іване, ти памятаєш що було 35 років тому? - Звісно ж! Чорнобиль вибухнув! - Дурнику, ми одружилися! - Так, біда не ходить одна…","Чорнобильське весілля"),
        new(JokeRarity.Epic,"Лікар, розглядаючи історію хвороби, каже чоловікові: Ваша теща абсолютно здорова. Це підтверджують всі аналізи і рентгенівські знімки. А чи не можна, лікарю, щоб я був зовсім спокійний, зробити розтин?","Здоров'я тещі"),
        new(JokeRarity.Epic,"Назвав тещу «дурою» – дружина замовкла на тиждень. Ще раз назвав дурою -замовкла ще на тиждень. Ось вона, кнопка «MUTE»‼!","Назвав тещу дурою"),
        new(JokeRarity.Epic,"Як добре було раніше, ніяких проблем вибору: яка ковбаса є в магазині, ту і купуєш, який інститут поблизу – в той і вступаєш, яка дівчина дала, на тій і одружуєшся!","Вибір ковбаси, інституту і дружини"),
        new(JokeRarity.Epic,"В дівчат місячні з-за того, що вони цілий місяць п’ють кров у чоловіків, а потім її вже нікуди дівати…","Місячні у дівчат"),
        new(JokeRarity.Epic,"Добрий день, куме, що п’єте? Українське мохіто. Ром та м’ята? Та ні, самогонка і петрушка.","Українське мохіто"),
        new(JokeRarity.Epic,"Кажуть, що у геніїв в будинку має бути безлад. Дивлюсь на свою дитину і гордість розпирає! Енштейна виховую!..","Безлад у геніїв"),
        new(JokeRarity.Epic,"Кума, а ти знаєш, що я коханка твого чоловіка? А мій брехун сказав, що вона молода і красива!","Кума і коханка"),
        new(JokeRarity.Epic,"Записка чоловікові: Пішла, куда послав. Веду себе, як ти назвав. І чому я тебе раніше не слухала?!","Записка чоловікові"),
        new(JokeRarity.Epic,"Чоловік поїхав у відпустку на курорт. Пише дружині телеграму: “Продай телевізор, вишли $ 500”. Трохи пізніше дружина теж їде у відпустку і теж пише чоловікові телеграму: “Висилаю $ 500, купи телевізор”.","Лист з відпустки"),
        new(JokeRarity.Epic,"Жінка в аптеці: Що для чоловіка краще: корвалол чи валідол? А який діагноз? Туфлі купила собі за 5 тисяч. Тоді візьміть ще й тональний крем від синців. То для себе!","В аптеці"),
    };
    private static readonly List<Joke> _uaLegendJokes = new()
    {
        new(JokeRarity.Legendary,"Допоможіть знайти чоловіка! Дуже заляканий і збентежений… Волосся сиве, одягнений в сині труси, сірий пуховик і чорну шкарпетку… Коротше, в чому встиг, курvа, в тому і втік!","Пошук чоловіка"),
        new(JokeRarity.Legendary,"Чим відрізняється нещасний випадок від нещастя? Якщо ваша теща впала в річку, то це – нещасний випадок. А якщо її врятували, то це – нещастя.","Нещасливий випадок"),
        new(JokeRarity.Legendary,"Лікар-психоаналітик пацієнту:\n— Після вашого обстеження у мене для вас дві новини – гарна та погана. Погана - поза всяким сумнівом, ви прихований гомосексуаліст. Пацієнт:\n— Яка ж може бути гарна? Лікар підсідає ближче: - Ви симпатичний!","Візит до лікаря-психоаналітика"),
        new(JokeRarity.Legendary,"Французький поцілунок - це, коли з язиком, а циганський - це, коли пропадають золоті зуби...","Поцілунки"),
    };
    private static List<Joke> GetCorrectLanguageList(JokeRarity rarity)
    {
        if (Game.Instance.Settings.Language == Languages.En)
        {
            switch (rarity)
            {
                case JokeRarity.Default: return _defJokes;
                case JokeRarity.Rare: return _rareJokes;
                case JokeRarity.Epic: return _epicJokes;
                case JokeRarity.Legendary: return _legendJokes;
            }
        }
        else
        {
            switch (rarity)
            {
                case JokeRarity.Default: return _uaDefJokes;
                case JokeRarity.Rare: return _uaRareJokes;
                case JokeRarity.Epic: return _uaEpicJokes;
                case JokeRarity.Legendary: return _uaLegendJokes;
            }
        }
        return new List<Joke>();
    }
    public static List<Joke> GenerateNewJokes(List<Joke> current, bool increaseLegendChance, bool extraJoke)
    {
        var def = RemoveListFromList(GetCorrectLanguageList(JokeRarity.Default), current);
        var rare = RemoveListFromList(GetCorrectLanguageList(JokeRarity.Rare), current);
        var epic = RemoveListFromList(GetCorrectLanguageList(JokeRarity.Epic), current);
        var leg = RemoveListFromList(GetCorrectLanguageList(JokeRarity.Legendary), current);
        int jokesCnt = newJokesCount;
        if (extraJoke) jokesCnt++;
        List<Joke> jokes = new List<Joke>();
        int tempLegChance = increaseLegendChance ? increasedLegendChance : legendChance;

        for (int i = 0; i < jokesCnt; i++)
        {
            int rand = Random.Range(0, 100);

            if (rand < tempLegChance)
            {
                Joke jokeToAdd = PopRandom(leg) ?? PopRandom(epic) ?? PopRandom(rare) ?? PopRandom(def);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
            else if (rand < tempLegChance + epicChance)
            {
                Joke jokeToAdd = PopRandom(epic) ?? PopRandom(rare) ?? PopRandom(def) ?? PopRandom(leg);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
            else if (rand < tempLegChance + epicChance + rareChance)
            {
                Joke jokeToAdd = PopRandom(rare) ?? PopRandom(def) ?? PopRandom(epic) ?? PopRandom(leg);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
            else
            {
                Joke jokeToAdd = PopRandom(def) ?? PopRandom(rare) ?? PopRandom(epic) ?? PopRandom(leg);
                if (jokeToAdd != null)
                    jokes.Add(jokeToAdd);
                else
                    break;
            }
        }
        return jokes;
    }
    private static Joke PopRandom(List<Joke> jokes)
    {
        if (jokes == null || jokes.Count == 0)
            return null;

        int index = Random.Range(0, jokes.Count);
        Joke poppedJoke = jokes[index];
        jokes.RemoveAt(index);
        return poppedJoke;
    }
    private static List<Joke> RemoveListFromList(List<Joke> a, List<Joke> b)
    {
        List<Joke> res = new List<Joke>(a);
        foreach (Joke joke in b)
            res.Remove(joke);
        return res;
    }
}
