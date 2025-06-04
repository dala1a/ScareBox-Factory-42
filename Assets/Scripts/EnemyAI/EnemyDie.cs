using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    Ragdoll ragdoll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
    }

    public void knockOut()
    {
        ragdoll.activateRagdoll();
    }
}
