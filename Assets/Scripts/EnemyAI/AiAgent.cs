using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{

    public AiStateMachine stateMachine;
    public AiStateID initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config; 
    public Transform playerTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        stateMachine = new AiStateMachine(this);
        stateMachine.registerState(new AiChasePlayerState()); 
        stateMachine.registerState(new AiIdlePlayerState());
        stateMachine.changeState(initialState);  
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.update(); 
    }
}
