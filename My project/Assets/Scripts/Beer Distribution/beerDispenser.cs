using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class beerDispenser : MonoBehaviour
{
    public beerSO beer;
    
    [SerializeField]public TextMeshProUGUI prompt;
    public SpriteRenderer promptSprite;
    public SpriteRenderer beerIcon;
    [SerializeField] public TextMeshProUGUI priceText;


    float promptTimer = 0.0f;

    private void Start()
    {
        HaltPrompt();
        UpdatePrice();
        beerIcon.sprite = beer.beerIcon;
    }

    private void Update()
    {
        if (Time.time > promptTimer /* && prompt.GetComponent<SpriteRenderer>().isVisible */)
            HaltPrompt();        
    }

    public void ShowPrompt(float promptTime, string qteInput)
    {
        promptTimer = Time.time + promptTime;
        prompt.SetText(qteInput);
    }

    public void HaltPrompt()
    {
    }

    public void UpdateBind(string code)
    {
        prompt.SetText(code);
    }

    void UpdatePrice()
    {
        priceText.SetText(beer.beerPrice.ToString());
    }
}
