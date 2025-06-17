using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** 
* Custom Scriptable object to act as the quest database. 
* @author: Yunseo Jeon
* @since: 2025-06-10
*/
[CreateAssetMenu()]
public class QuestAsset : ScriptableObject
{

    public QuestList quests;

}


/** 
* A list of custom quests 
* @author: Yunseo Jeon
* @since: 2025-06-10
*/
[System.Serializable]
public class QuestList
{
    public List<Quest> questList = new List<Quest>();
}

/** 
* Attributes of each quest 
* @author: Yunseo Jeon
* @since: 2025-06-10
*/
[System.Serializable]
public class Quest
{
    public String header;
    public String body;

    public Quest()
    {
        header = "";
        body = "";
    }
}