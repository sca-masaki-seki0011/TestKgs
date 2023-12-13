using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


/// <summary>
/// ����̓��[�h�I�������ƂȂ������Ɏg���X�N���v�g
/// </summary>
public class StageSelectController : MonoBehaviour
{
    RectTransform myPos;
    [SerializeField] RectTransform[] Pos;
    [SerializeField] GameObject Siabritukeru;
    SibariTukeru si;

    bool normal = false;
    public bool NORMAL {
        set {
            this.normal = value;
        }
        get {
            return this.normal;
        }
    }

    StageSelectController myScripts;
    [SerializeField] TitleManager title;

    // Start is called before the first frame update
    void Start()
    {
      
        myScripts = this.GetComponent<StageSelectController>();
        myScripts.enabled = true;
        si = Siabritukeru.GetComponent<SibariTukeru>();
        si.enabled = false;
        Siabritukeru.SetActive(false);
        myPos = this.GetComponent<RectTransform>();
        for(int i = 0; i < Pos.Length; i++) {
            Pos[i] = Pos[i].GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        StageIconMove();
        StageSelect();
     
    }

    void StageIconMove() {
        if(Gamepad.current.leftStick.up.wasPressedThisFrame) {
            
            myPos.localPosition = Pos[0].localPosition;

        }

        if(Gamepad.current.leftStick.down.wasPressedThisFrame) {
       
            myPos.localPosition = Pos[1].localPosition;
        }
    }

    void StageSelect() {
        if(myPos.localPosition == Pos[0].localPosition) {
            title.SelectSetumei(0);
            if(Gamepad.current.bButton.isPressed) {
              
                normal = true;
            }
            
        }
        if(myPos.localPosition == Pos[1].localPosition) {
            title.SelectSetumei(1);
            if(Gamepad.current.bButton.isPressed) {
        
                Siabritukeru.SetActive(true);
                StartCoroutine(SibariActive());
            }
        }
    }

    IEnumerator SibariActive() {
        yield return new WaitForSeconds(0.5f);
        si.enabled = true;
        myScripts.enabled = false;
    }
}
