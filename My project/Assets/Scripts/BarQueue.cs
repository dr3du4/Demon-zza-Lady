using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarQueue : MonoBehaviour
{
    // private List<GameObject> objectToSpawn ;
    [SerializeField]private List<Client> queue = new List<Client>();
	[SerializeField]private Vector3 offSetFirst = new Vector3(0f,-0.25f,0f);
	[SerializeField]private Vector3 offSet = new Vector3(0f,-0.75f,0f);
	public float dayTime = 180f;
    public int maxInLine = 15;
    private int InLine = 0;
	private Client firstClient = null;
	private float timerClient;
	private Vector3 first;


	bool clientServed = false;
	
	
	//added Kasia
	public TableClients[] allTables; // Reference to all three TableClients
    private List<Client> allClients = new List<Client>(); // List to hold all clients

	private bool soundPlayed = false;
	private float timer = 0f;
	public AudioClip yourSoundClip; // Assign your sound clip in the Inspector
    private AudioSource audioSource;
	public TMP_Text day;

	public AudioSource[] backgroundAudioSources; // Reference to multiple background audio sources
    public float muteDuration = 2.0f; // Duration to mute the background sounds

    private bool isMuting = false;

	private int eventCounter = 1; // Counter to track the 3-minute event occurrences
    //public Text eventCounterText; // Reference to the Text UI element

	private Tutorial tutorial;
	//


	public void SetClientServed(bool b) { clientServed = b; }
	public bool GetClientServed() { return clientServed; }

	private void Start()
	{
		tutorial = GameObject.FindWithTag("GameController").GetComponent<GameManager>().GetTutorial();
		day.text = "Dzien: "+ eventCounter.ToString();
		first = transform.position += offSetFirst;
		//added KASIA 3rano
		audioSource = GetComponent<AudioSource>();
		 if (allTables.Length > 0)
        {
            // Loop through all table clients and add their clients to the allClients list
            foreach (TableClients table in allTables)
            {
                allClients.AddRange(table.clients);
            }
        }
		timer = 0f;
	}

	private void Update() {
			if(firstClient != null && clientServed) {
				if (Vector3.Distance(firstClient.transform.position,first) < 0.1f) {
					firstClient.StartTimer(this);
					//cool
					firstClient.ShowIndicatorSquare(true); // Show indicator square for the first client
					firstClient = null;
					SetClientServed(false);
				}
			}
		int i = 0;
		foreach(Client c in queue)
        {
			if(c.TryGetComponent(out SpriteRenderer renderer))
				renderer.sortingOrder = i;
			i++;
        }

		// Timer logic  added KASIA o 3:32 niedziela

        /*    timer += Time.deltaTime;
            if (timer >= dayTime) // 180 seconds = 3 minutes
            {
				NextDay();
			}*/
		
	}

    private void SetClientsOnPos(){
		if (InLine == 0) return;
		for (int i=0; i<InLine; i++) {
			if(!queue[i].dayOver)
				StartCoroutine(queue[i].MoveTo(first + (i * offSet)));
		}
	}


    public void RemoveFirstClient() {
		if (InLine == 0) return;
		queue.RemoveAt(0);
		InLine--;
		firstClient = null;
		if (InLine == 0) return;
		SetClientsOnPos();
		firstClient = queue[0];
	}

	public void AddClient(Client c) {
		if (InLine >= maxInLine) {
			if(!c.dayOver)
				StartCoroutine(c.Die());
		}
		else{
			if(!c.dayOver)
				StartCoroutine(c.MoveTo(first + (InLine * offSet)));
			queue.Add(c);
			InLine++;
		}
		if (InLine == 1) firstClient = queue[0];
	}

	public void ForceClient(Client c) {
		if (InLine >= maxInLine) {
			int i = Random.Range(3,InLine);
			Client outC = queue[i];
			queue.RemoveAt(i);
			queue.Insert(i,c);
			if(!c.dayOver)
				StartCoroutine(outC.Die());
			SetClientsOnPos();

		}
		else {
			AddClient(c);
		}
	}


	
    private IEnumerator UnmuteBackgroundSounds()
    {
        yield return new WaitForSeconds(muteDuration);

        // Unmute or restore the volume of the background audio sources after the mute duration
        foreach (AudioSource audioSource in backgroundAudioSources)
        {
            audioSource.volume = 1.0f; // Restore the volume to its original level
        }

        isMuting = false; // Reset the flag
    }


	public void NextDay()
    {
		if (!soundPlayed) tutorial.ActivateTutorial(6);

		soundPlayed = true;
		// Play the assigned sound clip


		if (yourSoundClip != null && audioSource != null)
		{
			audioSource.PlayOneShot(yourSoundClip, 1.0f);
		}
		// Increment the event counter
		eventCounter++;

		// Output the event counter value to the console log
		day.text = "Dzien: " + eventCounter.ToString();

		Debug.Log("Day " + eventCounter);


		// Mute the background audio sources
		if (!isMuting && backgroundAudioSources != null)
		{
			isMuting = true;

			foreach (AudioSource audioSource in backgroundAudioSources)
			{
				audioSource.volume = 0.2f; // Adjust the volume to a lower level or set to 0 for muting
			}

			StartCoroutine(UnmuteBackgroundSounds());
		}

		List<Client> allClients = new List<Client>();
		GameObject[] allGc;
		allGc = GameObject.FindGameObjectsWithTag("klient");
		foreach (GameObject g in allGc)
		{
			Client c = g.GetComponent<Client>();
			allClients.Add(c);
			c.readyToDrink = false;
		}
		queue.Clear();
		InLine = 0;

		while (allClients.Count > 0)
		{
			Client c = allClients[0];
			c.StopAllCoroutines();
			// StopCoroutine(c.MoveTo(Vector3.zero));
			// StopCoroutine(c.Drink());
			allClients.RemoveAt(0);
			Debug.Log(c.gameObject.name);
			c.dayOver = true;
			StartCoroutine(c.Die());
		}
		GameManager managerG = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		managerG.klienciCoS = 0;
		// Reset the timer after the event
		timer = timer - dayTime;
	}
}
