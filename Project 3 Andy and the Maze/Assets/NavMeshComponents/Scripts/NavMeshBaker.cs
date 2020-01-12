using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface[] navMeshSurfaces;

    void Start()
    {
        for(int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
