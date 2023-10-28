using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ClientType", order = 1)]
public class clientTypeSO : ScriptableObject
{
    public enum ClientType
    {
        FARMER,
        RYCERZ,
        SZLACHCIC
    }

    public ClientType clientType;
    public string clientTypeName = "Seba";
    public beerSO beerPreference;
    public List<ClientType> companyPreference;
}
