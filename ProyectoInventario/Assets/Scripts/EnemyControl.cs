using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private NavMeshAgent Agent;

    public GameObject Target;


    private Vector3 FinalPos;
    private RaycastHit HitInfo;
    private Ray RayInfo;
    private Animator Anim;

    GameObject TempSphere;

    // Use this for initialization
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayInfo = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(RayInfo, out HitInfo))
            {
                FinalPos = HitInfo.point;
                Agent.SetDestination(FinalPos);
                if (TempSphere != null) TempSphere.GetComponent<Renderer>().material.color = Color.red;

                TempSphere = GameObject.CreatePrimitive((PrimitiveType)Random.Range(0, 4));
                TempSphere.transform.position = HitInfo.point;
                TempSphere.GetComponent<Renderer>().material.color = Color.green;
                Destroy(TempSphere, 1);
                Destroy(TempSphere.GetComponent<Collider>());

            }
        }

        if (Agent.isOnOffMeshLink == false)
        {
            Anim.SetBool("Fly", false);

            if (Agent.remainingDistance < 0.1f)
            {
                Anim.SetBool("Move", false);
                Anim.SetFloat("Speed", 0);
            }
            else
            {
                Anim.SetBool("Move", true);
                Anim.SetFloat("Speed", 1);
            }
        }
        else
        {
            Anim.SetBool("Fly", true);
        }
    }
}
