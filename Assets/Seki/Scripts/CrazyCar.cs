using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PathCreation;

public class CrazyCar : MonoBehaviour
{
    [SerializeField] private Transform[] Pos;
    private int destPoint = 0;
    NavMeshAgent agent;
    [SerializeField] PathCreator path;
    float P_speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }
    float d;
    // Update is called once per frame
    void Update()
    {
        
        if(!agent.pathPending && agent.remainingDistance < 0.3f) {
            GotoNextPoint();
        }
        /*
        d += P_speed * Time.deltaTime;
        this.transform.position = path.path.GetPointAtDistance(d);
        */
    }

    void GotoNextPoint() {
        if(Pos.Length == 0) {
            return;
        }

        //ene.SetBool("walk", false);
        agent.destination = Pos[destPoint].position;
        destPoint = (destPoint + 1) % Pos.Length;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "push") {
            agent.speed = 0f;
            //ミッションの達成ポイントを加算する
        }
    }
}
