using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class beltAlert : MonoBehaviour
{
    
    public beltManager bm;
    public Image alert;
    public bool isDeamon = false;
    // Start is called before the first frame update
    void Start()
    {
        
        isDeamon = bm.deamonExist;
    }

    // Update is called once per frame
    void Update()
    {
        isDeamon = bm.deamonExist;
        if (isDeamon == true)
        {
            alert.color=Color.white;
        }
        else
        {
            alert.color=Color.black;
        }
        
    }
}
