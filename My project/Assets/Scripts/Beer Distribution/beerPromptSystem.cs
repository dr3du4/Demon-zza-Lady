using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beerPromptSystem : MonoBehaviour
{
    public beerDispenser tescik; // Test

    // Beers that you can select during the minigame
    public List<beerDispenser> beerSelection;

    // The prompt that appears above a customer's head (their preference)
    public beerPreference preferencePrompt;

    // Bar object
    public BarQueue bar;

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
    Client currentClient;
    GameManager manager;

    // For testing the tip system
    public Client tempClient;

    //Beer prefab
    [SerializeField]private GameObject beer;
    [SerializeField]private Vector3 beerOffSet = new Vector3(0,0,0);

    private void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        foreach (beerDispenser dispenser in beerSelection)
            randomKeys.Add(dispenser, KeyCode.None);

        foreach (SpritePair pair in sprites)
        {
            keySprites.Add(pair.key, pair.image);
        }
    }

    private void Update()
    {
        // Change to be called by Client at bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // InitPrompt(tempClient);
            // AddDispenser(tescik);
        }




        // Minigame logic
        if (minigameActive)
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
                    foreach (beerDispenser dispenser in beerSelection)
                        dispenser.HaltPrompt();

                    // Disable the preference prompt
                    preferencePrompt.HidePreference();

                    // Check if the client wasn't assigned
                    if (currentClient && currentClient.clientPreference.beerPreference == nextServe)
                    {
                        float reactionBonus = (minigameTimer - Time.time) * 2;
                        // Calculating the tip amount
                        Debug.Log("Initial tip: " + (currentClient.beerCount * 10));
                        Debug.Log("Reaction bonus: " + (int)reactionBonus);
                        int t = Mathf.Clamp((currentClient.beerCount * 3) + (int)reactionBonus, 0, (currentClient.beerCount + 1) * 3);
                        Serve(nextServe, t);
                    } else
                        Serve(nextServe, 0);
                }
            }
        }
    }


    // Function used to initialize the minigame values, namely randomize which beer gets what QTE button, and start the QTE cooldown
    public void InitPrompt(Client client = null)
    {
        // Assign the client currently being served
        currentClient = client;
        if (currentClient)
        {
            Debug.Log("Obs�ugujemy: " + currentClient.clientPreference.clientTypeName);
            preferencePrompt.ShowPreference(timeWindow, currentClient.clientPreference.beerPreference);
        }


        // RandomButtonSelect(); // First approach, randomly selected buttons for jug
        StaticButtonSelect(); // Second approach, static buttons for jugs

        // Start the time limit
        minigameTimer = Time.time + timeWindow;

        minigameActive = true;
    }

    void Serve(beerSO toServe, int tip)
    {
        Debug.Log("Now serving: " + toServe.beerName + " Got tip: " + tip);
        manager.AddMoney(toServe.beerPrice, tip);
        // Increase client's beer count (with limit of 4)
        currentClient.beerCount = Mathf.Clamp(currentClient.beerCount + 1, 0, 4);
        //create beer next to currentClient
        //Instantiate(beer, currentClient.transform.position + beerOffSet, Quaternion.Euler(0, 0, 0), currentClient.transform);
        Transform cObj = currentClient.transform.Find("Beer");
        cObj.gameObject.SetActive(true);
        currentClient = null;
        nextServe = null;
        bar.SetClientServed(true);
        
    }

    public void AddDispenser(beerDispenser dispenser)
    {
        beerSelection.Add(dispenser);
        randomKeys.Add(dispenser, KeyCode.None);
    }

    void StaticButtonSelect()
    {
        // Assign the buttons
        int i = 0;
        foreach(beerDispenser beer in beerSelection)
        {
            if (qteKeys.Count > i)
            {
                randomKeys[beer] = qteKeys[i];
                i++;
            }
        }

        //  Show the prompt
        foreach (KeyValuePair<beerDispenser, KeyCode> pair in randomKeys)
        {
            // Show up the button prompt
            pair.Key.ShowPrompt(timeWindow, keySprites[pair.Value]);
        }
    }


    void RandomButtonSelect()
    {
        // Create helper objects for randomly selecting the qte buttons
        List<KeyCode> temp = new List<KeyCode>(qteKeys);
        Dictionary<beerDispenser, KeyCode> nextKeys = new Dictionary<beerDispenser, KeyCode>();


        // Randomly select qte buttons for the prompt
        foreach (KeyValuePair<beerDispenser, KeyCode> pair in randomKeys)
        {
            int i = Random.Range(0, temp.Count);
            nextKeys.Add(pair.Key, temp[i]);
            // Debug.Log("Beer: " + pair.Key.beer.beerName + " QTE Prompt: " + temp[i]);
            temp.RemoveAt(i);
        }
        // Change key assignment for the beer dispensers and show the prompt
        foreach (KeyValuePair<beerDispenser, KeyCode> pair in nextKeys)
        {
            randomKeys[pair.Key] = pair.Value;
            // Show up the button prompt
            pair.Key.ShowPrompt(timeWindow, keySprites[pair.Value]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered a trigger");
        if (collision.gameObject.GetComponent<Client>())
        {
            if (collision.gameObject.GetComponent<Client>().readyToDrink)
            {
                collision.gameObject.GetComponent<Client>().readyToDrink = false;
                InitPrompt(collision.gameObject.GetComponent<Client>());
            }
        }
    }
}
