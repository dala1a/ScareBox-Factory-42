using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private TextMeshProUGUI body;
    [SerializeField] private QuestAsset questAsset;
    [SerializeField] private InventoryObject inventoryObject;
    [SerializeField] private GameObject mapCanvas; 
    [SerializeField] private GameObject inspectCanvas; 
    
    private Rigidbody rb;
    private int questCounter = 0;
    private Animator animator;
    private bool isQuestHidden = false;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isQuestHidden = false; 
    }


    void Update()
    {
        checkQuest();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleQuestUI();
        }
    }


    private void checkQuest()
    {
        switch (questCounter)
        {
            case 0: // WASD QUEST
                if (rb.linearVelocity.x != 0)
                {
                    switchQuestEvent(0);
                }
                break;

            case 1: //  Escape Cell quest

                break;

            case 2: // Find Items quest

                for (int i = 0; i < inventoryObject.Container.Items.Length; i++)
                {
                    if (inventoryObject.Container.Items[i].ID == 1)
                    {
                        switchQuestEvent(2);
                    }
                }

                break;

            case 3: // Equip Item Quest
                for (int i = 0; i < inventoryObject.Container.Items.Length; i++)
                {
                    if (inventoryObject.Container.Items[i].isEquipped)
                    {
                        switchQuestEvent(3);
                    }
                }
                break;

            case 4: // Inspect Item Quest
                if (inspectCanvas.transform.childCount != 0)
                {
                    switchQuestEvent(4);
                }
                break;

            case 5: // Map Quest
                if (mapCanvas.activeSelf)
                {
                    switchQuestEvent(5);
                }
                break;
            case 6: // Escape Prison Room

                break; 




        }
    }

    public void updateText()
    {   
        header.text = questAsset.quests.questList[questCounter].header;
        body.text = questAsset.quests.questList[questCounter].body;
    }

    public void switchQuestEvent(int i)
    {
        i++;
        questCounter = i;

        if (questCounter >= questAsset.quests.questList.Count)
        { 
            if (!isQuestHidden)
            {
                animator.Play("QuestClose", 0, 0.0f);
            }
            return; 
        }

        if (!isQuestHidden)
        {
            animator.Play("SwitchQuest", 0, 0.0f);
        }
        else
        {
            updateText();
        }
    }

    private void toggleQuestUI()
    {
        if (isQuestHidden)
        {
            animator.Play("QuestOpen", 0, 0.0f);
        }
        else
        {
            animator.Play("QuestClose", 0, 0.0f);
        }
        isQuestHidden = !isQuestHidden; 
    }


}
