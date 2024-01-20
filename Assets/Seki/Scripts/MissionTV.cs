using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTV : MonoBehaviour
{
    [SerializeField] GameObject[] tvObj;

    // Start is called before the first frame update
    void Start() {

        tvObj[0].SetActive(true);
        tvObj[1].SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //通常画
        if(Input.GetKeyDown(KeyCode.X)) {
            tvObj[0].SetActive(true);
            tvObj[1].SetActive(false);
        }
        //ミッションの暴走ロボットから追いかけられる
        if(Input.GetKeyDown(KeyCode.Z)) {
            tvObj[1].SetActive(true);
            tvObj[0].SetActive(false);
        }

        //電灯の上にボタンを配置して停電もとに戻る
        //ミッションの停電
        //for(int i = 0; i < tvObj.Length; i++) {
        //tvObj[i].SetActive(false);
        // }
        //tvObj[0].SetActive(true);
        //tvObj[1].SetActive(false);
    }
}
