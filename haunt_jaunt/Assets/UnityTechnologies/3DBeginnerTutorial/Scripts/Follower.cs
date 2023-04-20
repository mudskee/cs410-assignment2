using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent navMeshAgent;
    private void Update()
    {
        Vector3 direction = Vector3.Normalize(player.position - transform.position);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float dot = Vector3.Dot(forward,direction);
        Debug.Log(dot);
        if(dot>.7f)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }


    }
}
