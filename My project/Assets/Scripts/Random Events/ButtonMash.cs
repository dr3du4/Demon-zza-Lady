using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMash : RandomEvent
{

    public float startValue = 30f;
    public float goalValue = 100f;
    public float mashIncrement = 5f;
    public float decreaseRate = 5f;
    public Slider mashProgressBar;
    public RandomEventManager manager;


    float value = 0;
    bool eventOver = false;

    

    private void Update()
    {
        if (!eventOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                value = Mathf.Clamp(value + mashIncrement, 0, goalValue);
                if (value == goalValue)
                {
                    eventOver = true;
                    Debug.Log("SUCCESS!");
                    CancelEvent();
                }
                // Debug.Log(value);
            }
            if (value > 0)
            {
                value = Mathf.Clamp(value - (Time.deltaTime * decreaseRate), 0, goalValue);
                if (value == 0)
                {
                    eventOver = true;
                    Debug.Log("FAILURE");
                    CancelEvent();
                }
            }

            mashProgressBar.value = value;
        }
    }

    public override bool CancelEvent()
    {
        eventOver = true;
        gameObject.SetActive(false);
        manager.EventOver();
        return true;
    }

    public override bool LaunchEvent() // Could add variables to launch event to modify difficulty of the button mash
    {

        gameObject.SetActive(true);
        mashProgressBar.maxValue = goalValue;
        value = startValue;
        mashProgressBar.value = value;

        eventOver = false;
        return true;
    }

    
}
