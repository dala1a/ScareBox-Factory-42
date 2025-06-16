using UnityEngine;

public class PlayerInteractionEvents : MonoBehaviour
{

    private bool isPrisonOpen = false;
    private bool isLeverUp = true;
    private bool isBossDoor = false;


    public void openPrisonDoor(Animator animator)
    {
        isPrisonOpen = !isPrisonOpen;

        if (isPrisonOpen)
        {
            animator.Play("OpenBar", 0, 0.0f);
        }
        else
        {
            animator.Play("CloseBar", 0, 0.0f);
        }
    }

    public void flipLever(Animator animator)
    {
        isLeverUp = !isLeverUp;

        if (isLeverUp)
        {
            animator.Play("LeverUp", 0, 0.0f);
        }
        else
        {
            animator.Play("LeverDown", 0, 0.0f);
        }
    }

    public void toggleDoor(Animator animator)
    {
        if (isLeverUp)
        {
            animator.Play("DoorClose", 0, 0.0f);
        }
        else
        {
            animator.Play("DoorOpen", 0, 0.0f);
        }
    }

    public void toggleBossDoor(Animator animator)
    {

        if (isBossDoor)
        {
            animator.Play("CloseDoor", 0, 0.0f);
        }
        else
        {
            animator.Play("OpenDoor", 0, 0.0f);
        }
        isBossDoor = !isBossDoor; 
    }






}