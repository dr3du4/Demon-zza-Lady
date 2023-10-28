using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogObject")]
public class DialogObject : ScriptableObject
{
    [SerializeField] [TextArea]private string[] dialogue;

    public string[] Dialogue => dialogue;

}
