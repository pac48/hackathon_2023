using UnityEngine;
using UnityEngine.AI;

public class ParkingController : MonoBehaviour{

private NavMeshAgent agent;
    
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
        agent.enabled = enable;
        if (enable)
        {
            agent.SetDestination(posTarget);
        }

        
    }
}
