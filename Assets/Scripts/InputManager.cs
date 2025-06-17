using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine;


/** 
* Takes in input from the new unity input manager and assigns then functions. 
* @author: Yunseo Jeon
* @since: 2025-05-25
*/ 
public class InputManager : MonoBehaviour
{

    [SerializeField] private GameObject displayInventory; // Reference to the inventory
    [SerializeField] private QuestController questController; // Reference to the quest controller. 

    private PlayerInput playerInput; // Reference to the unity input manager. 
    public PlayerInput.OnFootActions onFoot;


    private void Awake()
    {
        playerInput = new PlayerInput(); // Initialize the input manager. 
        onFoot = playerInput.OnFoot;

        // Button Bindings for inventory Slots. 
        onFoot.Inv1.performed += ctx => selectSlot1(); 
        onFoot.Inv2.performed += ctx => selectSlot2();
        onFoot.Inv3.performed += ctx => selectSlot3();
        onFoot.Inv4.performed += ctx => selectSlot4();
        onFoot.Inv5.performed += ctx => selectSlot5();
        onFoot.Inv6.performed += ctx => selectSlot6();
        onFoot.Inv7.performed += ctx => selectSlot7();

        // Button Binding to use an item. 
        onFoot.UseItem.performed += ctx => useItem();

    }

    /** 
    * Run the use item method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void useItem()
    {
        displayInventory.GetComponent<DisplayInventory>().useItem();
    }

    /** 
    * Run the selectSlot1 method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void selectSlot1()
    {
        displayInventory.GetComponent<DisplayInventory>().selectSlot1();
    }

    /** 
    * Run the selectSlot2 method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void selectSlot2()
    {
        displayInventory.GetComponent<DisplayInventory>().selectSlot2();
    }

    /** 
    * Run the selectSlot3 method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void selectSlot3()
    {
        displayInventory.GetComponent<DisplayInventory>().selectSlot3();
    }

    /** 
    * Run the selectSlot4 method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void selectSlot4()
    {
        displayInventory.GetComponent<DisplayInventory>().selectSlot4();
    }

    /** 
    * Run the selectSlot5 method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void selectSlot5()
    {
        displayInventory.GetComponent<DisplayInventory>().selectSlot5();
    }

    /** 
    * Run the selectSlot6 method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void selectSlot6()
    {
        displayInventory.GetComponent<DisplayInventory>().selectSlot6();
    }

    /** 
    * Run the selectSlot7 method in the display inventory script
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void selectSlot7()
    {
        displayInventory.GetComponent<DisplayInventory>().selectSlot7();
    }

   
    /** 
    * enable the onFoot keybinds when the script is initialized
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */ 
    private void OnEnable()
    {
        onFoot.Enable();
    }

    /** 
    * Disable the onFoot keybinds when the script is disabled
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */ 
    private void OnDisable()
    {
        onFoot.Disable();
    }

}
