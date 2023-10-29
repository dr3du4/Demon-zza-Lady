using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacja : MonoBehaviour
{
    
    private Animator animator;
    private Rigidbody2D rb;
    public MoveToClick mtc;
    public Client klient;
    public bool upDown ;
    public bool siad;
    public bool wstan;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        upDown = !klient.goingUp;
        siad = mtc.reach;
        wstan = klient.staryWstal;
    }

    void Update()
    {
        upDown = klient.goingUp;
        siad = mtc.reach;
        wstan = klient.staryWstal;
        

        if (upDown & !siad & !wstan)
        {
            animator.SetBool("Up", true);
            animator.SetBool("Stand", false);
            animator.SetBool("Sit", false);
            
        }
        else if (!upDown & !siad & !wstan)
        {
            
            animator.SetBool("Up", false);
            animator.SetBool("Stand", true);
            animator.SetBool("Sit", false);
        }
        else if(siad)
        {
            animator.SetBool("Up", false);
            animator.SetBool("Stand", false);
            animator.SetBool("Sit", true);
        }
        else if(wstan)
        {
            
            animator.SetBool("Up", false);
            animator.SetBool("Stand", false);
            animator.SetBool("Sit", false);
        }

        // Tutaj możesz obsłużyć ruch postaci w zależności od kierunku, np. przesuwać Rigidbody2D itp.
    }
}
