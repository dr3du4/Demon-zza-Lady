using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beerPromptSystem : MonoBehaviour
{
    // Beers that you can select during the minigame
    public List<beerDispenser> beerSelection;

    // Bool used to trigger the minigame (to be removed)
    public bool startMinigame = false;

    // Keys that can appear as a QTE prompt for the minigame
    public List<KeyCode> qteKeys;

    // Duration of the QTE prompt
    public float timeWindow = 5.0f;

    // Dictionary of inputs and sprites
    [System.Serializable]
    public struct SpritePair
    {
        public KeyCode key;
        public Sprite image;
    }

    public SpritePair[] sprites;


    Dictionary<KeyCode, Sprite> keySprites = new Dictionary<KeyCode, Sprite>();
    Dictionary<beerDispenser, KeyCode> randomKeys = new Dictionary<beerDispenser, KeyCode>();
    bool minigameActive = false;
    float minigameTimer = 0.0f;
    beerSO nextServe;

    private void Start()
    {
        foreach (beerDispenser dispenser in beerSelection)
            randomKeys.Add(dispenser, KeyCode.None);

        foreach(SpritePair pair in sprites)
        {
            keySprites.Add(pair.key, pair.image);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            startMinigame = true;
        }


        if(startMinigame)
        {
            startMinigame = false;
            minigameActive = InitPrompt();
        }

        
        // Minigame logic
        if(minigameActive)
        {
            // Check if the time is up for the prompt
            if (Time.time > minigameTimer)
            {
                minigameActive = false; // Some fail condition
                // Debug.Log("TIME\'S UP");
            }

            // Check if you pressed any of the keys assigned to any of the beer selections 
            foreach (KeyValuePair<beerDispenser, KeyCode> pair in randomKeys)
            {
                if (Input.GetKey(pair.Value))
                {
                    // Assign the next beer to serve
                    nextServe = pair.Key.beer;
                    minigameActive = false;
                    // Disable the prompt
                    foreach(beerDispenser dispenser in beerSelection)
                        dispenser.HaltPrompt();
                    Serve(nextServe);
                }
            }
        }
    }


    // Function used to initialize the minigame values, namely randomize which beer gets what QTE button, and start the QTE cooldown
    bool InitPrompt()
    {
        List<KeyCode> temp = new List<KeyCode>(qteKeys);
        Dictionary<beerDispenser, KeyCode> nextKeys = new Dictionary<beerDispenser, KeyCode>();


        foreach (KeyValuePair<beerDispenser, KeyCode> pair in randomKeys)
        {
            int i = Random.Range(0, temp.Count);
            nextKeys.Add(pair.Key, temp[i]);
            // Debug.Log("Beer: " + pair.Key.beer.beerName + " QTE Prompt: " + temp[i]);
            temp.RemoveAt(i);
        }
        foreach (KeyValuePair<beerDispenser, KeyCode> pair in nextKeys)
        {
            randomKeys[pair.Key] = pair.Value;
            // Show up the button prompt
            pair.Key.ShowPrompt(timeWindow, keySprites[pair.Value]);
        }

        minigameTimer = Time.time + timeWindow;


        return true;
    }

    void Serve(beerSO toServe)
    {
        Debug.Log("Now serving: " + toServe.beerName);
        nextServe = null;
    }
}
