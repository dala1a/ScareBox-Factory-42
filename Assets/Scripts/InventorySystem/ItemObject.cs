using System;
using UnityEngine;

/**
* A custom object to store information about each object. Makes it easier to retrieve information of each object
* @author: Oliver Thompson
* @since: 2025-05-25
*/
public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite uiDisplay;
    public GameObject itemObject;
    [TextArea(15, 20)]
    public string description;
}
