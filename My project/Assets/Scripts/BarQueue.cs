using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarQueue : MonoBehaviour
{
    // private List<GameObject> objectToSpawn ;
    [SerializeField]private List<Client> queue = new List<Client>();
	[SerializeField]private Vector3 offSetFirst = new Vector3(0f,-0.25f,0f);
	[SerializeField]private Vector3 offSet = new Vector3(0f,-0.75f,0f);
    public int maxInLine = 5;
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


	public AudioSource[] backgroundAudioSources; // Reference to multiple background audio sources
    public float muteDuration = 2.0f; // Duration to mute the background sounds

    private bool isMuting = false;

	
	//


	public void SetClientServed(bool b) { clientServed = b; }
	public bool GetClientServed() { return clientServed; }

	private void Start(){
		first = transform.position += offSetFirst;
		if(InLine > 0) {
			firstClient = queue[0];
			
			SetClientsOnPos();
		}
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

		// Timer logic  added KASIA o 3:32 niedziela
        if (InLine > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 10f && !soundPlayed) // 180 seconds = 3 minutes
            {
                soundPlayed = true;
				// Play the assigned sound clip
				
                if (yourSoundClip != null && audioSource != null)
                {
                    audioSource.PlayOneShot(yourSoundClip, 1.0f);
                }
				
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

				// Access all clients from the queue and tables
				List<Client> queueClients = new List<Client>();

				// Get clients from the queue
				if (queue != null)
				{
					queueClients.AddRange(queue);
				}

				// Add clients from all table clients to the allClients list
				foreach (TableClients table in allTables)
				{
					allClients.AddRange(table.clients);
				}

				// Combine clients from the queue and tables
				allClients.AddRange(queueClients);

				// Perform actions on allClients list
				foreach (Client client in allClients)
				{
					StartCoroutine(client.Die());
				}
			}
		
        }
	}

    private void SetClientsOnPos(){
		if (InLine == 0) return;
		for (int i=0; i<InLine; i++) {
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
			StartCoroutine(c.Die());
		}
		else{
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
}
