using UnityEngine;
using UnityEngine.AI;

/**
* Displays Debug information for the enemy controller. 
* @author: Yunseo Jeon
* @since: 2025-05-29
*/
public class DebugNavMesh : MonoBehaviour
{
    public bool velocity;
    public bool desiredVelocity;
    public bool path;
    NavMeshAgent agent; // A reference to the enemy controller. 

    /** 
    * Setup the variables at the start when the script is initialized. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-29
    */
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /** 
    * Inbuilt function of unity used to draw debug information. Drawing the path, velocity and the decired velocity. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */
    void OnDrawGizmos()
    {
        if (velocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);
        }
        if (desiredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }
        if (path)
        {
            Gizmos.color = Color.black;
            var agentPath = agent.path;
            Vector3 prevCorner = transform.position;
            foreach (var corner in agentPath.corners)
            {
                Gizmos.DrawLine(prevCorner, corner);
                Gizmos.DrawSphere(corner, 0.1f);
                prevCorner = corner;
            }
        }
    }

}
