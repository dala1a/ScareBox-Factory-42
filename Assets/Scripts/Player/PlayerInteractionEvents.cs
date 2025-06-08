using UnityEngine;

public class PlayerInteractionEvents : MonoBehaviour
{

    private bool isPrisonOpen = false;
 

    public void openPrisonDoor(Animator animator)
    {
        isPrisonOpen = !isPrisonOpen;

        if (isPrisonOpen)
        {
            animator.Play("OpenBar", 0, 0.0f);
                    Debug.Log("OPENING DOOR");
        }
        else
        {
            animator.Play("CloseBar", 0, 0.0f);
        }
    }






}