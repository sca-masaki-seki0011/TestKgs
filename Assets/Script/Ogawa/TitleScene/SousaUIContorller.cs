using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SousaUIContorller : MonoBehaviour
{
    //ページの変数
    int Pagecount = 0;

    //ステージ関係画面
    [SerializeField] GameObject[] stageImage;

    //操作説明画面
    [SerializeField] GameObject[] gameImage;
    //ページ数が分かるUI
    [SerializeField] Image[] nextImage;

    //ロードの全体のオブジェクト
    [SerializeField] GameObject _loadingUI;

    [SerializeField] GameObject okText;

    [SerializeField] GameObject sliderBar;
    Slider _slider;

    [SerializeField] private Text _text;

    float tipsTime;

    bool tips = false;

    int randomtips = 0;
    AsyncOperation async;

   
    // Start is called before the first frame update

    void Start()
    {
        okText.SetActive(false);
        
        _slider = sliderBar.GetComponent<Slider>();
        //表示するゲーム画面の初期化
        for(int i = 0; i< gameImage.Length; i++) {
            gameImage[i].SetActive(false);
        }  
        
        for(int u = 0; u < stageImage.Length; u++) {
            stageImage[u].SetActive(false);
        }
        randomtips = Random.Range(0,2);
        LoadNextScene();
    }

    public void LoadNextScene() {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }
    
    bool ok = false;

    //ここ改良する
    IEnumerator LoadScene() {
        yield return null;
        async = SceneManager.LoadSceneAsync(TitleManager.sceneName);
        async.allowSceneActivation = false;
        while(!async.isDone) {
            _slider.value = async.progress;

            _text.text = (async.progress * 100).ToString() + "%";

            //Pagecount = (int)async.progress%5;
            if(async.progress >= 0.9f) {
                _text.text = "100%";
                
                sliderBar.SetActive(false);
                StartCoroutine(Waitok());
                if(ok && Gamepad.current.bButton.wasPressedThisFrame) {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    IEnumerator Waitok() {
        yield return new WaitForSeconds(1.0f);
        _text.enabled = false;
        okText.SetActive(true);
        ok = true;
    }


    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(Pagecount);
   
        if(!tips) {
            tipsTime += Time.deltaTime;
            if((int)tipsTime != 0 && (int)tipsTime % 5 == 0) {
                tips = true;
            }
        }
       
        if(tips) {
        
            tips = false;
            if(Pagecount == 4) {
                Pagecount = 0;
            } else {
                Pagecount++;
            }
            tipsTime = 0f;
           
        }
        ImageNext(Pagecount, randomtips);


    }

    /// <summary>
    /// //Imageの色や非表示、表示などを行う関数
    /// </summary>
    /// <param name="count">ページ数</param>
    void ImageNext(int count,int random) {
        for(int i = 0; i < nextImage.Length; i++) {
            if(i == count) {//ページ数が同じなら
                if(random == 0) {
                    gameImage[count].SetActive(true);
                } else {
                    stageImage[count].SetActive(true);
                }
                
                nextImage[count].color = new Color(255,0,0);
            } else {
                if(random == 0) { 
                    gameImage[i].SetActive(false);
                }
                else {
                    stageImage[i].SetActive(false);
                }
                nextImage[i].color = new Color(255, 255, 255);
            }
        }
    }
}
