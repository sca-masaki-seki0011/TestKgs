using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllert : MonoBehaviour
{
    [SerializeField] AreaController bo;

    //�g�����v�������񂷂�|�C���g
    [SerializeField] private Transform[] points;
    //���񂷂�|�C���g����߂�ϐ�
    private int destPoint = 0;
    //NavMeshAgent�Ƃ���AI�@�\��g���ϐ�
    private NavMeshAgent agent;

    bool r = false;

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
            if(!agent.pathPending && agent.remainingDistance < 0.5f) {
            
                GotoNextPoint();
            }
      
        }
       
    }

    //�p�j����֐�
    void GotoNextPoint() {
       
        if(points.Length == 0) {
            return;
        }

        //ene.SetBool("walk", false);
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

        
    }
}
