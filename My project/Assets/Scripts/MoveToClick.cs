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
    public float proximityDistance = 1.25f; // Minimalna odległość, by uznać, że target jest blisko stolu
    public GameObject[] freePlace;
    private bool move=false;
    public float distanceToStol = 20f;
    public float helper = 20f;
    public bool isDeamon = false;
    public bool reach = false;
    public float dynamicDistance = 20f;
    // Bodzio fix
    /*private Vector3 sitPos;
    bool movingToClick = false;*/

    private Vector3 sitPos = new Vector3(0,0,0);

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
                stol = freePlace[indeks];
            }
            distanceToStol = helper;
            Debug.Log(distanceToStol);
            TableClients t = stol.GetComponent<TableClients>();
            if (!t.active) {
                distanceToStol = 100f;
                return;
            }
            else if (!t.IsClient(client))sitPos = t.AddClient(client);
            client.goingUp = false;
            // Bodzio fix
            //movingToClick = true;
        }

        // Bodzio fix 
        /*if (distanceToStol <= proximityDistance & !reach)
        {*/
        if (sitPos != Vector3.zero)
        {   
            client.waiting = false;
            //Debug.Log("mozesz podejsc");
            Debug.Log(sitPos);
            transform.position = Vector3.MoveTowards(transform.position, sitPos, speed * Time.deltaTime);
            dynamicDistance = Vector3.Distance(transform.position, sitPos);

            if (dynamicDistance < 0.3f)
            {
                reach = true;
                // Bodzio fix
                /*if (client.sit > 0 && client.sit < 3) client.goingUp = true;
                else client.goingUp = false;
            }


        }
        else if (!client.waiting && movingToClick && !reach)
        {
            //StartCoroutine(client.Die());
            client.waiting = true;
            movingToClick = false;*/
                sitPos = new Vector3(0,0,0);
                if(client.sit > 0 && client.sit < 3) client.goingUp = true;
                else client.goingUp = false;
            }           
        }
    }
}