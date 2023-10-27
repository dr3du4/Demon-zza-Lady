using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;


public class Walker : MonoBehaviour
{
    public GameObject This;
    public float speed = 0.7f;
    private Vector3 startPostion;
    private bool enter = true;
    public int waitTime = 10;
    private Vector3 objectPosition;
    private float currentTime = 0.0f;
    private bool isCountingDown = true;
    
    // Start is called before the first frame update
    void Start()
    {
        startPostion = this.transform.position;
        waitTime = Random.Range(0, 60);
        Debug.Log(waitTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        objectPosition=transform.position;
        if (objectPosition.y < 5 & enter)
        {
            moveUP();
            
        }
        if (objectPosition.y >= 5)
        {
            
            if (isCountingDown)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= waitTime)
                {
                    
                    enter = changeToFalse(enter);

                }
            }

        }
        if (this &!enter)
        {
            moveDown();
            
            
        }

        if (this & objectPosition.y < startPostion.y)
        {
            Destroy(This);
            
        }

        
       

    }

    void moveUP()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);

    }

    void moveDown()
    {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public bool changeToFalse(bool boolian)
    {
        if (boolian)
            return !boolian;
        return boolian;
    }
    
    public void StartCountdown()
    {
        
        currentTime = 0.0f;
    }
   
}
