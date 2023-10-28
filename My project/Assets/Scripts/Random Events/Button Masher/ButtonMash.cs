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

    public List<MasherDifficultySO> difficulties;

    public Slider mashProgressBar;
    RandomEventManager manager;


    float value = 0;
    bool eventOver = false;

    private void Start()
    {
        manager = GetComponentInParent<RandomEventManager>();
    }

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
                    CancelEvent();
                }
                // Debug.Log(value);
            }
            if (value > 0)
            {
                value = Mathf.Clamp(value - (Time.deltaTime * decreaseRate), 0, goalValue);
                if (value == 0)
                {
                    CancelEvent();
                }
            }

            mashProgressBar.value = value;
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
        {
            manager.EventOver(true);
        }
        gameObject.SetActive(false);
        return true;
    }

    public override RandomEvent LaunchEvent(Difficulty_Level difficulty) // Could add variables to launch event to modify difficulty of the button mash
    {
        AssignDifficulty(difficulty);
            
        gameObject.SetActive(true);
        mashProgressBar.maxValue = goalValue;
        value = startValue;
        mashProgressBar.value = value;

        eventOver = false;
        return gameObject.GetComponent<RandomEvent>();
    }

    void AssignDifficulty(Difficulty_Level difficulty)
    {
        startValue = difficulties[(int) difficulty].startValue;
        goalValue = difficulties[(int) difficulty].goalValue;
        mashIncrement = difficulties[(int) difficulty].mashIncrement;
        decreaseRate = difficulties[(int)difficulty].decreaseRate;
    }

}
