using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeSpotUI : MonoBehaviour
{
    public int minPos = -100;
    public int maxPos = 100;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }


    public void ConfigureSafeSpot(float from, float to)
    {
        from /= 100f;
        to /= 100f;
        float x = ((maxPos - minPos) * from);
        rect.localScale = new Vector3(to - from, 1f, 1f);
        rect.anchoredPosition = new Vector3(x, 0, 0);
    }
}
