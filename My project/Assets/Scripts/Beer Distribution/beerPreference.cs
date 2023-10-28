using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beerPreference : MonoBehaviour
{
    public SpriteRenderer beerIcon;
    float cooldown = 0.0f;

    private void Start()
    {
        HidePreference();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && Time.time > cooldown)
            HidePreference();

    }

    public void ShowPreference(float timeLimit, beerSO _beer)
    {
        cooldown = Time.time + timeLimit;
        gameObject.SetActive(true);
        beerIcon.sprite = _beer.beerIcon;
    }

    public void HidePreference()
    {
        gameObject.SetActive(false);
    }
}
