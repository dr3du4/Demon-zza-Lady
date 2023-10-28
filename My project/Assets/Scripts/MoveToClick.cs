using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;

public class MoveToClick : MonoBehaviour
{
    private Client client; 
    public float speed = 5f;
    private Vector3 target;
    public GameObject stol;
    public float proximityDistance = 0.9f; // Minimalna odległość, by uznać, że target jest blisko stolu
    public GameObject[] freePlace;
    private bool move=false;
    public float distanceToStol = 20f;
    public float helper = 20f;
    public bool isDeamon = false;
    public bool reach = false;
    public float dynamicDistance = 20f;
    private Vector3 sitPos;

    private void Start()
    {
        client = GetComponent<Client>();
        target = transform.position;
        freePlace=GameObject.FindGameObjectsWithTag("chair");
        stol = freePlace[0];
    }

    private void Update()
    {

        if (client.waiting && Input.GetMouseButtonDown(1))
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            // Sprawdzenie odległości między target a stol
            if (!isDeamon)
            {
                for (int i = 0; i < freePlace.Length; i++)
                {
                    Debug.Log(freePlace.Length);
                    distanceToStol = Vector3.Distance(target, freePlace[i].transform.position);
                    
                    if (distanceToStol < helper)
                    {   
                        helper = distanceToStol;
                        stol = freePlace[i];

                    }

                }
            }
            else
            {
                int indeks = Random.Range(0, freePlace.Length);
                helper=Vector3.Distance(target, freePlace[indeks].transform.position);
            }
            distanceToStol = helper;
            Debug.Log(distanceToStol);
            if (!stol.GetComponent<TableClients>().active) {
                distanceToStol = 20f;
                return;
            }
            sitPos = stol.GetComponent<TableClients>().AddClient(client);
        }

        if (distanceToStol <= proximityDistance & !reach)
        {   
            client.waiting = false;
            Debug.Log("mozesz podejsc");
            transform.position = Vector3.MoveTowards(transform.position, sitPos, speed * Time.deltaTime);
            dynamicDistance = Vector3.Distance(transform.position, sitPos);
            
            if (dynamicDistance < 0.3f){
                reach = true;
            }
            
                       
        }
        
        
        
    }
}