using UnityEngine;

public class PhoneScript : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private bool isPhoneOn = false;

    public void togglePhone()
    {
        isPhoneOn = !isPhoneOn;
        playPhoneAnimation();
    }

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

}
