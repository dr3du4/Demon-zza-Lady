using UnityEngine;

[System.Serializable]
public struct stats
{
    public int money;
    public int soul;

    public stats( int Soul, int Money)
    {
        soul = Soul;
        money = Money;
    }
}