using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/** 
* Class containing the main behaviors for the quest system. 
*  @author: Yunseo Jeon
* @since: 2025-06-10
*/ 
public class QuestController : MonoBehaviour
{

    [SerializeField] private GameObject player; // Reference to the player
    [SerializeField] private TextMeshProUGUI header; // Reference the to quest Header
    [SerializeField] private TextMeshProUGUI body; // Reference to the quest body
    [SerializeField] private QuestAsset questAsset; // Reference to the quest scriptable object. 
    [SerializeField] private InventoryObject inventoryObject; // Reference to the inventory scriptable object
    [SerializeField] private GameObject mapCanvas; // Reference to the map (for a quest)
    [SerializeField] private GameObject inspectCanvas; // Reference to the inspect menu (for a quest)

    private Rigidbody rb; // Reference to the player rigidbody. 
    private int questCounter = 0; // keeping track of what quest we are on
    private Animator animator; // Reference to the quest ui animator
    private bool isQuestHidden = false; // Toggle quest ui 

    /** 
    * Initalize all the variables if needed on startup
    * @author: Yunseo Jeon
    * @since: 2025-06-10
    */ 
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isQuestHidden = false;
    }

    /** 
    * Repeatedly update the quest ui
    * @author: Yunseo Jeon
    * @since: 2025-06-10
    */
    void Update()
    {   
        // Check if quest is done or not
        checkQuest();

        // Togle quest ui
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleQuestUI();
        }
    }

    /** 
    * Check what quest we are on and check if its completed or not based on the quest counter. The conditions for each quest goes in here
    * @author: Yunseo Jeon
    * @since: 2025-06-10
    */
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

    /** 
    * Update the quest UI textboxes with the information of the current quest. 
    * @author: Yunseo Jeon
    * @since: 2025-06-10
    */
    public void updateText()
    {
        header.text = questAsset.quests.questList[questCounter].header;
        body.text = questAsset.quests.questList[questCounter].body;
    }

    
    /** 
    * Switch from one quest to another, Plays the animation and updates the text. 
    * @author: Yunseo Jeon
    * @since: 2025-06-10
    * @param i: The current quest you just finished.
    */
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

    
    /** 
    * Toggle quest ui by playing the animation 
    * @author: Yunseo Jeon
    * @since: 2025-06-10
    */
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
