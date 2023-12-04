using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //�p�l���̃C���[�W�𑀍삷��̂ɕK�v
using UnityEngine.SceneManagement;

public class Fadeout : MonoBehaviour
{
    [SerializeField] GameObject easy;
    [SerializeField] GameObject normal;
    [SerializeField] GameObject hard;

    [SerializeField] GameObject SousaUI;

    float fadeSpeed = 0.001f;        //�����x���ς��X�s�[�h���Ǘ�
    float red, green, blue, alfa;   //�p�l���̐F�A�s�����x���Ǘ�

    private bool isFadeOut = false;  //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    private bool isFadeIn = false;   //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O
    private string changeSceneName; // �t�F�[�h�A�E�g������A�V�[���J�ڂ���ꍇ�̃V�[����


    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W

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

    void StartFadeIn()//���̂Ƃ���g��Ȃ�
    {
        alfa -= fadeSpeed;                //a)�s�����x�����X�ɉ�����
        SetAlpha();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa <= 0)
        {                    //c)���S�ɓ����ɂȂ����珈���𔲂���
            isFadeIn = false;
            fadeImage.enabled = false;    //d)�p�l���̕\�����I�t�ɂ���
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;  // a)�p�l���̕\�����I���ɂ���
        alfa += fadeSpeed;         // b)�s�����x�����X�ɂ�����
        SetAlpha();               // c)�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)
        {             // d)���S�ɕs�����ɂȂ����珈���𔲂���
            isFadeOut = false;
            //�����܂ŗ����瑀�������\��
            SousaUI.gameObject.SetActive(true);

            /*if (changeSceneName != "")
            {
                Debug.Log(changeSceneName + "�ɑJ�ڂ��܂�");
                SceneManager.LoadScene(changeSceneName);
            }*/
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    //�@�X�^�[�g�{�^��������������s����
    public void GameStart()
    {
        easy.gameObject.SetActive(false);
        normal.gameObject.SetActive(false);
        hard.gameObject.SetActive(false);
        isFadeOut = true;

        changeSceneName = "PlayScene";//�Q�[���V�[����""�̒��ɓ����   
    }

}


