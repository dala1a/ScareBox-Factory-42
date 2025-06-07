using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDist = 1.0f;
    public float maxSightDistance = 5.0f;
    public float maxAgroRange = 10f; 
}
