using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject IconObject;
    IconController Icon;
    [SerializeField] GameObject SibaritukeruObject;
    SibariTukeru sibaritukeru;

    [SerializeField] Image FadeObj;
    float alfa;

    float fadeSpeed = 0.02f;

    [SerializeField] Hontoni hontoi;

    [SerializeField] StageSelectController stageSelect;

    public static string sceneName;

    [SerializeField] GameObject playerImage;
    [SerializeField] GameObject[] titleSetumeiText;
    [SerializeField] GameObject[] playerComent;
    [SerializeField] GameObject playerModel;
    Animator anim;

    bool fade = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = playerModel.GetComponent<Animator>();
        playerModel.SetActive(false);
        for(int t = 0; t < titleSetumeiText.Length;t++) {
            titleSetumeiText[t].SetActive(false);
            playerComent[t].SetActive(false);
        }
        playerImage.SetActive(false);
        stageSelect = stageSelect.GetComponent<StageSelectController>();
        Icon = IconObject.GetComponent<IconController>();
        Icon.enabled = false;
        FadeObj = FadeObj.GetComponent<Image>();
        alfa = FadeObj.color.a;
        sibaritukeru = SibaritukeruObject.GetComponent<SibariTukeru>();
        Invoke("InconActive",0.5f);
    }

    void InconActive() {
        Icon.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Icon.STAGE) {
            Icon.enabled = true;
            playerImage.SetActive(true);
            playerModel.SetActive(true);
            IconObject.SetActive(false);
            Icon.STAGE = false;
        }
        if(sibaritukeru.SIBARIFLAG) {
            SibaritukeruObject.SetActive(false);
            sibaritukeru.SIBARIFLAG = false;
        }

        if(sibaritukeru.NO) {
            SelectSetumei(5);
            sibaritukeru.enabled = false;
            fade = true;
            
        }

        if(hontoi.YES) {
            SelectSetumei(5);
            hontoi.enabled = false;
            
            fade = true;
            //FadeOut();
        }

        if(stageSelect.NORMAL) {
            SelectSetumei(5);
            stageSelect.enabled = false;
            fade = true;
            
            //FadeOut();
        }

        if(Gamepad.current.yButton.isPressed) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }

    private void FixedUpdate() {
        if(fade) {
            StartCoroutine(WaitFade());
        }
        
    }

    public void SelectSetumei(int count) {
        switch(count) {
            case 0:
                anim.SetBool("ganba", false);
                anim.SetBool("hazu",true);
                break;
            case 1:
                anim.SetBool("hazu", false);
                anim.SetBool("ganba", true);
                break;
            case 2:
                anim.SetBool("ganba", false);
                anim.SetBool("quetion", true);
                break;
            case 3:
                anim.SetBool("think",true);
                anim.SetBool("quetion",false);
                break;
            case 4:
                anim.SetBool("think", false);
                anim.SetBool("ready", true);
                break;
            case 5:
                anim.SetBool("ready", false);
                anim.SetBool("quetion", false);
                anim.SetBool("ok",true);
                anim.SetBool("hazu", false);
                break;
        }
        for(int u = 0; u < titleSetumeiText.Length; u++) {
            if(u == count) {
                titleSetumeiText[count].SetActive(true);
                playerComent[count].SetActive(true);
            } else {
                titleSetumeiText[u].SetActive(false);
                playerComent[u].SetActive(false);
            }
        }
    }

    IEnumerator WaitFade() {
        yield return new WaitForSeconds(2.0f);
        FadeOut();
    }

    void FadeOut() {
        alfa += fadeSpeed;
        SetAlpha();
        if(alfa >= 1.0f) {
            sibaritukeru.NO = false;
            hontoi.YES = false;
            stageSelect.NORMAL = false;
            fade = false;
            sceneName = "PlayScene";
            SceneManager.LoadScene("LoadScene");
        }
    }

    void SetAlpha() {
        FadeObj.color = new Color(0, 0, 0, alfa);
    }
}
