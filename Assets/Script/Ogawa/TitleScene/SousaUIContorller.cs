using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SousaUIContorller : MonoBehaviour
{
    //�y�[�W�̕ϐ�
    int Pagecount = 0;
    //�\������Q�[�����
    [SerializeField] GameObject[] GameImage;
    //�y�[�W����������UI
    [SerializeField] Image[] NextImage;

    [SerializeField] GameObject _loadingUI;

    [SerializeField] private Slider _slider;

    [SerializeField] private Text _text;

    // Start is called before the first frame update

    void Start()
    {
        //�\������Q�[����ʂ̏�����
        for(int i = 0; i< GameImage.Length; i++) {
            GameImage[i].SetActive(false);
        }

        
    }

    public void LoadNextScene() {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }

    //�������ǂ���
    IEnumerator LoadScene() {
        AsyncOperation async = SceneManager.LoadSceneAsync(TitleManager.sceneName);
        //async.allowSceneActivation = false;
        while(!async.isDone) {
            _slider.value = async.progress;

            _text.text = (async.progress * 100).ToString() + "%";

            Pagecount = (int)async.progress%5;
            if(async.progress >= 0.9f) {
                _text.text = "100%";
                //yield return new WaitForSeconds(3.0f);
                async.allowSceneActivation = true;
            }
            yield return null;
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        LoadNextScene();
        ImageNext(Pagecount);

    }

    /// <summary>
    /// //Image�̐F���\���A�\���Ȃǂ��s���֐�
    /// </summary>
    /// <param name="count">�y�[�W��</param>
    void ImageNext(int count) {
        for(int i = 0; i < NextImage.Length; i++) {
            if(i == count) {//�y�[�W���������Ȃ�
                GameImage[i].SetActive(true);
                NextImage[i].color = new Color(255,0,0);
            } else {
                GameImage[i].SetActive(false);
                NextImage[i].color = new Color(255, 255, 255);
            }
        }
    }
}
