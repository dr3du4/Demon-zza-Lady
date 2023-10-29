using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preferenceHelper : MonoBehaviour
{
    [System.Serializable]
    public struct typeSprite
    {
        public clientTypeSO.ClientType type;
        public Sprite sprite;
    }


    public List<typeSprite> typeSprites;
    Dictionary<clientTypeSO.ClientType, Sprite> _typeSprites = new Dictionary<clientTypeSO.ClientType, Sprite>();

    public List<SpriteRenderer> icons;

    private void Start()
    {
        foreach(typeSprite tp in typeSprites)
        {
            _typeSprites.Add(tp.type, tp.sprite);
        }
        HidePreferences();
    }

    public void ShowPreferences(List<clientTypeSO.ClientType> types)
    {
        int i = 0;
        foreach(clientTypeSO.ClientType type in types)
        {
            icons[i].gameObject.SetActive(true);
            icons[i].sprite = _typeSprites[types[i]];
            i++;
        }
    }

    public void HidePreferences()
    {
        foreach (SpriteRenderer sprite in icons)
            sprite.gameObject.SetActive(false);
    }

}
