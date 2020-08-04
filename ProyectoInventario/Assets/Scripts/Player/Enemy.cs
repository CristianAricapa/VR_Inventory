using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Agent;
    public GameObject Target;
    private Vector3 FinalPos;


    // Use this for initialization
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Target = GameObject.Find("Room");
        Agent.speed = Random.Range(3f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.EnemyManager.EnemyCanMove == true)
        {
            FinalPos = Target.transform.position;
            Agent.SetDestination(FinalPos);
        }

    }


}
