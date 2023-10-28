using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarQueue : MonoBehaviour
{
    // private List<GameObject> objectToSpawn ;
    [SerializeField]private List<Client> queue = new List<Client>();
    public int maxInLine = 10;
    public int InLine = 0;
	public Client firstClient = null;
	private float timerClient;
	private Vector3 first;

	private void Start(){
		first = transform.position - new Vector3(0f,1.5f,0f);
		if(InLine > 0) {
			firstClient = queue[0];
			
			SetClientsOnPos();
		}
	}

	private void Update() {
		if(firstClient != null) {
			if (Vector3.Distance(firstClient.transform.position,first) < 0.1f) {
				firstClient.StartTimer(this);
				//added
				firstClient.ShowIndicatorSquare(true); // Show indicator square for the first client
				firstClient = null;
			}
		}
	}

    private void SetClientsOnPos(){
		if (InLine == 0) return;
		for (int i=0; i<InLine; i++) {
			StartCoroutine(queue[i].MoveTo(first - (i * new Vector3(0,1.5f,0))));
		}
	}


    public void RemoveFirstClient() {
		if (InLine == 0) return;
		//added
	//	firstClient.ShowIndicatorSquare(false); // Hide indicator square for the departing first client
		queue.RemoveAt(0);
		InLine--;
		firstClient = null;
		if (InLine == 0) return;
		SetClientsOnPos();
		firstClient = queue[0];
	}

	public void AddClient(Client c) {
		StartCoroutine(c.MoveTo(first - (InLine * new Vector3(0,1.5f,0))));
		queue.Add(c);
		InLine++;
		firstClient = queue[0];
		//added
		/*
		if (InLine > 1) // If the queue already has clients, hide the previous indicator square
		{
			queue[0].ShowIndicatorSquare(false);
		}
		c.ShowIndicatorSquare(true); // Show indicator square for the new first client
	*/}
}
