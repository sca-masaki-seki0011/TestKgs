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

    float fadeSpeed = 0.04f;

    [SerializeField] Hontoni hontoi;

    [SerializeField] StageSelectController stageSelect;

    public static string sceneName;

    [SerializeField] GameObject playerImage;
    [SerializeField] GameObject[] titleSetumeiText;
    [SerializeField] GameObject[] playerComent;

    // Start is called before the first frame update
    void Start()
    {
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
        Invoke("InconActive",1.0f);
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
            IconObject.SetActive(false);
            Icon.STAGE = false;
        }
        if(sibaritukeru.SIBARIFLAG) {
            SibaritukeruObject.SetActive(false);
            sibaritukeru.SIBARIFLAG = false;
        }

        if(sibaritukeru.NO) {
            FadeOut();
        }

        if(hontoi.YES) {
            FadeOut();
        }

        if(stageSelect.NORMAL) {
            FadeOut();
        }

        if(Gamepad.current.yButton.isPressed) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }

    public void SelectSetumei(int count) {
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


    void FadeOut() {
        alfa += fadeSpeed;
        SetAlpha();
        if(alfa >= 1.0f) {
            sibaritukeru.NO = false;
            hontoi.YES = false;
            stageSelect.NORMAL = false;
            sceneName = "PlayScene";
            SceneManager.LoadScene("LoadScene");
        }
    }

    void SetAlpha() {
        FadeObj.color = new Color(0, 0, 0, alfa);
    }

    /*
    IEnumerator IconScirpts() {
        yield return new WaitForSeconds(1.0f);
        Icon.enabled = true;
    }
    */
}
