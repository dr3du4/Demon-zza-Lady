using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsSlider : MonoBehaviour
{
    [SerializeField] private float max = 2.2f;
    [SerializeField] private float min = 0f;
    [SerializeField] private RectTransform rectTransform;
    private GameObject gmObj;
    private GameManager gm;
    float _x;
    private float liczba_dusz;

    private bool active = false;

    private void Start() {
        gameObject.SetActive(false);
        gmObj = GameObject.FindWithTag("GameController");
        gm  = gmObj.GetComponent<GameManager>();
    }

    private void Update(){
        liczba_dusz = gm.GetSouls();
        _x = (liczba_dusz/50) * (max-min);
        var scale = rectTransform.localScale;
        scale.x = _x;
        rectTransform.localScale = scale;
    }

    public void ToogleActive() {
        active = !active;
        gameObject.SetActive(active);
    }
}
