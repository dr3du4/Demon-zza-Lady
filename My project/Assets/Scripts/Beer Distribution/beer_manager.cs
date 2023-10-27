using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beer_manager : MonoBehaviour
{
    // DO KOSZA XDD
    public bool minigameActive = false;

    public List<Queue<char>> beers;

    List<bool> viable;

    // Start is called before the first frame update
    void Start()
    {
        viable = RenewBeers(beers);
        printCodes(beers);
    }

    List<bool> RenewBeers(List<Queue<char>> beerCodes)
    {
        List<bool> retVal = new List<bool>();
        foreach (Queue<char> c in beerCodes)
            retVal.Add(true);
        return retVal;
    }

    void Update()
    {
        if (minigameActive && Input.anyKeyDown)
        {
            printCodes(beers);
            foreach (char c in Input.inputString)
            {
                for (int i = 0; i < beers.Count; i++)
                    if (c != beers[i].Dequeue())
                        viable[i] = false;
            }
        }
    }

    void printCodes(List<Queue<char>> beers)
    {
        foreach(Queue<char> q in beers)
        {
            string toPrint = "";
            foreach (char c in q)
                toPrint += c;

            Debug.Log("Beer Code: " + toPrint);
        }
    }
}
