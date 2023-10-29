using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    private GameManager manager;
    public beerPromptSystem beerSystem;

    public Button buttonOnOff;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;

    public GameObject shop;

    public GameObject table1;

    public GameObject table2;

    public beerDispenser beer1;

    public beerDispenser beer2;

    public beerDispenser beer3;

    public AudioSource buttonClickSound; // Add an AudioSource component to your GameObject and assign it here in the Inspector.

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void Table1()
    {
        buttonShop shop = button1.GetComponent<buttonShop>();
        if (CheckPrice(shop.price, shop.soul))
        {
            table1.SetActive(true);
            button1.gameObject.SetActive(false);
            PlayButtonClickSound();
        }
        else
            Debug.Log("Not enough cash!");
    }
    public void Table2()
    {
        buttonShop shop = button2.GetComponent<buttonShop>();
        if (CheckPrice(shop.price, shop.soul))
        {
            table2.SetActive(true);
            button2.gameObject.SetActive(false);
            PlayButtonClickSound();
        }
        else
            Debug.Log("Not enough cash!");
    }
    public void Beer1()
    {
        buttonShop shop = button3.GetComponent<buttonShop>();
        if (CheckPrice(shop.price, shop.soul))
        {
            beer1.AddToDispensers(beerSystem);
            button3.gameObject.SetActive(false);
            PlayButtonClickSound();
        }
        else
            Debug.Log("Not enough cash!");
    }
    public void Beer2()
    {
        buttonShop shop = button4.GetComponent<buttonShop>();
        if (CheckPrice(shop.price, shop.soul))
        {
            beer2.AddToDispensers(beerSystem);
            button4.gameObject.SetActive(false);
            PlayButtonClickSound();
        }
        else
            Debug.Log("Not enough cash!");
    }

    public void Beer3()
    {
        buttonShop shop = button6.GetComponent<buttonShop>();
        if (CheckPrice(shop.price, shop.soul))
        {
            beer3.AddToDispensers(beerSystem);
            button6.gameObject.SetActive(false);
            PlayButtonClickSound();
        }
        else
            Debug.Log("Not enough cash!");
    }

    public void Cock()
    {
        buttonShop shop = button5.GetComponent<buttonShop>();
        if (CheckPrice(shop.price, shop.soul))
        {
            button5.gameObject.SetActive(false);
            PlayButtonClickSound();
        }
    }
    
    bool CheckPrice(int money, int gnieciuchy)
    {
        if (manager.GetGnieciuchy() >= gnieciuchy && manager.Pay(money))
            return true;
        else
            return false;
    }

    public void TurnOfON()
    {
        if(shop.active==true)
            shop.SetActive(false);
        else
            shop.SetActive(true);
        PlayButtonClickSound();
    }

        void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play(); // Play the assigned click sound.
        }
    }


}
