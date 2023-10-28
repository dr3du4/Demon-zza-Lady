using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Minigames/Charge Difficulty", order = 1)]
public class ChargeDifficultySO : ScriptableObject
{
    public float minValue = 60f;
    public float maxValue = 80f;
    public float startValue = 50f;
    public float holdIncrement = 5f;
    public float decreaseRate = 5f;
    public float passTime = 3f;
}
