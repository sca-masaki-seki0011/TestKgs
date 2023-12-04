using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameC : MonoBehaviour
{
    [Header("入力された文字を表示するImage"),SerializeField]
    private Image[] inputSpellImages;
    [Header("文字配列"),SerializeField]
    private Image[] spellImages;
    [Header("選択中カーソル"),SerializeField]
    private Image[] selectSpellCursolImage;
 
    //入力された文字たち
    private string inputName = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
