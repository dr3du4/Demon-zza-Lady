using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charge : RandomEvent
{
    // Default values for the event
    public float minValue = 60f;
    public float maxValue = 80f;
    public float startValue = 50f;
    public float holdIncrement = 60f;
    public float decreaseRate = 30f;
    public float passTime = 0.75f;

    public List<ChargeDifficultySO> difficulties;

    public Slider holdProgressBar;
    public Slider charge;
    public SafeSpotUI safeSpot;
    RandomEventManager manager;


    float value = 0;
    bool eventOver = false;
    float chargingTime = 0f;

    private void Start()
    {
        manager = GetComponentInParent<RandomEventManager>();
    }

    private void Update()
    {
        if (!eventOver)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                value = Mathf.Clamp(value + (holdIncrement * Time.deltaTime), 0, 100);
                // Debug.Log(value);
            }
            if (value >= minValue && value <= maxValue)
            {
                chargingTime += 2 * Time.deltaTime;
                if (chargingTime >= passTime)
                {
                    eventOver = true;
                    CancelEvent();
                }
            }
            if (value > 0)
            {
                value = Mathf.Clamp(value - (Time.deltaTime * decreaseRate), 0, 100);
                if (value == 0)
                {
                    CancelEvent();
                }
            }
            if (chargingTime > 0)
            {
                chargingTime = Mathf.Clamp(chargingTime - Time.deltaTime, 0, 100);
            }

            holdProgressBar.value = value;
            // Debug.Log(chargingTime / passTime);
            charge.value = (chargingTime / passTime) * 100;
        }
    }

    public override bool CancelEvent()
    {
        if (!eventOver)
        {
            eventOver = true;
            manager.EventOver(false);
        }
        else
            manager.EventOver(true);
        gameObject.SetActive(false);
        return true;
    }

    public override RandomEvent LaunchEvent(Difficulty_Level difficulty) // Could add variables to launch event to modify difficulty of the charge
    {

        gameObject.SetActive(true);
        AssignDifficulty(difficulty);

        holdProgressBar.value = value;
        safeSpot.ConfigureSafeSpot(minValue, maxValue);

        eventOver = false;
        return gameObject.GetComponent<RandomEvent>();
    }


    void AssignDifficulty(Difficulty_Level difficulty)
    {
        // Assign values for the minigame from the difficulty SO
        maxValue = difficulties[(int) difficulty].maxValue;
        minValue = difficulties[(int)difficulty].minValue;
        value = difficulties[(int)difficulty].startValue;
        holdIncrement = difficulties[(int)difficulty].holdIncrement;
        decreaseRate = difficulties[(int)difficulty].decreaseRate;
        passTime = difficulties[(int)difficulty].passTime;
    }
}
