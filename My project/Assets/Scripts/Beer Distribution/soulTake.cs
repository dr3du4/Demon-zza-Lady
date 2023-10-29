using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulTake : MonoBehaviour
{
    // public GameObject killToolTip;
    // public Sprite killIcon;

    public float disablePosY;
    public float enablePosY;
    public int speed = 1;
    Animation anim;
    public bool active = false;
    public Animation soulAnim;

    public List<string> soulAnimNames = new List<string>() { "soul1", "soul2" };

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (active)
            {
                DisableButton();
            }
            else
                EnableButton();
        }*/
    }


    public void EnableButton()
    {
        active = true;
        anim.Play("slideDown", PlayMode.StopAll);
    }

    public void DisableButton()
    {
        active = false;
        anim.Play("slideUp", PlayMode.StopAll);
    }

    public void PlaySoulTakingAnim()
    {
        int r = Random.Range(0, 1);
        soulAnim.Play(soulAnimNames[r]);
    }
}
