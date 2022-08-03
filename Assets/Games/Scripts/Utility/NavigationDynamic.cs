using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavigationDynamic : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private MeshRenderer[] meshesGround;


    [Button]
    public void Bake()
    {
        EnableMeshGround(true);
        navMeshSurface.BuildNavMesh();  
        EnableMeshGround(false);
    }

    private void EnableMeshGround(bool enable)
    {
        foreach (MeshRenderer mesh in meshesGround)
        {
            mesh.enabled = enable;
        }
    }

}
