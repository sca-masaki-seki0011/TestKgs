using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CatController : MonoBehaviour
{
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
    [SerializeField] PlayerInput player;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = m_gameObject[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(dirastionCount);
        if(dirastionCount != -1) { 
        if(this.transform.position == m_gameObject[0].transform.position) {
             Debug.Log("拠点");
             if(dirastionCount == 0 || dirastionCount == 1) {

                CatMovbe(1);
             }
             
             if(dirastionCount == 2) {
                CatMovbe(3);
            }
        }
        }
        /*
        if(this.transform.position == m_gameObject[1].transform.position) {
            if(dirastionCount == 0 || dirastionCount == 1) {
                CatMovbe(0);
            }
            if(dirastionCount == 2) {
                CatMovbe(2);
            }
        }
        if(this.transform.position == m_gameObject[2].transform.position) {
            if(dirastionCount == 0 || dirastionCount == 1) {
                CatMovbe(1);
            }
         
        }
        if(this.transform.position == m_gameObject[3].transform.position) {
            if(dirastionCount == 0 || dirastionCount == 1) {
                //CatMovbe(0);
            }
        
        }
        //CatMovbe(dirastionCount);
        */
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
        StartCoroutine(WaitFlag());
        
    }

    IEnumerator WaitFlag() {
        yield return new WaitForSeconds(3.0f);
        player.enabled = true;
    }
}
