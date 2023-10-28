using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class gnieciuch : MonoBehaviour
{

    GameManager gameManager;
    public List<GameObject> listOfGnieciuch;
<<<<<<< HEAD:My project/Assets/gnieciuch.cs
=======
    public GameObject finishScreen;
    void Start()
    {
        Stats = new stats(debug,10); //linijka tylko do debugu
        souls = Stats.soul;
        souls = souls / 10;
>>>>>>> 72e8fc63d279709b95c80e4d054f06b1bfa0024e:My project/Assets/Scripts/gnieciuch.cs

    int currentGnieciuchy = 0;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }


    public void gnieciuchy()
    {
        Debug.Log(gameManager.GetGnieciuchy());
        switch (gameManager.GetGnieciuchy())
        {
            case 0:
                if (currentGnieciuchy != gameManager.GetGnieciuchy())
                {
                    Debug.Log("Brak gnieciuchow");
                    RandomPlaces(0);
                }
                break;
            case 1:
                if (currentGnieciuchy != gameManager.GetGnieciuchy())
                {
                    Debug.Log("Jest jeden gnieciuch");
                    RandomPlaces(1);
                }
                break;
            case 2 :
                if (currentGnieciuchy != gameManager.GetGnieciuchy())
                {
                    Debug.Log("Populacja gnieciuchów się zdublowała");
                    RandomPlaces(2);
                }
                break;
            case 3:
                if (currentGnieciuchy != gameManager.GetGnieciuchy())
                {
                    Debug.Log("już mamy 3 gnieciuchy");
                    RandomPlaces(3);
                }
                break;
            case 4:
                if (currentGnieciuchy != gameManager.GetGnieciuchy())
                {
                    Debug.Log("cztery gnieciuchy join to the chat");
                    RandomPlaces(4);
                }
                break;
            case 5:
<<<<<<< HEAD:My project/Assets/gnieciuch.cs
                if (currentGnieciuchy != gameManager.GetGnieciuchy())
                {
                    Debug.Log("Jest AŻ pięć gnieciuchów");
                    RandomPlaces(5);
                }
=======
                Debug.Log("Jest AŻ pięć gnieciuchów");
                RandomPlaces(5);
                WinGame();
>>>>>>> 72e8fc63d279709b95c80e4d054f06b1bfa0024e:My project/Assets/Scripts/gnieciuch.cs
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
            while (listOfGnieciuch[help].activeSelf == true)
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
