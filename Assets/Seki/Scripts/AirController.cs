using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirController : MonoBehaviour
{
    [SerializeField] GameObject Pos;
    float speed = 20.0f;
    bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!hit) { 
        var delta = this.transform.position - Pos.transform.position;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if(delta == Vector3.zero)
            return;

        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        //var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // オブジェクトの回転に反映
        //this.transform.rotation = rotation;


        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, Pos.transform.position, step);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "air") {
            hit = true;
            Debug.Log("HITT");
        }
    }
}
