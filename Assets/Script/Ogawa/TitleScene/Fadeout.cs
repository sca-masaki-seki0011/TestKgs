using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
using UnityEngine.SceneManagement;

public class Fadeout : MonoBehaviour
{
    [SerializeField] GameObject easy;
    [SerializeField] GameObject normal;
    [SerializeField] GameObject hard;

    [SerializeField] GameObject SousaUI;

    float fadeSpeed = 0.001f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理

    private bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
    private string changeSceneName; // フェードアウト処理後、シーン遷移する場合のシーン名


    Image fadeImage;                //透明度を変更するパネルのイメージ

    void Start()
    {

        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;

        SousaUI.gameObject.SetActive(false);
}

    void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    void StartFadeIn()//今のところ使わない
    {
        alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
            //ここまで来たら操作説明を表示
            SousaUI.gameObject.SetActive(true);

            /*if (changeSceneName != "")
            {
                Debug.Log(changeSceneName + "に遷移します");
                SceneManager.LoadScene(changeSceneName);
            }*/
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    //　スタートボタンを押したら実行する
    public void GameStart()
    {
        easy.gameObject.SetActive(false);
        normal.gameObject.SetActive(false);
        hard.gameObject.SetActive(false);
        isFadeOut = true;

        changeSceneName = "PlayScene";//ゲームシーンを""の中に入れる   
    }

}


