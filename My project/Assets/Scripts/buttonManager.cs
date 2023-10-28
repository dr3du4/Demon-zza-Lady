using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    public Button buttonOnOff;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    public GameObject shop;

    public GameObject table1;

    public GameObject table2;

    public GameObject beer1;

    public GameObject beer2;

    public void Table1()
    {
        table1.SetActive(true);
        button1.gameObject.SetActive(false);
    }
    public void Table2()
    {
        table2.SetActive(true);
        button2.gameObject.SetActive(false);
    }
    public void Beer1()
    {
        beer1.SetActive(true);
        button3.gameObject.SetActive(false);
    }
    public void Beer2()
    {
        beer2.SetActive(true);
        button4.gameObject.SetActive(false);
    }
    public void Cock()
    {
        
        button5.gameObject.SetActive(false);
    }
    

    public void TurnOfON()
    {
        if(shop.active==true)
            shop.SetActive(false);
        else
            shop.SetActive(true);
    }
}
