using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    [SerializeField] private Transform[] m_gameObject;
    float speed = 1.0f;
    int dirastionCount = -1;
    public int DIRATIONCOUNT
    {
        set {
            this.dirastionCount = value;
        }
        get {
            return this.dirastionCount;
        }
    }
    [SerializeField] GameObject playerObj;
    PlayerC playerC;
    PlayerInput player;
    [SerializeField] MissionManager missionManager;
    NavMeshAgent agent;
    int dire = 0;
    bool stop = false;
    public bool STOPCAT {
        set {
            this.stop = value;
        }
        get {
            return this.stop;
        }
    }

    private int destPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
      
        player = playerObj.GetComponent<PlayerInput>();
        playerC = playerObj.GetComponent<PlayerC>();
        destPoint = Random.Range(0, m_gameObject.Length);
        this.transform.position = m_gameObject[0].transform.position;
    
    }


    bool move = false;
    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f) {
            GotoNextPoint();
        }

            

            if(dirastionCount == 4) {
                player.enabled = true;
                playerC.MISSIO = true;
                missionManager.MISSIONVALUE[missionManager.RADOMMISSIONCOUNT]++;
                missionManager.KeyActive(missionManager.RADOMMISSIONCOUNT);
                StartCoroutine(WaitNotActive());
            }
            //CatStop();
    }

    void GotoNextPoint() {

        if(m_gameObject.Length == 0) {
            return;
        }

        //ene.SetBool("walk", false);
        agent.destination = m_gameObject[destPoint].position;
        destPoint = (destPoint + 1) % m_gameObject.Length;


    }

    //IEnumerator Waitmove() {
    //yield return new WaitForSeconds(5.0f);
    //move = false;
    //}

    IEnumerator WaitNotActive() {
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }

    /*
    void CatStop() {
      
        if(dirastionCount != -1 && this.transform.position == m_gameObject[3].transform.position) {
            Debug.Log("���_3");
            stop = true;
          
            StartCoroutine(WaitStop());
        }
        if(dirastionCount != -1 && this.transform.position == m_gameObject[0].transform.position) {
            Debug.Log("���_0");
            stop = true;
         
            StartCoroutine(WaitStop());
        }
        if(dirastionCount != -1 && this.transform.position == m_gameObject[1].transform.position) {
            Debug.Log("���_1");
            stop = true;
        
            StartCoroutine(WaitStop());
        }
        if(dirastionCount != -1 && this.transform.position == m_gameObject[2].transform.position) {
            Debug.Log("���_2");
            stop = true;
       
            StartCoroutine(WaitStop());
        }
    }
    
    

    IEnumerator WaitStop() {
        yield return new WaitForSeconds(1.0f);
        dire = 0;
        dirastionCount = -1;
        player.enabled = true;
    }


    void CatMoveCount(int c) {
      
     
        switch(c) {
            case 1:
                if(dirastionCount == 0) {
                    CatMovbe(3);
                }
                if(dirastionCount == 2 || dirastionCount == 1) {
                    CatMovbe(1);
                }
                break;
            case 2:
                if(dirastionCount == 1) {
                    CatMovbe(2);
                }
                if(dirastionCount == 0 || dirastionCount == 2) {
                    CatMovbe(0);
                }
                break;
            case 3:
                if(dirastionCount == 0 || dirastionCount == 1) {
                    CatMovbe(1);
                }
                break;
            case 4:
                if(dirastionCount == 0 || dirastionCount == 1) {
                    CatMovbe(0);
                }
                break;
        }
    }

    int CatPos() {
        if(this.transform.position == m_gameObject[0].transform.position) {
            dire = 1;
        }
        if(this.transform.position == m_gameObject[1].transform.position) {
        
            dire = 2;
        }
        if(this.transform.position == m_gameObject[2].transform.position) {
            dire = 3;
        }
        if(this.transform.position == m_gameObject[3].transform.position) {
            dire = 4;
        }
        return dire;
    }

    

    void CatMovbe(int c) {
        // �ړ��ʂ��v�Z
        var delta = this.transform.position - m_gameObject[c].transform.position;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if(delta == Vector3.zero)
            return;

        // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // �I�u�W�F�N�g�̉�]�ɔ��f
        this.transform.rotation = rotation;
        
       
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, m_gameObject[c].transform.position, step);
        
    
    }
    */
}
