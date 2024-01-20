using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class KaidanC : MonoBehaviour
{
    
    [SerializeField] Transform[] Pos;
    int destPoint = 0;
    public int DESTPOINT {
        set {
            this.destPoint = value;
        }
        get {
            return this.destPoint;
        }
    }
    float speed = 4.0f;
    bool up = false;
    public bool UP {
        set {
            this.up = value;
        }
        get {
            return this.up;
        }
    }
    bool down = false;
    public bool DOWN {
        set {
            this.down = value;
        }
        get {
            return this.down;
        }
    }

    public bool ka = false;
    public bool KA {
        set {
            this.ka = value;
        }
        get {
            return this.ka;
        }
    }

    bool stop = false;
    public bool STOP {
        set {
            this.stop = value;
        }
        get {
            return this.stop;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Pos[destPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(up) {
            UpMove();
        }
        if(down) {
            DownMove();
        }
      
        
        GotoNextPoint(destPoint);
        
  
        
    }

    void UpMove() {
        if(this.transform.position == Pos[destPoint].transform.position) {
            //Debug.Log("����");

            if(destPoint >= Pos.Length - 1) {
                //destPoint = 0;
                //slope = false;
                
               
                up = false;
                stop = true;
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
                stop = true;
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
        if(stop) {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);

        } else {
            this.transform.rotation = Quaternion.Euler(0, 180f, 0);
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, Pos[c].transform.position, step);
        }
    }
}
