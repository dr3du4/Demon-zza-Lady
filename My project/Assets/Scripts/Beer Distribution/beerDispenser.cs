using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class beerDispenser : MonoBehaviour
{
    public beerSO beer;
    
    public GameObject prompt;
    public SpriteRenderer promptSprite;
    public SpriteRenderer beerIcon;


    float promptTimer = 0.0f;

    private void Start()
    {
        HaltPrompt();
        beerIcon.sprite = beer.beerIcon;
    }

    private void Update()
    {
        if (Time.time > promptTimer /* && prompt.GetComponent<SpriteRenderer>().isVisible */)
            HaltPrompt();        
    }

    public void ShowPrompt(float promptTime, Sprite qteInput)
    {
        promptTimer = Time.time + promptTime;
        prompt.GetComponent<SpriteRenderer>().enabled = true;
        promptSprite.sprite = qteInput;
    }

    public void HaltPrompt()
    {
        prompt.GetComponent<SpriteRenderer>().enabled = false;
        promptSprite.sprite = null;
    }

}
