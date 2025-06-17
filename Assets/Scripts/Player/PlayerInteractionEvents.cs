using UnityEngine;

/** 
* Contains animations for all interactable items.
* @author: Oliver Thompson
* @since: 2025-05-30
*/
public class PlayerInteractionEvents : MonoBehaviour
{

    private bool isPrisonOpen = false; // Toggle Prison Door
    private bool isLeverUp = true; // Toggle Prison Lever
    private bool isBossDoor = false; // Toggle Boss room Door
    [SerializeField] public GameObject mannequinPrefab; 

    /** 
    * Toggle prison door
    * @author: Oliver Thompson
    * @since: 2025-05-30
    * @param: Reference to the prison door animator to play the appropriate animation
    */
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

    /** 
    * Toggle Prison lever
    * @author: Oliver Thompson
    * @since: 2025-05-30
    * @param: Reference to the prison lever animator to play the appropriate animation
    */
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

    /** 
    * Toggle ExitDoot
    * @author: Oliver Thompson
    * @since: 2025-05-30
    * @param: Reference to the exit door animator to play the appropriate animation
    */
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

    /** 
    * Toggle boss door
    * @author: Oliver Thompson
    * @since: 2025-05-30
    * @param: Reference to the boss door animator to play the appropriate animation
    */
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

    public void diguise(GameObject gameObject)
    {   
        Vector3 manPos = gameObject.transform.position;
        var obj = Instantiate(mannequinPrefab, gameObject.transform.parent.transform);
        obj.transform.position = manPos;
        Destroy(gameObject); 
    }






}