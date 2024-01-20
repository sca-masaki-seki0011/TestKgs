using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirController : MonoBehaviour
{
    [SerializeField] GameObject Pos;
    float speed = 20.0f;
    bool hit = false;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerC player;
    [SerializeField] Image missimage;
    [SerializeField] GameObject[] missIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ミッション"+ StopAir.count);
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
        } else {
            for(int i = 0; i < missIcon.Length; i++) { 
                missIcon[i].SetActive(false); 
            }

            missimage.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        /*
        if(collision.gameObject.tag == "air") {
            hit = true;
            gameManager.GAMEOVER = true;
            gameManager.ManagerRemain = -1;
            
            player.FALLING = true;
            Debug.Log("HITT");
        }
        */
    }
}
