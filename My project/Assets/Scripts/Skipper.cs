using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Skipper : MonoBehaviour
{
    private Slider skipSlider;
    float value = 0f;
    public int skipSpeed = 5;

    private void Start()
    {
        skipSlider = GetComponentInChildren<Slider>();
        skipSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if (!skipSlider.isActiveAndEnabled)
                skipSlider.gameObject.SetActive(true);
            value += skipSpeed *  Time.deltaTime;
            skipSlider.value = value;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            value = 0;
            skipSlider.value = value;
            skipSlider.gameObject.SetActive(false);
        }

        if (value >= skipSlider.maxValue)
            SceneManager.LoadScene("Scenes/Ship/Main");
    }
}
