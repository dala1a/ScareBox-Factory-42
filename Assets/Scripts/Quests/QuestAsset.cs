using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class QuestAsset : ScriptableObject
{

    public QuestList quests;

}

[System.Serializable]
public class QuestList
{
    public List<Quest> questList = new List<Quest>();
}

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