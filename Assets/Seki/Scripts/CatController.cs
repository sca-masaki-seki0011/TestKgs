using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CatController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_gameObject;
    float speed = 5.0f;
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

    int dire = 0;
    bool stop = false;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("�J�E���g"+ dirastionCount+ "����"+dire);
        if(dirastionCount != -1 && !stop) {
            CatMoveCount(CatPos());
        }


        if(this.transform.position == m_gameObject[3].transform.position) {
            Debug.Log("���_1");
            
        }
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
        // �ړ��ʂ��v�Z
        var delta = this.transform.position - m_gameObject[c].transform.position;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if(delta == Vector3.zero)
            return;

        // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // �I�u�W�F�N�g�̉�]�ɔ��f
        this.transform.rotation = rotation;
        /*�����̓������ł�[�Ղł���Ă݂�
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, m_gameObject[c].transform.position, step);
        */
        StartCoroutine(WaitFlag());
    
    }

    IEnumerator WaitFlag() {
        
        yield return new WaitForSeconds(1.01f);
        stop = true;
        dire = 0;
        dirastionCount = -1;
        player.enabled = true;
    }
}
