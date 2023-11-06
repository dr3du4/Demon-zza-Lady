using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<TutorialSO> tutorials  = new List<TutorialSO>();
    [SerializeField]private Image _image;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Button button;
    [SerializeField] private Sprite nextB;
    [SerializeField] private Sprite closeB;
    private int nextTut = -1;

    private int countTip = 0;

    private void Start() {
        //_image = GetComponent<Image>();
        // gameObject.SetActive(false);
    }


    private TutorialSO GetTutorialSO(int i) {
        foreach (TutorialSO t in tutorials) {
            if (t.id == i) return t;
        }
        return null;
    }
    
    public void ActivateTutorial(int i) {
        TutorialSO x = GetTutorialSO(i);
        if (x == null) return;
        _image.sprite = x.tutorialSprite;
        _image.color = new Color32(255,255,255,255);
        tutorialText.text = x.tutorialText;
        nextTut = x.nextTutorial;
        if (nextTut == -1) buttonImage.sprite = closeB;
        else buttonImage.sprite = nextB;
        Time.timeScale = 0f;
        button.onClick.AddListener(DeactivateTutorial);
        gameObject.SetActive(true);
    }

    public void CheckTipTutorial() {
        countTip++;
        if (countTip > 1) return;
        ActivateTutorial(10);
    }

    public void DeactivateTutorial() {
        button.onClick.RemoveListener(DeactivateTutorial);
        _image.sprite = null;
        tutorialText.text = "";
        _image.color = new Color32(255,255,255,0);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        if (nextTut != -1) ActivateTutorial(nextTut);
    }

    public void ShowTutorial(int i) {
        TutorialSO x = GetTutorialSO(i);
        if (x == null) return;
        _image.sprite = x.tutorialSprite;
        _image.color = new Color32(255,255,255,255);
        tutorialText.text = x.tutorialText;
        buttonImage.sprite = closeB;
        button.onClick.AddListener(HideTutorial);
        gameObject.SetActive(true);
    }

    public void HideTutorial() {
        button.onClick.RemoveListener(HideTutorial);
        _image.sprite = null;
        tutorialText.text = "";
        _image.color = new Color32(255,255,255,0);
        gameObject.SetActive(false);
    }
}
