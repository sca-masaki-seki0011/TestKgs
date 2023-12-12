using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] RectTransform[] Point;
    [SerializeField] RectTransform myPos;
    bool pause = false;
    public bool PAUSE {
        set {
            this.pause = value;
        }
        get {
            return this.pause;
        }
    }

    int selectCount = 0;
 
    [SerializeField] GameObject PauseUI;
    [SerializeField] RawImage PausePanel;

    float red,green,blue,alfa;
    float P_red, P_green, P_blue, P_alfa;

    float fadeSpeed = 0.04f;
    float P_fadeSpeed = 0.01f;
    bool isFadeFlag = false;

    bool check =false;
    bool playStart = false;

    [SerializeField] GameManager gameManager;

    [SerializeField] Text[] ScoreText;

    [SerializeField] MissionManager mission;
    [SerializeField] GameObject player;
    PlayerC playerC;
    PlayerInput playerInput;

    [SerializeField] Image[] KeyImage;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Point.Length; i++) {
            Point[i] = Point[i].GetComponent<RectTransform>();
        }
     
        for(int u = 0; u < KeyImage.Length; u++) {
            KeyImage[u] = KeyImage[u].GetComponent<Image>();
            KeyImage[u].color = new Color(0f,0f,0f);
        }

        playerInput = player.GetComponent<PlayerInput>();
        playerC = player.GetComponent<PlayerC>();
        PausePanel = PausePanel.GetComponent<RawImage>();
        PauseUI.SetActive(false);
        red = PausePanel.color.r;
        green = PausePanel.color.g;
        blue = PausePanel.color.b;
        alfa = PausePanel.color.a;
        //alfa = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText[0].text = "×"+gameManager.ManagerRemain.ToString();
        ScoreText[1].text = "×" + gameManager.GETCOIN.ToString();

        if(playerC.KEYCOUNT != 0) {
            KeyImage[playerC.KEYCOUNT -1].color = new Color(255f,255f,255f);
        }
        
        //ポーズ中
        if(pause) {
           
            if(selectCount > 0) {
                if(Gamepad.current.leftStick.up.wasPressedThisFrame) {
                    selectCount--;
                    myPos.localPosition = Point[selectCount].localPosition;
                }
            }

            if(selectCount < 2) {
                if(Gamepad.current.leftStick.down.wasPressedThisFrame) {

                    selectCount++;
                    myPos.localPosition = Point[selectCount].localPosition;
                }
               
            }

           if(myPos.localPosition == Point[0].localPosition) {
                if(Gamepad.current.bButton.isPressed) {
                    isFadeFlag = true;
                    check = true;
                    playStart = true;
                    playerInput.enabled = true;
                }
           }

           if(myPos.localPosition == Point[2].localPosition) {
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    TitleManager.sceneName = "Masaki";
                    SceneManager.LoadScene("LoadScene");
                }
            }
            
        } 
        //ポーズ中じゃない
        else if(!mission.MISSIONFLAG) {//
            if(Gamepad.current.startButton.isPressed) {
                pause = true;
                isFadeFlag = true;
                check = false;
                playerInput.enabled = false;
            }
        }


        
    }

    private void FixedUpdate() {
        if(isFadeFlag) {
            StartFadeOut();
        } else if(!isFadeFlag && pause) {
            StartFadeIn();
        }
    }

    void StartFadeOut() {
            alfa += fadeSpeed;
            SetAlpha();
           if(alfa >= 1f) {
            StartFadeIn();
        }
           
    }


    void StartFadeIn() {
        if(alfa > 0f) {
            if(check) {
                if(alfa >= 0.55f) {
                    PauseUI.SetActive(false);
                } 
                
            }
            if(!check) {
               
                PauseUI.SetActive(true);
            }
            alfa -= P_fadeSpeed;
            SetAlpha();
            isFadeFlag = false;
        }
        
        if(alfa <= 0f && playStart) {
            pause = false;
            playStart = false;
        }
        
    }

    void SetAlpha() {
        PausePanel.color = new Color(red, green, blue, alfa);
    }
}
