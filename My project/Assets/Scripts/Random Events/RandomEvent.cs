using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RandomEvent : MonoBehaviour
{
    public abstract bool LaunchEvent();
    public abstract bool CancelEvent();
}
