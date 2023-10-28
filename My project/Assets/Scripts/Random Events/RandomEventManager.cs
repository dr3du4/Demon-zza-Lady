using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public List<RandomEvent> randomEvents;
    public float minEventDelay = 10f;
    public float maxEventDelay = 30f;
    public float timeLimit = 5f;


    RandomEvent currentEvent;
    float eventTimer = 0.0f;
    bool eventActive = false;
    float nextEvent = 10f;
    private void Update()
    {
        if(Time.time > nextEvent && !eventActive)
        {
            currentEvent = randomEvents[Random.Range(0, randomEvents.Count)].LaunchEvent(RandomEvent.Difficulty_Level.MEDIUM); // Later on create code that will change difficulty level based on level progress
            eventActive = true;
            eventTimer = Time.time + timeLimit;
        }
        if(eventActive && Time.time > eventTimer)
            currentEvent.CancelEvent();
    }

    public void EventOver(bool success)
    {
        Debug.Log("Event complete?: " + success);
        nextEvent = Time.time + Random.Range(minEventDelay, maxEventDelay);
        Debug.Log("Next event on: " + nextEvent);
        eventActive = false;
    }
}
