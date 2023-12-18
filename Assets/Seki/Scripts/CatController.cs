using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private NavMeshAgent agent;
    private int destPoint = 0;
    [SerializeField] private GameObject[] m_gameObject;
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

    bool syukai = false;
    bool bareta = false;

    Vector3 pos;
   

    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<PlayerInput>();
        playerC = playerObj.GetComponent<PlayerC>();
        agent = GetComponent<NavMeshAgent>();
        pos = this.transform.position ;
    }


    float step;
    bool rote = false;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(bareta);
        /*
        if(!bareta&& !syukai) {
            if(!agent.pathPending && agent.remainingDistance < 0.3f) {

                GotoNextPoint();
           }
       }
        */
        /*
        if(bareta && !rote) {
            // 移動量を計算
            var delta = this.transform.position - m_gameObject[0].transform.position;

            // 静止している状態だと、進行方向を特定できないため回転しない
            if(delta == Vector3.zero)
                return;

            // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
            var rotation = Quaternion.LookRotation(delta, Vector3.up);

            // オブジェクトの回転に反映
            this.transform.rotation = rotation;
            rote = true;
        }
        */
        if(bareta && !syukai) {

            agent.destination = m_gameObject[0].transform.position;

            //pos.x = speed * Time.deltaTime;
            //this.transform.position = pos;
            //step = speed*Time.deltaTime;
            //transform.position = Vector3.Lerp(this.transform.position, m_gameObject[0].transform.position, step);
            //this.transform.position = Vector3.MoveTowards(this.transform.position, m_gameObject[0].transform.position, step);
            //syukai = true;
        }

        
        if(bareta && this.transform.position == m_gameObject[0].transform.position) {
            Debug.Log("ついた");
            //syukai = true;
            agent.enabled = false;
            StartCoroutine(NotSyukai());
        }
        
        /*
        if(syukai) {
            
            if(dirastionCount != -1 && !stop) {
                CatMoveCount(CatPos());
            }

            if(dirastionCount == 4) {
                player.enabled = true;
                playerC.MISSIO = true;
                missionManager.MISSIONVALUE[missionManager.RADOMMISSIONCOUNT]++;
                missionManager.KeyActive(missionManager.RADOMMISSIONCOUNT);
                StartCoroutine(WaitNotActive());
            }
            CatStop();
        } 
        */
    }
    IEnumerator NotSyukai() {
        yield return new WaitForSeconds(0.1f);
        syukai = true;
    }

    void CatStop() {
        if(dirastionCount != -1 && this.transform.position == m_gameObject[3].transform.position) {
            Debug.Log("拠点3");
            stop = true;
            StartCoroutine(WaitStop());
        }
        if(dirastionCount != -1 && this.transform.position == m_gameObject[0].transform.position) {
            Debug.Log("拠点0");
            stop = true;
            StartCoroutine(WaitStop());
        }
        if(dirastionCount != -1 && this.transform.position == m_gameObject[1].transform.position) {
            Debug.Log("拠点1");
            stop = true;
            StartCoroutine(WaitStop());
        }
        if(dirastionCount != -1 && this.transform.position == m_gameObject[2].transform.position) {
            Debug.Log("拠点2");
            stop = true;
            StartCoroutine(WaitStop());
        }
    }
    
    IEnumerator WaitNotActive() {
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }

    IEnumerator WaitStop() {
        yield return new WaitForSeconds(1.0f);
        dire = 0;
        dirastionCount = -1;
        player.enabled = true;
    }

    void GotoNextPoint() {

        if(points.Length == 0) {
            
            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

    }

    void CatMoveCount(int c) {
        
        switch(c) {
            case 1:
                if(dirastionCount == 0 || dirastionCount == 1) {
                    CatMovbe(3);
                }
                if(dirastionCount == 2) {
                    CatMovbe(1);
                }
                break;
            case 2:
                if(dirastionCount == 0 || dirastionCount == 1) {
                    CatMovbe(0);
                }
                if(dirastionCount == 2) {
                    CatMovbe(2);
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
        // 移動量を計算
        var delta = this.transform.position - m_gameObject[c].transform.position;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if(delta == Vector3.zero)
            return;

        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // オブジェクトの回転に反映
        this.transform.rotation = rotation;
        
       
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, m_gameObject[c].transform.position, step);
        
    
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "cateye") {
            Debug.Log("見つかった");
            bareta = true;
        }
    }
}
