using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyControllert : MonoBehaviour
{
    //[SerializeField] AreaController bo;

    //�g�����v�������񂷂�|�C���g
    [SerializeField] private Transform[] points;
    //���񂷂�|�C���g����߂�ϐ�
    private int destPoint = 0;
    //NavMeshAgent�Ƃ���AI�@�\��g���ϐ�
    private NavMeshAgent agent;

    bool r = false;
    Animator anim;
    bool playerHit = false;
    [SerializeField] GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //bo = bo.GetComponent<AreaController>();
        destPoint = Random.Range(0, points.Length);
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerHit) {
            if(!agent.pathPending && agent.remainingDistance < 0.5f) {
            
                //GotoNextPoint();
            }
      
        }
        if(playerHit) {
            transform.DOLookAt(playerObj.transform.position,1.0f);
            anim.SetTrigger("attack");
            playerHit = false;
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

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            //if(//ここにプレイヤーの回避モーション中だったらの条件が入る)
            //回避モーション中だったら自分を消す
            //else
            playerHit = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.tag == "Player") {
            playerHit = false;
        }
    }
}
