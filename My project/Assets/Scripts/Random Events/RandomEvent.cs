using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RandomEvent : MonoBehaviour
{
    public enum Difficulty_Level : int
    {
        VERY_EASY = 0,
        EASY = 1,
        MEDIUM = 2,
        HARD = 3,
        VERY_HARD = 4
    }
    public abstract RandomEvent LaunchEvent(Difficulty_Level difficulty);
    public abstract bool CancelEvent();
}
