using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KaidanC : MonoBehaviour
{
    
    [SerializeField] Transform[] Pos;
    int destPoint = 4;
    float speed = 4.0f;
    bool up = false;
    bool down = false;

    // Start is called before the first frame update
    void Start()
    {
       this.transform.position = Pos[destPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            up = true;
            down = false;
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            down = true;
            up = false; ;
        }

      
        if(up) {
            UpMove();
        }
        if(down) {
            DownMove();
        }
      
        
        GotoNextPoint(destPoint);
        //Debug.Log("�|�C���g"+ destPoint);
        Debug.Log("�A�b�v"+up);
        Debug.Log("�_�E��" + down);
        
    }

    void UpMove() {
        if(this.transform.position == Pos[destPoint].transform.position) {
            //Debug.Log("����");

            if(destPoint >= Pos.Length - 1) {
                //destPoint = 0;
                //slope = false;
                up = false;
            } else {
                destPoint++;
            }

        }
    }

    void DownMove() {
        if(this.transform.position == Pos[destPoint].transform.position) {
            //Debug.Log("����");

            if(destPoint == 0) {
                //destPoint = 0;
                //slope = false;
                down = false;
            } else {
                destPoint--;
            }

        }
    }

    void GotoNextPoint(int c) {
        var delta = this.transform.position - Pos[c].transform.position;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if(delta == Vector3.zero)
            return;

        // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
        //var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // �I�u�W�F�N�g�̉�]�ɔ��f
        //this.transform.rotation = rotation;


        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, Pos[c].transform.position, step);
        
        //if(Pos.Length == 0) {
        //return;
        //}

        //ene.SetBool("walk", false);
        //agent.destination = Pos[destPoint].position;
        //destPoint = (destPoint + 1) % Pos.Length;
    }
}
