using System;
using UnityEngine;


public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite uiDisplay;
    [TextArea(15, 20)]
    public string description;

}
