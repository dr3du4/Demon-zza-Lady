using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<TutorialSO> tutorials  = new List<TutorialSO>();
    private Image _image;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Image button;
    [SerializeField] private Sprite nextB;
    [SerializeField] private Sprite closeB;
    private int nextTut = -1;

    private int countTip = 0;

    private void Start() {
        _image = GetComponent<Image>();
        // gameObject.SetActive(false);
    }


    private TutorialSO GetTutorial(int i) {
        foreach (TutorialSO t in tutorials) {
            if (t.id == i) return t;
        }
        return null;
    }
    
    public void ActivateTutorial(int i) {
        TutorialSO x = GetTutorial(i);
        if (x == null) return;
        _image.sprite = x.tutorialSprite;
        _image.color = new Color32(255,255,255,255);
        tutorialText.text = x.tutorialText;
        Time.timeScale = 0f;
        nextTut = x.nextTutorial;
        if (nextTut == -1) button.sprite = closeB;
        else button.sprite = nextB;
        gameObject.SetActive(true);
    }

    public void CheckTipTutorial() {
        countTip++;
        if (countTip > 1) return;
        ActivateTutorial(10);
    }

    public void DeactivateTutorial() {
        gameObject.SetActive(false);
        _image.sprite = null;
        tutorialText.text = "";
        _image.color = new Color32(255,255,255,0);
        Time.timeScale = 1f;
        if (nextTut != -1) ActivateTutorial(nextTut);
    }
}
