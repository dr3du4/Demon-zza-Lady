using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;


public class Walker : MonoBehaviour
{
    public GameObject This;
    public float speed = 5f;
    private Vector3 startPostion;
    private bool enter = true;
    public int waitTime = 10;
    private Vector3 objectPosition;
    private float currentTime = 0.0f;
    private bool isCountingDown = true;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        startPostion = this.transform.position;
        waitTime = Random.Range(10, 35);
        Debug.Log(waitTime);
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        objectPosition=transform.position;
        if (objectPosition.y < 4 & enter)
        {
            moveUP();
            
        }
        if (objectPosition.y >= 4)
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
        Vector3 moveDirection = new Vector3(0, 2);
        moveDirection.Normalize();
        
        Vector2 newPosition =transform.position + moveDirection * speed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    void moveDown()
    {
        Vector3 moveDirection = new Vector3(0, -2);
        moveDirection.Normalize();
        
        Vector2 newPosition =transform.position + moveDirection * speed * Time.deltaTime;
        rb.MovePosition(newPosition);
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
