using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject easy;
    [SerializeField] GameObject normal;
    [SerializeField] GameObject hard;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        //start�{�^���������ꂽ��start�{�^���������ē�Փx�I���{�^�����o���B
        start.gameObject.SetActive(false);
        easy.gameObject.SetActive(true);
        normal.gameObject.SetActive(true);
        hard.gameObject.SetActive(true);


    }

    public void SelectButton()
    {
        //�V�[���J��
        if(easy)
        {
            //�t�F�[�h�A�E�g������
            FadeOut();
            SceneManager.LoadScene("");
        }

        if(normal)
        {
            FadeOut();
            SceneManager.LoadScene("");
        }
        
        if(hard)
        {
            FadeOut();
            SceneManager.LoadScene("");
        }
    }

    private void FadeOut()
    {

    }
}
