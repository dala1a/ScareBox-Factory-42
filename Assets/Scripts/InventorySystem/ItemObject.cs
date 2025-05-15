using System;
using UnityEngine;


public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    [TextArea(15,20)] public String description; 
}
