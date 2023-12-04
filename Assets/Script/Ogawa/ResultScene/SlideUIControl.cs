using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideUIControl : MonoBehaviour
{
    public int state = 0;
    public bool loop = false;

    bool GameClear = false;
    public bool GAMECLEAR {
        set {
            this.GameClear = value;
        }
        get {
            return this.GameClear;
        }
    }

    [Header("Text")]
    public Vector3 outPos01;
    public Vector3 inPos;
    public Vector3 outPos02;


    /*
    // �V���O���g���C���X�^���X
    private static SlideUIControl instance;

    // �C���X�^���X�ɃA�N�Z�X���邽�߂̃v���p�e�B
    public static SlideUIControl Instance
    {
        get
        {
            // �C���X�^���X���܂��쐬����Ă��Ȃ��ꍇ�͐V�����쐬
            if (instance == null)
            {
                instance = FindObjectOfType<SlideUIControl>();

                // �V�[�����ɑ��݂��Ȃ��ꍇ�̓G���[���O��\��
                if (instance == null)
                {
                    Debug.LogError("SlideUIControl �C���X�^���X��������܂���ł����B");
                }
            }

            return instance;
        }
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //����m�F�p
        if (Input.GetKeyDown("space"))
        {
            
        }
        */
       

        if (GameClear)
        {
            if (state == 1)
            {
                if (transform.localPosition.x > inPos.x - 1.0f &&
                    transform.localPosition.y > inPos.y - 1.0f &&
                    transform.localPosition.z > inPos.z - 1.0f)
                {
                    transform.localPosition = inPos;
                }

                else
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, 4.0f * Time.unscaledDeltaTime);
                }
            }
        }

    }

    public bool IsGamePaused
    {
        get { return GameClear; }
    }

    // �Z�b�^�[
    public void SetGamePaused(bool value)
    {
        GameClear = value;
    }
}
