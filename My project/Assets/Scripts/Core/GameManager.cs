using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<int> gnieciuchTresholds = new List<int>() { 10, 20, 30, 40, 60 };

    int money;
    int souls;
    int gnieciuchy;

    gnieciuch g;

    private void Start()
    {
        g = GetComponent<gnieciuch>();
    }

    public GameManager(int _money, int _souls, int _gnieciuchy) 
    {
        money = _money;
        souls = _souls;
        gnieciuchy = _gnieciuchy;
    }

    private void Update()
    {
        if (souls >= gnieciuchTresholds[Mathf.Clamp(gnieciuchy, 0, gnieciuchTresholds.Count - 1)] && souls <= gnieciuchTresholds[gnieciuchTresholds.Count-1])
        {
            gnieciuchy++;
            g.gnieciuchy();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            souls += 5;
    }

    public bool Pay(int price)
    {
        if (money < price)
            return false;

        money -= price;
        return true;
        // Some form of visual/sound effect there?
    }
    public void AddMoney(int amount, int tip)
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

}
