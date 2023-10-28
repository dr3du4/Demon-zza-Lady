using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class beltManager : MonoBehaviour
{
    public GameObject[] belts;
    public GameObject belt;
    public List<GameObject> places;
    public int amountOfPeople = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        belts=GameObject.FindGameObjectsWithTag("belt");    
    }

    // Update is called once per frame
    void Update()
    {   
        if (amountOfPeople>=8 & belts.Length==0)
        {
            CreateObject();
            belts=GameObject.FindGameObjectsWithTag("belt"); 
        }
        else if (amountOfPeople < 8 & belts.Length > 0)
        {
            DestroyObjects();
            belts=GameObject.FindGameObjectsWithTag("belt"); 
        }
    }
    void CreateObject()
    {
        int indeks = Random.Range(0, places.Count);
        Instantiate(belt, places[indeks].transform.position, Quaternion.identity);
    }

    void DestroyObjects()
    {
        for (int i = 0; i < belts.Length; i++)
        {
            Destroy(belts[i]);
        }
    }
    
}
