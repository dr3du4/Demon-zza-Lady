using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewritterEffect : MonoBehaviour
{

    [SerializeField] private float typerSpeed = 50f;
    public Coroutine Run(string TextToType, TMP_Text textLabel)
    {
        return StartCoroutine( TypeText(TextToType, textLabel));

    }

    private IEnumerator TypeText(string TextToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        //yield return new WaitForSeconds(2);

        float t =0;
        int charIndex =0;

        while(charIndex < TextToType.Length)
        {
            t+=Time.deltaTime * typerSpeed;
            charIndex =Mathf.FloorToInt(t);
            charIndex=Mathf.Clamp(charIndex, 0, TextToType.Length);

            textLabel.text = TextToType.Substring(0,charIndex);


            yield return null;
        }

        textLabel.text = TextToType;
    }
}
