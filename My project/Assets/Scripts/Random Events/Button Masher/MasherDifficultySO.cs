using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Minigames/Masher Difficulty", order = 1)]
public class MasherDifficultySO : ScriptableObject
{
    public float startValue = 30f;
    public float goalValue = 100f;
    public float mashIncrement = 5f;
    public float decreaseRate = 25f;
}
