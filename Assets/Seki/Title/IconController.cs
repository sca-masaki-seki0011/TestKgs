using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IconController : MonoBehaviour
{
    RectTransform my;
    [SerializeField] RectTransform[] Title;
    bool stageFlag = false;
    public bool STAGE {
        set {
            this.stageFlag = value;
        }
        get {
            return this.stageFlag;
        }
    }
  
    [SerializeField] GameObject StageSelectUI;


    // Start is called before the first frame update
    void Start()
    {
        my = this.GetComponent<RectTransform>();

        StageSelectUI.SetActive(false);
        my.localPosition = Title[0].localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stageFlag) {
            IconPos();
        }
        


        if(my.localPosition == Title[0].localPosition) {
            if(Gamepad.current.bButton.wasReleasedThisFrame) {
                stageFlag = true;
                StageSelectUI.SetActive(true);
            }
        }

        if(my.localPosition == Title[1].localPosition) {
            if(Gamepad.current.bButton.wasReleasedThisFrame) {
                //stageFlag = true;
                
            }
        }

    }


    /// <summary>
    /// �^�C�g����Icon�𓮂����֐�
    /// </summary>
    void IconPos() {
        if(Gamepad.current.leftStick.up.wasReleasedThisFrame) {
            my.localPosition = Title[0].localPosition;

        }
        if(Gamepad.current.leftStick.down.wasReleasedThisFrame) {
            my.localPosition = Title[1].localPosition;

        }
    }
}
