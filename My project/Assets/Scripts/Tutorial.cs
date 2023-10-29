using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<Sprite> tutorialSprites  = new List<Sprite>();
    [SerializeField][TextArea] private List<string> tutorialTexts  = new List<string>();
    private Image _image;
    private int lastInt = -1;
    [SerializeField] private TextMeshProUGUI tutorialText;

    private void Start() {
        _image = GetComponent<Image>();
    }

    public void ActivateTutorial(int i) {
        _image.sprite = tutorialSprites[i];
        _image.color = new Color32(255,255,255,255);
        tutorialText.text = tutorialTexts[i];
        Time.timeScale = 0f;
        lastInt = i;
    }

    public void DeactivateTutorial() {
        _image.sprite = null;
        tutorialText.text = "";
        _image.color = new Color32(255,255,255,0);
        Time.timeScale = 1f;
        switch(lastInt){
            case 0:
                ActivateTutorial(1);
                break;
            case 3:
                ActivateTutorial(4);
                break;
            default:
                break;
        }
    }
}
