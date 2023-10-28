using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class gnieciuch : MonoBehaviour
{

    public int debug;
    public stats Stats;
    public int souls;
    private int helper;
    public List<GameObject> listOfGnieciuch;
    public GameObject finishScreen;
    void Start()
    {
        Stats = new stats(debug,10); //linijka tylko do debugu
        souls = Stats.soul;
        souls = souls / 10;

        helper = souls;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        souls = Stats.soul;
        souls = souls / 10;
        if (souls != helper)
        {
            helper = souls;
            gnieciuchy();
        }
       
    }

    void gnieciuchy()
    {
        switch (souls)
        {
            case 0:
                Debug.Log("Brak gnieciuchow");
                RandomPlaces(0);
                break;
            case 1:
                Debug.Log("Jest jeden gnieciuch");
                RandomPlaces(1);
                break;
            case 2 :
                Debug.Log("Populacja gnieciuchów się zdublowała");
                RandomPlaces(2);
                break;
            case 3:
                Debug.Log("już mamy 3 gnieciuchy");
                RandomPlaces(3);
                break;
            case 4:
                Debug.Log("cztery gnieciuchy join to the chat");
                RandomPlaces(4);
                break;
            case 5:
                Debug.Log("Jest AŻ pięć gnieciuchów");
                RandomPlaces(5);
                WinGame();
                break;
            default:
                Debug.Log("XDDDDD");
                break;
            
        }
    }

    void RandomPlaces(int number)
    {   
        
        for (int i = 0; i< listOfGnieciuch.Count; i++)
        {
            listOfGnieciuch[i].SetActive(false);
        }

        for (int i = 0; i < number; i++)
        {
            int help = Random.Range(0, listOfGnieciuch.Count);
            while (listOfGnieciuch[help].active == true)
            {
                help=Random.Range(0, listOfGnieciuch.Count);
            }
            listOfGnieciuch[help].SetActive(true);    
        }
    }

    void WinGame()
    {
        finishScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    
}
