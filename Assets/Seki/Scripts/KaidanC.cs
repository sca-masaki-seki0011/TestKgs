using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KaidanC : MonoBehaviour
{
    
    [SerializeField] Transform[] Pos;
    int destPoint = 1;
    float speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
       this.transform.position = Pos[destPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) { 
        //if(!agent.pathPending && agent.remainingDistance < 0.5f) {
            if(destPoint >= Pos.Length-1) {
                destPoint = 0;
            } else {
                destPoint++;
            }
            
        }
        GotoNextPoint(destPoint);
        Debug.Log("ポイント"+destPoint);
    }

    void GotoNextPoint(int c) {
        var delta = this.transform.position - Pos[c].transform.position;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if(delta == Vector3.zero)
            return;

        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // オブジェクトの回転に反映
        this.transform.rotation = rotation;


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
