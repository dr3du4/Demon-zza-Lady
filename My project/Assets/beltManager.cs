using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class beltManager : MonoBehaviour
{
    public GameObject[] belts;
    private Vector3 target;
    public GameObject belt;
    public List<GameObject> places;
    public int amountOfPeople = 1;
    public int HP = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        belts=GameObject.FindGameObjectsWithTag("belt");    
    }

    // Update is called once per frame
    void Update()
    {   
        if (amountOfPeople%8==0 & amountOfPeople>0 & belts.Length==0)
        {
            CreateObject();
            belts=GameObject.FindGameObjectsWithTag("belt"); 
            
        }
        else if (amountOfPeople%8!=0 & belts.Length > 0)
        {
            DestroyObjects();
            belts=GameObject.FindGameObjectsWithTag("belt");
            HP = 0;
        }

        if (HP == 0 & belts.Length > 0)
        {
            DestroyObjects();
            belts=GameObject.FindGameObjectsWithTag("belt");
            amountOfPeople = amountOfPeople-2; 

        }

        if (Input.GetMouseButtonDown(0))
        {
            PIERDOLNIJDEMONAWCYMBAL();
        }
    }
    void CreateObject()
    {
        int indeks = Random.Range(0, places.Count);
        Instantiate(belt, places[indeks].transform.position, Quaternion.identity);
        HP = 5;
    }

    void DestroyObjects()
    {
        for (int i = 0; i < belts.Length; i++)
        {
            Destroy(belts[i]);
        }
    }

    void PIERDOLNIJDEMONAWCYMBAL()
    {
        
        
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        
       
        
        float distanceToStol = Vector3.Distance(target, belts[0].transform.position);
        if (distanceToStol < 0.2f)
        {
            HP--;

        }
    }
    
    
}
