using UnityEngine;

/** 
* Class containing all the behaviors for the phone item. 
* @author: Oliver Thompson
* @since: 2025-05-27
*/ 
public class PhoneScript : MonoBehaviour
{

    [SerializeField] private Animator animator; // Reference to the phone animator
    private bool isPhoneOn = false;

    /**
     * Play the phone animation
     * @author: Oliver Thomspon
     * @since: 2025-05-27
     */
    public void togglePhone()
    {
        isPhoneOn = !isPhoneOn;
        playPhoneAnimation();
    }

    /**
     * Calls the phone animator to play the phone animation. 
     * @author: Oliver Thomspon
     * @since: 2025-05-27
     */
    private void playPhoneAnimation()
    {
        if (isPhoneOn)
        {
            animator.Play("OpenPhone", 0, 0.0f);
        }
        else
        {
            animator.Play("ClosePhone", 0, 0.0f);
        }
    }

    /**
     * Plays the turn off phone animation. 
     * @author: Oliver Thomspon
     * @since: 2025-05-27
     */
    public void turnOffPhone()
    {
        if (isPhoneOn)
        {
            animator.Play("ClosePhone", 0, 0.0f);
        }
    }

}
