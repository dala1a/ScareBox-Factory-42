using UnityEngine;


[CreateAssetMenu()] // Creates a custom menu to add this asset to the project manager. 

/** 
* An object to store the variables that might be vary for different types of enemies. 
* @author: Yunseo Jeon
* @since: 2025-05-29
*/
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f; // The time it takes to update the enemy. 
    public float maxDist = 1.0f; // The distance before the enemy starts chasing the player. 
    public float maxSightDistance = 5.0f; // The distance when the enemy first detects the player. 
    public float maxAgroRange = 10f; // The distance when the enemy loses agro. 
}
