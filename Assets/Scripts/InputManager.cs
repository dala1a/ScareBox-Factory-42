using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine;

public class InputManager : MonoBehaviour
{

   // [SerializeField] private PlayerMovement playerMovement; 
    //[SerializeField] private PlayerLook playerLook;
    [SerializeField] private GameObject displayInventory;

    private PlayerInput playerInput; 
    public PlayerInput.OnFootActions onFoot;


    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        //onFoot.Jump.performed += ctx => playerMovement.jump();
        //onFoot.CursorLock.performed += ctx => playerLook.ToggleLock();
        // onFoot.DropItem.performed += ctx => dropItem();
        onFoot.Inv1.performed += ctx => selectSlot1(); 
        onFoot.Inv2.performed += ctx => selectSlot2(); 
        onFoot.Inv3.performed += ctx => selectSlot3(); 
        onFoot.Inv4.performed += ctx => selectSlot4(); 
        onFoot.Inv5.performed += ctx => selectSlot5(); 
        onFoot.Inv6.performed += ctx => selectSlot6(); 
        onFoot.Inv7.performed += ctx => selectSlot7(); 

    }

    private void dropItem()
    {
        displayInventory.GetComponent<DisplayInventory>().dropItem(); 
    }

    private void selectSlot1()
    { 
        displayInventory.GetComponent<DisplayInventory>().selectSlot1(); 
    }

        private void selectSlot2()
    { 
        displayInventory.GetComponent<DisplayInventory>().selectSlot2(); 
    }

        private void selectSlot3()
    { 
        displayInventory.GetComponent<DisplayInventory>().selectSlot3(); 
    }

        private void selectSlot4()
    { 
        displayInventory.GetComponent<DisplayInventory>().selectSlot4(); 
    }

        private void selectSlot5()
    { 
        displayInventory.GetComponent<DisplayInventory>().selectSlot5(); 
    }

        private void selectSlot6()
    { 
        displayInventory.GetComponent<DisplayInventory>().selectSlot6(); 
    }

        private void selectSlot7()
    { 
        displayInventory.GetComponent<DisplayInventory>().selectSlot7(); 
    }

    // private void FixedUpdate()
    // {
    //     playerMovement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    // }

    // private void LateUpdate()
    // { 
    //     playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>()); 
    // }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    { 
        onFoot.Disable(); 
    }

}
