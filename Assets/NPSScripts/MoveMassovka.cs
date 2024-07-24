using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MoveMassovka : MonoBehaviour
{
    private NavMeshAgent agent;

    public Vector3[] _pathAgent;

    private int targetMove = 0;

    public bool isCustomer;


    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        agent.Warp(transform.position);

        agent.speed = Random.Range(1f, 3.1f);

        GetComponent<Animator>().SetBool("IsMoving", true);
    }

    void Update()
    {
            if (Vector3.Distance(transform.position, _pathAgent[targetMove]) < 1)
            {
                if (_pathAgent.Length > (targetMove + 1))
                {
                    targetMove++;
                }
                else
                {

                        DestroyScript();
                }
            }
            else
            {
                agent.destination = _pathAgent[targetMove];
        }
    }
    private void DestroyScript()
    {
        if (isCustomer == false)
        {
            GameObject.Find("Spawner").GetComponent<SpawnerNPS>()._countSpawnedPeople -= 1;
            Destroy(gameObject);
        }
        else
        {
            gameObject.AddComponent<CustomerMoveScript>();
            Destroy(this);
        }
        
    }
}
