using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SousaUIContorller : MonoBehaviour
{
    //�y�[�W�̕ϐ�
    int Pagecount = 0;

    //�X�e�[�W�֌W���
    [SerializeField] GameObject[] stageImage;

    //����������
    [SerializeField] GameObject[] gameImage;
    //�y�[�W����������UI
    [SerializeField] Image[] nextImage;

    //���[�h�̑S�̂̃I�u�W�F�N�g
    [SerializeField] GameObject _loadingUI;

    [SerializeField] GameObject okText;

    [SerializeField] GameObject sliderBar;
    Slider _slider;

    [SerializeField] private Text _text;

    float tipsTime;

    bool tips = false;

    int randomtips = 0;
    AsyncOperation async;

    [SerializeField] GameObject playerObj;
    Vector3 pos;
    Animator playeranim;
    // Start is called before the first frame update

    void Start()
    {
        pos = playerObj.transform.position;
        playeranim = playerObj.GetComponent<Animator>();
        okText.SetActive(false);
        
        _slider = sliderBar.GetComponent<Slider>();
        //�\������Q�[����ʂ̏�����
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

    //�������ǂ���
    IEnumerator LoadScene() {
        yield return null;
        async = SceneManager.LoadSceneAsync(TitleManager.sceneName);
        async.allowSceneActivation = false;
        while(!async.isDone) {
            _slider.value = async.progress*100;

            _text.text = (async.progress * 100).ToString() + "%";

            //Pagecount = (int)async.progress%5;
            if(async.progress >= 0.9f) {
                _slider.value = 100.0f;
                _text.text = "100%";
                
                StartCoroutine(Waitok());
                if(Gamepad.current.bButton.wasPressedThisFrame) {//ok && 
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    IEnumerator Waitok() {
        yield return new WaitForSeconds(1.0f);
        sliderBar.SetActive(false);
        _text.enabled = false;
        okText.SetActive(true);
        playerObj.SetActive(false);
        ok = true;
    }

    bool move = false;

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(Pagecount);
    
        if(_slider.value != 0 && _slider.value % 10 == 0 && _slider.value < 100.0f) {
           move = true; 
        }

        if(move) {
            pos.x += 1.8f;
            playerObj.transform.position = pos;
            move = false;
        }
   
        if(!tips) {
            tipsTime += Time.deltaTime;
            if((int)tipsTime != 0 && (int)tipsTime % 5 == 0) {
                tips = true;
            }
        }
       
        if(_slider.value >= 100.0f) {
            playeranim.SetBool("Fall",true);
        }

        if(tips) {
            //_slider.value += 10f;//�f�o�b�N�悤�ɃX���C�_�[�̒l��������������
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
    /// //Image�̐F���\���A�\���Ȃǂ��s���֐�
    /// </summary>
    /// <param name="count">�y�[�W��</param>
    void ImageNext(int count,int random) {
        for(int i = 0; i < nextImage.Length; i++) {
            if(i == count) {//�y�[�W���������Ȃ�
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
