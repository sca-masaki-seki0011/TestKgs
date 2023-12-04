using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverIcon : MonoBehaviour
{
    RectTransform my;
    [SerializeField] RectTransform[] gameOverPos;
    // Start is called before the first frame update
    void Start()
    {
        my = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        IconMove();
        if(my.localPosition == gameOverPos[0].localPosition) {
            if(Gamepad.current.bButton.wasPressedThisFrame) {
                TitleManager.sceneName = "PlayScene";
                SceneManager.LoadScene("LoadScene");
            }
          
        }
        else if(my.localPosition == gameOverPos[1].localPosition) {
            if(Gamepad.current.bButton.wasPressedThisFrame) {
                TitleManager.sceneName = "Masaki";
                 SceneManager.LoadScene("LoadScene");
            }
        }
    }

    void IconMove() {
        if(Gamepad.current.leftStick.left.wasReleasedThisFrame) {
            my.localPosition = gameOverPos[0].localPosition;

        }
        if(Gamepad.current.leftStick.right.wasReleasedThisFrame) {
            my.localPosition = gameOverPos[1].localPosition;

        }
    }
}
