using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubGameManager : MonoBehaviour
{
    //�|�[�Y���Ȃ�false�ɂȂ�A���̎��͂��ׂẴI�u�W�F�N�g�͒�~����
    private bool plaingGame = true;
    //�|�[�YUI
    [SerializeField] private GameObject pousePanel;
    //�|�[�Y�ɍs���Ƃ��̃t�F�[�h�C���[�W
    [SerializeField] private Image pouseFade;

    //�v���p�e�B
    public bool PlaingGame
    {
        get { return this.plaingGame;}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
