using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<int> gnieciuchTresholds = new List<int>() { 10, 20, 30, 40, 60 };
    [SerializeField] private Tutorial tutorial;
    [SerializeField] public TextMeshProUGUI moneyText;
    [SerializeField] public TextMeshProUGUI soulsText;

    int money = 0;
    int souls = 0;
    int gnieciuchy = 0;

    public int sprzedanepiwa;

    Gnieciuch g;
    

    private void Start()
    {
        g = GetComponent<Gnieciuch>();
    }

    public GameManager(int _money, int _souls, int _gnieciuchy, int _sprzedanepiwa) 
    {
        money = _money;
        souls = _souls;
        gnieciuchy = _gnieciuchy;
        sprzedanepiwa = _sprzedanepiwa;
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
        if (Input.GetKeyDown(KeyCode.M))
            MoneyCheat();

        if (Input.GetKeyDown(KeyCode.K))
            SoulCheat();

        if (souls >= gnieciuchTresholds[Mathf.Clamp(gnieciuchy, 0, gnieciuchTresholds.Count - 1)] && souls <= gnieciuchTresholds[gnieciuchTresholds.Count-1])
        {
            gnieciuchy++;
            g.gnieciuchy();
            if (gnieciuchy == 1){    
                tutorial.ActivateTutorial(2);
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
            //souls += 5;

        UpdateUI();
    }

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
        souls+=amount;
        // Again as above...
    }

    public int GetGnieciuchy() { return gnieciuchy; }
    public int GetMoney() { return money; }
    public int GetSouls() { return souls; }

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
}
