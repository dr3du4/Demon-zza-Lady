using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public List<RandomEvent> randomEvents;
    public float minEventDelay = 10f;
    public float maxEventDelay = 30f;

    bool eventActive = false;
    float nextEvent = 10f;
    private void Update()
    {
        if(Time.time > nextEvent && !eventActive)
        {
            randomEvents[Random.Range(0, randomEvents.Count)].LaunchEvent();
            eventActive = true;
        }
    }

    public void EventOver()
    {
        nextEvent = Time.time + Random.Range(minEventDelay, maxEventDelay);
        Debug.Log("Next event on: " + nextEvent);
        eventActive = false;
    }
}
