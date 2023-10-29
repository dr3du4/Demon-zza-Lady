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
    [SerializeField] private TextMeshProUGUI tutorialText;

    private void Start() {
        _image = GetComponent<Image>();
    }

    public void ActivateTutorial(int i) {
        _image.color = new Color32(255,255,255,255);
        _image.sprite = tutorialSprites[i];
        tutorialText.text = tutorialTexts[i];
        Time.timeScale = 0f;
    }

    public void DeactivateTutorial() {
        _image.sprite = null;
        _image.color = new Color32(255,255,255,0);
        Time.timeScale = 1f;
    }
}
