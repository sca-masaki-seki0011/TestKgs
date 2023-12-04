using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScoreManager : MonoBehaviour
{
    [Header("�c�@"),SerializeField] 
    private int _remain;
    //�X�R�A
    private int _score = 0;
    //�l�����z
    private int _money = 0;

    public int Remain
    {
        get { return _remain;}
    }

    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�c�@�A�X�R�A�A���z�̍X�V
    public void ChangeScores(int updateRemain, int updateScore, int updateMoney)
    {
        _remain = updateRemain;
        _score = updateScore;
        _money = updateMoney;
    }
}
