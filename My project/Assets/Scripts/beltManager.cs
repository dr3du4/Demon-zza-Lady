using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class beltManager : MonoBehaviour
{
    public GameManager gm;
    [SerializeField] private Tutorial tutorial;
    public GameObject[] belts;
    private Vector3 target;
    public GameObject belt;
    public List<GameObject> places;
    public int amountOfPeople = 1;
    public int HP = 5;
    public bool deamonExist = false;
    public int killedDeamons = 0;

    // Start is called before the first frame update
    void Start()
    {
        amountOfPeople = gm.sprzedanepiwa;
        target = transform.position;
        belts=GameObject.FindGameObjectsWithTag("belt");    
    }

    // Update is called once per frame
    void Update()
    {   
        amountOfPeople = (gm.sprzedanepiwa-8*(killedDeamons));
        if (belts.Length > 0)
        {
            //if (killedDeamons==0) tutorial.ActivateTutorial(7);
            deamonExist = true;
        }
        else
        {
            deamonExist = false;
        }

        if (amountOfPeople%8==0 & amountOfPeople>0 & belts.Length==0)
        {
            CreateObject();
            belts=GameObject.FindGameObjectsWithTag("belt"); 
            
        }
        // else if (amountOfPeople%8!=0 & belts.Length > 0)
        // {
        //     DestroyObjects();
        //     belts=GameObject.FindGameObjectsWithTag("belt");
        //     HP = 0;
        // }

        if (HP == 0 & belts.Length > 0)
        {
            DestroyObjects();
            belts=GameObject.FindGameObjectsWithTag("belt");
            

        }

        if (Input.GetMouseButtonDown(0) && belts.Length>0)
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

        if (HP == 0)
        {
            killedDeamons++;
        }
    }
    
    
}
