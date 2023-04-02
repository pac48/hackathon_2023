using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

using NavMeshPlus.Components;
public class NavigationBaker : MonoBehaviour {

    public NavMeshSurface[] surfaces; 
    private int ind = 0;

    // Use this for initialization
    void Update ()
    {
        ind += 1;
        if (ind == 30)
        {
            for (int i = 0; i < surfaces.Length; i++) 
            {
                surfaces [i].BuildNavMesh ();    
            }

            ind = 0;
        }

            
    }

}