using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ClientType", order = 1)]
public class clientTypeSO : ScriptableObject
{
    public string clientTypeName = "Seba";
    public beerSO beerPreference;
}
