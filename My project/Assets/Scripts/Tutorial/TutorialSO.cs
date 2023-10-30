using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial-Data", menuName = "ScriptableObjects/TutorialSO")]
public class TutorialSO : ScriptableObject
{
    public int id;
    public Sprite tutorialSprite;
    [TextArea] public string tutorialText;
    public int nextTutorial = -1;
}
