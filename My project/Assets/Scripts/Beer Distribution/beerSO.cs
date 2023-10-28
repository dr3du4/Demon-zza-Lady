using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Beer", order = 1)]
public class beerSO : ScriptableObject
{
    public string beerName;
    public Sprite beerIcon;
    public Sprite beerMugIcon;
    public int beerPrice = 10;
}
