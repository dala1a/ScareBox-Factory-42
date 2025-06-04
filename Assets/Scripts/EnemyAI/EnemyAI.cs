using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1.0f;
    public float maxDist = 1.0f;
    float timer = 0.0f;

    NavMeshAgent agent;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            float distance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (distance > maxDist * maxDist)
            {
                agent.destination = playerTransform.position;
            }
            timer = maxTime; 
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
