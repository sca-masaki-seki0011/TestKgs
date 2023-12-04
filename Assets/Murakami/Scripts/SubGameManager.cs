using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubGameManager : MonoBehaviour
{
    //ポーズ中ならfalseになり、この時はすべてのオブジェクトは停止する
    private bool plaingGame = true;
    //ポーズUI
    [SerializeField] private GameObject pousePanel;
    //ポーズに行くときのフェードイメージ
    [SerializeField] private Image pouseFade;

    //プロパティ
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
