using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class SItem : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public int Value;
    public int Type;
}
