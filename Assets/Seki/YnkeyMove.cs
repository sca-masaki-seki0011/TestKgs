using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YnkeyMove : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private NavMeshAgent agent;
    private int destPoint = 0;
    [SerializeField] AreaController bo;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bo = bo.GetComponent<AreaController>();
        destPoint = Random.Range(0, points.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(!bo.HIT) {
            if(!agent.pathPending && agent.remainingDistance < 0.3f) {

                GotoNextPoint();
            }
        }
    }

    void GotoNextPoint() {

        if(points.Length == 0) {

            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

    }
}
