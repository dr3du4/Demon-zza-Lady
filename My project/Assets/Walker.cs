using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class Walker : MonoBehaviour
{
    public GameObject This;
    public float speed = 0.7f;
    private Vector3 startPostion;
    private bool enter = true;
    public int waitTime = 10;
    private Vector3 objectPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        startPostion = this.transform.position;
         
        
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
            
            enter = changeToFalse(enter);
            
        }
        if (this &!enter)
        {
            moveDown();
            
            
        }

        if (this & objectPosition.y < startPostion.y)
        {
            Destroy(This);
            Debug.Log("fswjsdjhkfjkhsddjkhfhjk");
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
    
    
    
}
