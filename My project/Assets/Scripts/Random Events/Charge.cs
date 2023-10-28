using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charge : RandomEvent
{
    public float minValue = 60f;
    public float maxValue = 80f;
    public float startValue = 50f;
    public float holdIncrement = 5f;
    public float decreaseRate = 5f;
    public Slider holdProgressBar;
    public RandomEventManager manager;
    public float passTime = 3f;


    float value = 0;
    bool eventOver = false;
    float chargingTime = 0f;

    

    private void Update()
    {
        if (!eventOver)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                value = Mathf.Clamp(value + (holdIncrement * Time.deltaTime), 0, 100);
                if (value >= minValue && value <= maxValue)
                {
                    chargingTime += Time.deltaTime;
                    if(chargingTime >= passTime)
                    {
                        eventOver = true;
                        Debug.Log("SUCCESS!");
                        CancelEvent();
                    }
                }else
                {
                    chargingTime = 0f;
                }
            }
            if (value > 0)
            {
                value = Mathf.Clamp(value - (Time.deltaTime * decreaseRate), 0, 100);
                if (value == 0)
                {
                    eventOver = true;
                    Debug.Log("FAILURE");
                    CancelEvent();
                }
            }

            holdProgressBar.value = value;
        }
    }

    public override bool CancelEvent()
    {
        eventOver = true;
        gameObject.SetActive(false);
        manager.EventOver();
        return true;
    }

    public override bool LaunchEvent() // Could add variables to launch event to modify difficulty of the charge
    {

        gameObject.SetActive(true);
        holdProgressBar.maxValue = 100;
        value = startValue;
        holdProgressBar.value = value;

        eventOver = false;
        return true;
    }

}
