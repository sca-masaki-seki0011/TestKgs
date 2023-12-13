using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SibariTukeru : MonoBehaviour
{
    [SerializeField] RectTransform mypos;
    [SerializeField] RectTransform[] handan;
    [SerializeField] GameObject sibari;
    bool sibariFlag = false;

    bool no = false;

    public bool NO {
        set {
            this.no = value;
        }
        get {
            return this.no;
        }
    }

    public bool SIBARIFLAG {
        set {
            this.sibariFlag = value;
        }
        get {
            return this.sibariFlag;
        }
    }
    SibariUIController sibariKind;
    [SerializeField] TitleManager titleManager;
    // Start is called before the first frame update
    void Start()
    {
        titleManager.SelectSetumei(2);
        mypos = mypos.GetComponent<RectTransform>();
        sibariKind = sibari.GetComponentInChildren<SibariUIController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.leftStick.left.wasReleasedThisFrame) {
            mypos.localPosition = handan[0].localPosition;
        }
        if(Gamepad.current.leftStick.right.wasReleasedThisFrame) {
            mypos.localPosition = handan[1].localPosition;
        }

        if(!sibariFlag) {
            if(mypos.localPosition == handan[0].localPosition) {
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    sibariFlag = true;
                    sibariKind.enabled = true;
                    sibari.SetActive(true);
                }
            }
            if(mypos.localPosition == handan[1].localPosition) {
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    //sibariFlag = true;
                    no = true;
                }
            }

            
        }
       
    }

   
}
