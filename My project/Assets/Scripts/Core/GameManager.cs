using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public BarQueue bar;
    public ClientsSpawn spawner;
    public float dayLength = 180f;
    float dayTimer = 0.0f;

    public List<int> gnieciuchTresholds = new List<int>() { 10, 20, 30, 40, 50 };
    private Tutorial tutorial;
    [SerializeField] public TextMeshProUGUI moneyText;
    [SerializeField] public TextMeshProUGUI soulsText;

    public GameObject mainMenu;
    public GameObject credits;
    public GameObject tuts;

    int money = 0;
    int souls = 0;
    int gnieciuchy = 0;

    public int sprzedanepiwa;
    public int klienciCoS;

    Gnieciuch g;
    

    private void Start()
    {
        g = GetComponent<Gnieciuch>();
        // Debug.Log(mainMenu);
        // Debug.Log(credits);
        // Debug.Log(tuts);
        dayTimer = Time.time + dayLength;
        tutorial = GameObject.FindWithTag("Tutorial").GetComponent<Tutorial>();
        tutorial.gameObject.SetActive(false);
        SetVolumeInit();
    }

    public GameManager(int _money, int _souls, int _gnieciuchy, int _sprzedanepiwa) 
    {
        money = _money;
        souls = _souls;
        gnieciuchy = _gnieciuchy;
        sprzedanepiwa = _sprzedanepiwa;
        klienciCoS = 0;
    }


    void MoneyCheat()
    {
        AddMoney(100);
    }

    void SoulCheat()
    {
        AddSoul(5);
    }

    private void Update()
    {
        // CHEATS
        if (Input.GetKeyDown(KeyCode.M))
            MoneyCheat();

        if (Input.GetKeyDown(KeyCode.K))
            SoulCheat();
        // CHEATS

        if (Time.time > dayTimer)
        {
            Debug.Log(Time.time);
            Debug.Log(dayTimer);
            // Restart day
            Debug.Log("new day!");
            dayTimer = Time.time + dayLength;
            NextDay();
        }
        else if (Time.time + 4f >= dayTimer) // 4 seconds before the end of the day spawner should stop spawning (so we don't spawn a new Client when the end of the day happens)
            spawner.NextDay();


        if (souls >= gnieciuchTresholds[Mathf.Clamp(gnieciuchy, 0, gnieciuchTresholds.Count - 1)] && souls <= gnieciuchTresholds[gnieciuchTresholds.Count-1])
        {
            gnieciuchy++;
            g.gnieciuchy();
            if (gnieciuchy == 1){    
                tutorial.ActivateTutorial(2);
            }
        }


        UpdateUI();
    }

    public Tutorial GetTutorial() { return tutorial;}
    
    public bool Pay(int price)
    {
        if (money < price)
            return false;

        money -= price;
        return true;
        // Some form of visual/sound effect there?
    }
    public void AddMoney(int amount, int tip = 0)
    {
        money += amount + tip;

        // Again do some cool audiovisual effect when this happens
    }

    public void AddSoul(int amount = 1)
    {
        


        if(souls < gnieciuchTresholds[gnieciuchTresholds.Count-1])
            souls += amount;
        // Again as above...
    }

    public int GetGnieciuchy() { return gnieciuchy; }
    public int GetMoney() { return money; }
    public int GetSouls() { return souls; }
    public int GetBeers() { return sprzedanepiwa; }

    public void AddSoldBeer()
    {
        if (sprzedanepiwa == 0){    
            tutorial.ActivateTutorial(0);
        }
        sprzedanepiwa++;
    }
    void UpdateUI()
    {
        moneyText.SetText(money.ToString());
        soulsText.SetText(souls.ToString());
    }

    void NextDay()
    {
        Debug.Log("Next day!");
        // spawner.NextDay();
        bar.NextDay();
        //TEMP-> MOVE THAT TO GAME MANAGER ~Bodzio
        GameObject.FindGameObjectWithTag("beerPromptSystem").GetComponent<beerPromptSystem>().NewDay();

        foreach(GameObject table in GameObject.FindGameObjectsWithTag("chair"))
        {
            // Debug.Log(table.name);
            if(table.activeInHierarchy && table.TryGetComponent<TableClients>(out TableClients tb))
                tb.RestartTable();
        }
    }

    void SetVolumeInit()
    {
        float _volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        foreach(AudioSource s in FindObjectsOfType(typeof(AudioSource)))
        {
            s.volume = _volume;
        }
    }


    public void Pause()
    {
        Time.timeScale = 0;
        mainMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CreditsOpen()
    {
        credits.SetActive(true);
    }

    public void CreditsClose()
    {
        credits.SetActive(false);
    }

    public void Tut(int i)
    {
        tutorial.ActivateTutorial(i);
    }

    public void TutsOpen()
    {
        tuts.SetActive(true);
    }

    public void TutsClose()
    {
        tuts.SetActive(false);
    }
}
