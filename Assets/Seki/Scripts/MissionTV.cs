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
        //�ʏ��
        if(Input.GetKeyDown(KeyCode.X)) {
            tvObj[0].SetActive(true);
            tvObj[1].SetActive(false);
        }
        //�~�b�V�����̖\�����{�b�g����ǂ���������
        if(Input.GetKeyDown(KeyCode.Z)) {
            tvObj[1].SetActive(true);
            tvObj[0].SetActive(false);
        }

        //�d���̏�Ƀ{�^����z�u���Ē�d���Ƃɖ߂�
        //�~�b�V�����̒�d
        //for(int i = 0; i < tvObj.Length; i++) {
        //tvObj[i].SetActive(false);
        // }
        //tvObj[0].SetActive(true);
        //tvObj[1].SetActive(false);
    }
}
