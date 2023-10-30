using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    public List<GameObject> tips;

    Dictionary<GameObject, int> currentTips = new Dictionary<GameObject, int>();
    GameManager manager;
    FlipCoin tipAnim;
    [SerializeField] private Tutorial tutorial;
    private void Start()
    {
        foreach(GameObject tip in tips)
            currentTips.Add(tip, 0);
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        tipAnim = GameObject.FindGameObjectWithTag("FlipCoin").GetComponent<FlipCoin>();
    }

    public void spawnTip(int tipAmount)
    {
        // tutorial.CheckTipTutorial();
        Debug.Log("Spawn tip called");
        int i = Random.Range(0, tips.Count);
        if(!tips[i].activeInHierarchy)
            tips[i].SetActive(true);
        currentTips[tips[i]] = tipAmount;
    }

    public void collectTip(GameObject tip)
    {
        manager.AddMoney(0, currentTips[tip]);
        tip.SetActive(false);
        tipAnim.Throw();
    }
}
