using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class buttonShop : MonoBehaviour
{
    public TextMeshProUGUI text1; // Przypisz obiekt TMP Text w inspektorze Unity
    public TextMeshProUGUI text2; // Przypisz obiekt TMP Text w inspektorze Unity
    public TextMeshProUGUI text3; // Przypisz obiekt TMP Text w inspektorze Unity
    public Image icon;
    public string name;
    public int soul;
    public Sprite newSprite; 
    public int price;

    // Start is called before the first frame update
    void Start()
    {
        text1.text = name;
        text2.text = "Potrzebne Gnieciuchy: "+soul;
        text3.text = "Cena: "+price;
        icon.sprite = newSprite;
    }

}
