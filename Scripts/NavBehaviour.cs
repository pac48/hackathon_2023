using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] NavMeshAgent agent;
    [SerializeField]  Transform target;
    
    void Start()
    {
        agent.enabled = true;
        agent.SetDestination(target.position);
        
    }




    // Update is called once per frame
    void Update()
    {
       
    }
}
