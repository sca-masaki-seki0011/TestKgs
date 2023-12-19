using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("ステージの制限時間"), SerializeField]
    private int stageCount;
    [Header("使用する画像"), SerializeField]
    private Sprite[] numberImage;
    [Header("タイム用Image"), SerializeField]
    private Image[] timeImage;

    //[Header("スコア表示用"), SerializeField]
    //private Image[] scoreImage;
    [SerializeField]
    private TMP_Text scoreText;
    [Header("連続獲得を許容する時間（F）"), SerializeField]
    private float maxGetCount;

    [Header("管理している残機"),SerializeField]
    private int managerRemain;
    //残機のゲッターセッター
    public int ManagerRemain {
        get { return this.managerRemain; }
        set { this.managerRemain = value; }
    }

    Animator zankiAnim;
    bool zanki = false;
    float zankiTime;
    [SerializeField]
    private TMP_Text ZankiText;


    //上位５名分＋挑戦した際のスコア
    private int[] rankingScore; 
    //今回のスコア
    private int gameScore;
    //連続で獲得したコインの数
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
    //連続獲得可能時間の演算用変数
    private float canGetCoinsTime = 0.0f;
    //連続で獲得可能フラグ
    private bool canGetCoins = false;
    //選択された縛り
    private string[] selectBinding;
    //ポーズ中
    private bool stopGame = false;
    //実行中の経過時間
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
        ZankiText.SetText("×{0}", managerRemain);
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
        

        //コインの獲得時の動き
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
        zankiIconText.text = "×" + managerRemain;
        List<IEnumerator> ie = new List<IEnumerator>();
        ie.Add(WaitU());
        foreach(IEnumerator item in ie) {
            StartCoroutine(item);
            
            yield return item.Current;// <===ここが重要
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
            yield return item.Current;// <===ここが重要
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
    //ゲーム時間計算
    private void CountGameTime()
    {
        //時間計測
        currentGameTime += Time.deltaTime;
        if(currentGameTime >= 1.0f)
        {
            stageCount -= 1;
            currentGameTime = 0.0f;
        }
       
        //表記更新
        /*
        for(int i = 0; i < 3; i++)
        {
            //100,10の秒数の計算
            if(i < 1)
            {
                timeImage[i].sprite = numberImage[stageCount / rage];
                rage /= 10;
            }
            //10,1の秒数の計算
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

    //スコア計算
    public void AddScore(int score)
    {
        //現在時間計測を行っているなら時間をリセットする
        if(canGetCoins)
        {
            canGetCoinsTime = 0.0f;
        }
        else
        {
            canGetCoins = true;
        }
        //スコア加算
        //var getScore = score * Mathf.Pow(2, getCoinCount);
        gameScore += score;//(int)getScore;
        //ここでカウントを増加する（必ず１倍、２倍、４倍...という感じで計算したいから）
        
        scoreText.SetText("Score:{0}", gameScore);
    }

    public void AddCoin(int coin) {
        if(windowUP) {
            coinAnim.enabled = windowUP;
            coinAnim.SetBool("coin", windowUP);
            //windowFlag = false;
        }
        
        getCoinCount += coin;
        //所持金が1000上がるごとに残機1増える
        if(getCoinCount % 1000 == 0 && getCoinCount != 0) {
            zanki = true;
            getCoinCount-=1000;
            zankiAnim.enabled = zanki;
            zankiAnim.SetBool("zanki", zanki);
            managerRemain++;
        }
        CoinText.SetText("Coin:{0}", getCoinCount);
        ZankiText.SetText("×{0}", managerRemain);
    }

    //連続獲得可能時間の計算処理
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
    /// 残機があった場合の生き返り関数
    /// </summary>
    /// <param name="recount">チェックポイントの数</param>
    public void RevivePlayer() {
        if(managerRemain != 0) {
            //if(recount == 0) {
                //ふれていなかったらのチェックポイント
                //playerObject.transform.position = new Vector3(-1.07f, -0.06f, -26.7f);
            //} else {
                playerObject.transform.position = dessPosition;
            //}
        }
    }
}
