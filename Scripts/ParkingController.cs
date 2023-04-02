using UnityEngine;
using UnityEngine.AI;

public class ParkingController : MonoBehaviour{

private NavMeshAgent agent;
public NavMeshObstacle obj;
    
public Vector3 posTarget;
public bool enable = false;

    public void setTransformTarget(Vector3 pos)
    {
        posTarget = new Vector3();
        posTarget = pos;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


 
    

    void Update()
    {
        
        obj.enabled = !enable;
        agent.enabled = enable;
        if (!agent.enabled && enable)
        {
            agent.SetDestination(posTarget);
        }
        
        agent.enabled = enable;
        
        
    }
}
