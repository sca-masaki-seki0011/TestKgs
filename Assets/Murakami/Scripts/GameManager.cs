using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("�X�e�[�W�̐�������"), SerializeField]
    private int stageCount;
    [Header("�g�p����摜"), SerializeField]
    private Sprite[] numberImage;
    [Header("�^�C���pImage"), SerializeField]
    private Image[] timeImage;

    //[Header("�X�R�A�\���p"), SerializeField]
    //private Image[] scoreImage;
    [SerializeField]
    private TMP_Text scoreText;
    [Header("�A���l�������e���鎞�ԁiF�j"), SerializeField]
    private float maxGetCount;

    [Header("�Ǘ����Ă���c�@"),SerializeField]
    private int managerRemain;
    //�c�@�̃Q�b�^�[�Z�b�^�[
    public int ManagerRemain {
        get { return this.managerRemain; }
        set { this.managerRemain = value; }
    }

    Animator zankiAnim;
    bool zanki = false;
    float zankiTime;
    [SerializeField]
    private TMP_Text ZankiText;


    //��ʂT�����{���킵���ۂ̃X�R�A
    private int[] rankingScore; 
    //����̃X�R�A
    private int gameScore;
    //�A���Ŋl�������R�C���̐�
    private int getCoinCount = 0;
    public int GETCOIN {
        set {
            this.getCoinCount = value;
        }
        get {
            return this.getCoinCount;
        }
    }
    Animator coinAnim;
    [SerializeField]
    private TMP_Text CoinText;
    //�A���l���\���Ԃ̉��Z�p�ϐ�
    private float canGetCoinsTime = 0.0f;
    //�A���Ŋl���\�t���O
    private bool canGetCoins = false;
    //�I�����ꂽ����
    private string[] selectBinding;
    //�|�[�Y��
    private bool stopGame = false;
    //���s���̌o�ߎ���
    private float currentGameTime = 0.0f;

    public string[] SelectBinding
    {
        get { return this.selectBinding;}
        set { this.selectBinding = value;}
    }

    

    bool windowUP = false;
    public bool WINDOWUP {
        set {
            this.windowUP = value;
        }
        get {
            return windowUP;
        }
    }
    float timeUI = 0;

    [SerializeField] GameObject playerObject;
    PlayerC player;
    PlayerInput playerInput;

    [SerializeField] Image PausePanel;
    float fadeSpeed = 0.02f;
    float P_red, P_green, P_blue, P_alfa;

    [SerializeField] PauseManager pause;

    [SerializeField] MissionManager mission;

    Vector3 dessPosition;

    public Vector3 DESSPOS {
        set {
            this.dessPosition = value;
        }
        get {
            return this.dessPosition;
        }
    }

    

    bool fadeIn = false;

    bool GameOver = false;
    public bool GAMEOVER {
        set {
            this.GameOver = value;
        }
        get {
            return this.GameOver;
        }
    }
    [SerializeField] MeshCollider[] plane;


    [SerializeField] GameObject gameOverThings;
    [SerializeField] GameObject gameClearThings;

    private void Awake() {
        managerRemain = 3;
    }

    [SerializeField] GameObject zankiIocn;
    Text zankiIconText;
    [SerializeField] GameObject missText;

    CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = playerObject.GetComponent<CharacterController>();
        zankiIconText = zankiIocn.GetComponentInChildren<Text>();
        gameOverThings.SetActive(false);
        gameClearThings.SetActive(false);
        for(int u = 0; u < plane.Length; u++) {
            plane[u] = plane[u].GetComponent<MeshCollider>();
        }
        
        player = playerObject.GetComponent<PlayerC>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        mission = mission.GetComponent<MissionManager>();
        pause = pause.GetComponent<PauseManager>();
        PausePanel = PausePanel.GetComponent<Image>();
        P_red = PausePanel.color.r;
        P_green = PausePanel.color.g;
        P_blue = PausePanel.color.b;
        P_alfa = PausePanel.color.a;
        coinAnim = CoinText.GetComponent<Animator>();
        coinAnim.enabled = false;
        scoreText.SetText("Score:{0}",gameScore);
        CoinText.SetText("Coin:{0}", getCoinCount);
        zankiAnim = ZankiText.GetComponent<Animator>();
        zankiAnim.enabled = false;
        ZankiText.SetText("�~{0}", managerRemain);
        dessPosition = player.transform.position;
    }

    bool zankiAdd = false;
    int c = 0;
    // Update is called once per frame
    void Update()
    {
        if(GameOver) {
            //GameOver = true;
            GameOverActive();
        }

      

        if(zanki) {
          
            zankiTime += Time.deltaTime;
        }
        if(zankiTime >= 5.0f) {
            zankiTime = 0f;
            zanki = false;
            zankiAdd = false;
            zankiAnim.SetBool("zanki",zanki);
           
        }
        

        //�R�C���̊l�����̓���
        if(windowUP) {
            if(timeUI <= 5.0f) {
                timeUI += Time.deltaTime;
            } 

        }
        if(timeUI >= 5.0f) {
            timeUI = 0f;
            windowUP = false;
            coinAnim.SetBool("coin",windowUP);
            
        }
        //=====================================
        if(canGetCoins)
        {
            CountSuccession();
        }

        if(!pause.PAUSE && !mission.MISSIONFLAG && !player.ALLGOAL && !GameOver)
        {
            CountGameTime();
        }

        if(player.ALLGOAL) {
            StartCoroutine(WaitGameClearActive());
        }
    }

    void GameClearActive() {
        gameClearThings.SetActive(true);
    }

    IEnumerator WaitGameClearActive() {
        yield return new WaitForSeconds(7.0f);
        GameClearActive();
    }

    void GameOverActive() {
        gameOverThings.SetActive(true);
    }
    
    private void FixedUpdate() {
        if(!fadeIn && (player.FALLING || player.ALLGOAL)) {
            FadeOut();
        }
    }

  

    void FadeOut() {
        if(player.FALLING) {
            if(!player.ALLGOAL) {
                StartCoroutine(WaitInoti());
            }
            
            if(P_alfa < 1.0f) {
                P_alfa += fadeSpeed;
                SetAlpha();
            }
            else if(P_alfa >= 1.0f) {
                fadeIn = true;
                
            }
        }
        if(player.ALLGOAL) {
            if(P_alfa < 0.75f) {
                P_alfa += fadeSpeed;
                SetAlpha();
            }
        }
        
    }

    IEnumerator WaitInoti() {
        if(managerRemain != 0) {
            missText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        
        for(int u = 0; u < plane.Length; u++) {
            plane[u].enabled = true;
        }
        RevivePlayer();
        
        
        missText.SetActive(false);
        zankiIocn.SetActive(true);
        zankiIocn.SetActive(true);
        zankiIconText.text = "�~" + managerRemain;
        List<IEnumerator> ie = new List<IEnumerator>();
        ie.Add(WaitU());
        foreach(IEnumerator item in ie) {
            StartCoroutine(item);
            
            yield return item.Current;// <===�������d�v
        }
        yield return null;
        } else {
            GameOver = true;
        }
    }

    bool remain = false;
    bool g = false;
    IEnumerator WaitU() {
        yield return new WaitForSeconds(1.0f);
        if(!remain) {
            managerRemain--;
            
            remain = true;
        }
        _characterController.enabled = true;
        player.FALLING = false;
        playerInput.enabled = true;

        if(!GameOver) { 

        List<IEnumerator> ie = new List<IEnumerator>();
        ie.Add(WaitFadeIn());
        foreach(IEnumerator item in ie) {
            StartCoroutine(item);
            yield return item.Current;// <===�������d�v
        }
        yield return null;
        } else {
            //GameOverActive();
            //g = true;
            yield break;
        }
    }

    IEnumerator WaitFadeIn() {
        yield return new WaitForSeconds(1.0f);
        
        FadeIn();
    }

    void FadeIn() {


        zankiIocn.SetActive(false);
        
        if(P_alfa > 0.0f) {
            P_alfa -= fadeSpeed;
            SetAlpha();
        }
        
        if(P_alfa <= 0.0f) { 
            fadeIn = false;
            remain = false;
        }
    }




    void SetAlpha() {
        PausePanel.color = new Color(P_red, P_green, P_blue, P_alfa);
    }

    int rage = 100;
    //�Q�[�����Ԍv�Z
    private void CountGameTime()
    {
        //���Ԍv��
        currentGameTime += Time.deltaTime;
        if(currentGameTime >= 1.0f)
        {
            stageCount -= 1;
            currentGameTime = 0.0f;
        }
       
        //�\�L�X�V
        /*
        for(int i = 0; i < 3; i++)
        {
            //100,10�̕b���̌v�Z
            if(i < 1)
            {
                timeImage[i].sprite = numberImage[stageCount / rage];
                rage /= 10;
            }
            //10,1�̕b���̌v�Z
            else
            {
                timeImage[i].sprite = numberImage[stageCount % rage];
                if(i == 1)
                rage /= 10;
                else
                rage = 100;
            }
        }*/
        timeImage[0].sprite = numberImage[stageCount / rage];
        timeImage[1].sprite = numberImage[(stageCount % rage) / 10];
        timeImage[2].sprite = numberImage[stageCount % (rage / 10)];
    }

    //�X�R�A�v�Z
    public void AddScore(int score)
    {
        //���ݎ��Ԍv�����s���Ă���Ȃ玞�Ԃ����Z�b�g����
        if(canGetCoins)
        {
            canGetCoinsTime = 0.0f;
        }
        else
        {
            canGetCoins = true;
        }
        //�X�R�A���Z
        //var getScore = score * Mathf.Pow(2, getCoinCount);
        gameScore += score;//(int)getScore;
        //�����ŃJ�E���g�𑝉�����i�K���P�{�A�Q�{�A�S�{...�Ƃ��������Ōv�Z����������j
        
        scoreText.SetText("Score:{0}", gameScore);
    }

    public void AddCoin(int coin) {
        if(windowUP) {
            coinAnim.enabled = windowUP;
            coinAnim.SetBool("coin", windowUP);
            //windowFlag = false;
        }
        
        getCoinCount += coin;
        //��������1000�オ�邲�ƂɎc�@1������
        if(getCoinCount % 1000 == 0 && getCoinCount != 0) {
            zanki = true;
            getCoinCount-=1000;
            zankiAnim.enabled = zanki;
            zankiAnim.SetBool("zanki", zanki);
            managerRemain++;
        }
        CoinText.SetText("Coin:{0}", getCoinCount);
        ZankiText.SetText("�~{0}", managerRemain);
    }

    //�A���l���\���Ԃ̌v�Z����
    private void CountSuccession()
    {
        canGetCoinsTime += 0.02f;
        if(canGetCoinsTime >= maxGetCount)
        {
            canGetCoins = false;
            //getCoinCount = 0;
        }
    }

    public void NameChange()
    {
        scoreText.SetText("Goal");
    }

    /// <summary>
    /// �c�@���������ꍇ�̐����Ԃ�֐�
    /// </summary>
    /// <param name="recount">�`�F�b�N�|�C���g�̐�</param>
    public void RevivePlayer() {
        if(managerRemain != 0) {
            //if(recount == 0) {
                //�ӂ�Ă��Ȃ�������̃`�F�b�N�|�C���g
                //playerObject.transform.position = new Vector3(-1.07f, -0.06f, -26.7f);
            //} else {
                playerObject.transform.position = dessPosition;
            //}
        }
    }
}
