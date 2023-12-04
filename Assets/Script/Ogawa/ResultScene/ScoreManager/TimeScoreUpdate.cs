using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScoreUpdate : MonoBehaviour
{
    //�����̉摜
    [SerializeField] private Sprite[] numberImage;
    //�����̔z�u�ʒu
    [SerializeField] private Image[] imageNumber;
    //�J���}�̔z�u
    //[SerializeField] private Image[] commaImage;

    //�X�V�O�̃X�R�A
    private int oldScore = 0;
    //���݂̃X�R�A
    private int _timeScore = 5000;

    private bool delayComplete = false;

    private float _currentDisplayScore = 0f;
    [SerializeField] SlideUIControl slide;

    // Start is called before the first frame update
    void Start()
    {
        
        //���l�̏�����
        for (int count = 0; count < imageNumber.Length; count++)
        {
            imageNumber[count].sprite = numberImage[0];
            //�X�R�A�������\��(0,000)�ɂ���
            foreach (Image imageNum in imageNumber)
            {
                imageNum.enabled = false;
            }
            //if (count < 3) imageNumber[count].enabled = true;
            //if (count >= 4) imageNumber[count].enabled = false;
        }

        //�R���}�\���̏�����
        //commaImage[1].enabled = false;
       

    }

    private void Update()
    {
        

        if (slide.GAMECLEAR)
        {
            StartCoroutine(DelayedStart());
            if (delayComplete)
            {
                
                UpdateScore();
                ShowScore((int)_currentDisplayScore);
            }
        }

    }
    //�e�X�g�p
    private void FixedUpdate()
    {
        //Debug.Log("�X�R�A�F" + _score);
        //Debug.Log("OldScore�F" + oldScore);
    }

    //�\���X�R�A�̍X�V
    public void UpdateScore()
    {
        if (_currentDisplayScore == _timeScore) return;
        float localOldScore = _currentDisplayScore;
        if (_timeScore - _currentDisplayScore < 0) _currentDisplayScore += (float)_timeScore - _currentDisplayScore;
        else
        {
            //0.3�b�ŃX�R�A���Z�̉��o���I��点�邽�߂̕ϐ�  
            float _scoreMaxSec = 0.333f;
            //�P�����Z���Ă����悤�ɂ��邽�߂̉��Z���@
            localOldScore += (_timeScore - oldScore) / (_scoreMaxSec / Time.deltaTime);
            _currentDisplayScore = (int)localOldScore;
        }
        localOldScore = _currentDisplayScore;

        for (int count = 0; count < imageNumber.Length; count++)
        {
            int showNum = (int)localOldScore % 10;
            //Debug.Log(showNum);

            imageNumber[count].sprite = numberImage[(int)showNum];
            localOldScore /= 10;
        }
    }

    //�\������X�R�A�̌����E�R���}�̕\�����X�V
    public void ShowScore(int score)
    {
        int calcScore = score;
        int nums = 0;

        while (calcScore > 0)
        {
            imageNumber[nums].enabled = true;
            calcScore /= 10;
            if (nums >= 6)
            {
                Debug.Log(score);
                //commaImage[1].enabled = true;
            }
            nums++;
        }
        if (score > 1000) return;
        for (int count = 0; count < 4; count++)
        {

            imageNumber[count].enabled = true;
        }
        ///commaImage[0].enabled = true;
    }
    IEnumerator DelayedStart()
    {
        // 3�b�ҋ@����
        yield return new WaitForSeconds(3.0f);

        // �J�n��x�点�鏈��
        delayComplete = true;
    }
}