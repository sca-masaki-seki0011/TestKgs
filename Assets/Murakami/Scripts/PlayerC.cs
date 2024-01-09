using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;
using PathCreation;

[RequireComponent(typeof(CharacterController))]
public class PlayerC : MonoBehaviour
{
    #region//編集可能変数
    [Header("通常の速度"), SerializeField]
    private float playerSpeed;
    [Header("回避時の速度"), SerializeField]
    private float avoidanceSpeed;
    [Header("ダッシュ時の速度"), SerializeField]
    private float dashSpeed;   
    [Header("しゃがみ中の速度"), SerializeField] 
    private float shitSpeed;
    [Header("落下の初速"), SerializeField]
    private float _initFallSpeed;
    [Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    private float _fallSpeed;
    [Header("重力加速度"), SerializeField]
    private float _gravity;
    [Header("ジャンプする瞬間の速さ(ジャンプ力)"), SerializeField]
    private float _jumpSpeed;
    [Header("体力ゲージの最大値"),SerializeField]
    private float maxStamina;

    [Header("回避のクールタイム"), SerializeField]
    private int  coolTime;
    [Header("残機を増やす時の所持金額"), SerializeField]
    private int maxCoins;

    [Header("体力ゲージ（緑、赤、黒の順番）"), SerializeField]
    private Image[] gutsGauge;
    [Header("体力用Canvas"),SerializeField]
    private Canvas canvas;
    [Header("カメラ"), SerializeField]
    private GameObject cameraObject;
    //[Header("カメラ"), SerializeField]
    //private GameObject cameraC;
    [Header("ゲーマネ"),SerializeField]
    private GameManager gameManager;
    [Header("アニメーション"),SerializeField]
    private Animator anim = null;

    [SerializeField] MeshCollider[] planeCol;

    
    [SerializeField] BoxCollider[] kaidan;

    #endregion

    #region//変数
    //カメラの右方向のベクトル（横移動用）
    private Vector3 cameraRightVec = Vector3.zero;
    //カメラの正面方向のベクトル（縦移動用）
    private Vector3 cameraForVec = Vector3.zero;
    //入力されたベクトル
    private Vector2 _inputMove;
    //現在の向きと座標
    private Transform _transform;
    //上向きのベクトル
    private float _verticalVelocity;
    //回転
    private float _turnVelocity;
    //実際に計算を行う変数
    private float _runCounter;
    //最新の体力状況
    private float nowStamina = 0.0f;
    //雨降っている判定
    private bool isRain = true;
    //接地判定
    private bool _isGroundedPrev;
    //トランポリンの接触判定
    private bool onTramporin = false;
    //無敵判定
    private bool isAvoidansing = false;
    //お守りを所持している状態
    private bool getedGuardian = false;
    //ゴール可能状態
    private bool canGoal = false;

    //完全なゴール判定フラグ
    bool allGoal = false;

    public bool ALLGOAL {
        set {
            this.allGoal = value;
        }
        get {
            return this.allGoal;
        }
    }
    //残機
    private int playerRemain = 0;

    bool falling = false;
    public bool FALLING {
        set {
            this.falling = value;
        }
        get {
            return falling;
        }
    }

    int KeyCount = 0;
    public int KEYCOUNT {
        set {
            this.KeyCount = value;
        }
        get {
            return this.KeyCount;
        }
    }

    public PathCreator pathCreator;
    public PathCreator pathCreators;
    float P_speed = 0.5f;//20.0f;
    float d;

    private GameObject _mainCamera;

    //スティックの入力
    [SerializeField] private PlayerInput _playerInput;
    //
    private InputAction _buttonAction;
    private CharacterController _characterController;
    #endregion

    //プレイヤーの状態（通常状態、ダッシュ中、しゃがみ中）
    private enum PlayerPlam
    {
        Normal,
        Dash,
        Shit//,
        //Avoidansing
    };

    private PlayerPlam playrPlam = PlayerPlam.Normal;

    //回避の状態（使用可能、回避中、クールタイム中）
    private enum AvoidansingPlam
    {
        Can,
        Doing,
        CoolTime
    };

    private AvoidansingPlam avoiPlam = AvoidansingPlam.Can;

    //スタミナの状態（使える、使用中、回復中、使用不可）
    private enum StaminaPlam
    {
        CanUse,
        Using,
        Recovery,
        NotUse
    };

    private StaminaPlam staminaPlam = StaminaPlam.CanUse;

    #region//プロパティ
    public bool GetedGuardian
    {
        get { return this.getedGuardian;}
        set { this.getedGuardian = value;}
    }

    public bool IsRain
    {
        get { return this.isRain;}
        set { this.isRain = value;}
    }
    #endregion

    //[Header("Cinemachine")]
    //[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    //public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    bool bokinn = false;
    public bool BOKIN {
        set {
            this.bokinn = value;
        }
        get {
            return this.bokinn;
        }
    }

    [SerializeField] MissionManager mission;
    [System.NonSerialized]
    public Vector3 groundNormal = Vector3.zero;

    private Vector3 lastGroundNormal = Vector3.zero;

    [System.NonSerialized]
    public Vector3 lastHitPoint = new Vector3(Mathf.Infinity, 0, 0);


    bool missio = false;
    public bool MISSIO {
        set {
            missio = value;
        }
        get {
            return missio;
        }
    }
    float missioTime;

    [SerializeField] Animator tranporin;
    [SerializeField] GameObject DamagePanel;

    private void Awake()
    {
        mission = mission.GetComponent<MissionManager>();
        //メインカメラを取得する
        if(_mainCamera == null) {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _transform = transform;
        _runCounter = coolTime;
        nowStamina = maxStamina;
        _characterController = GetComponent<CharacterController>();
        anim = anim.GetComponent<Animator>();
        playerRemain = gameManager.ManagerRemain;
    }

    bool p = false;

    void Start()
    {
        DamagePanel.SetActive(false);
        for(int u = 0; u < planeCol.Length; u++) {
            planeCol[u] = planeCol[u].GetComponentInChildren<MeshCollider>();
            planeCol[u].enabled = true;
        }
        
        for(int count = 0; count < gutsGauge.Length; count++)
        {
            gutsGauge[count].enabled = false;
        }
    }

    bool up = false;
    bool down = false;

    void Update()
    {
  
       if(onTramporin) {
            bigJump = true;
       
        
       if(bigJump) {
            _jumpSpeed = 20f;
            _fallSpeed = 50f;
            isGrounded = false;
            _verticalVelocity = _jumpSpeed;
            _verticalVelocity -= _gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
            if(_verticalVelocity < -_fallSpeed)
                _verticalVelocity = -_fallSpeed;
            bigJump = false;
        }

        isGrounded = _characterController.isGrounded;
        
        if(isGrounded && !_isGroundedPrev) {
            // 着地する瞬間に落下の初速を指定しておく
            _verticalVelocity = -_initFallSpeed;
        } else if(!isGrounded) {
            // 空中にいるときは、下向きに重力加速度を与えて落下させる
            _verticalVelocity -= _gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
            if(_verticalVelocity < -_fallSpeed)
                _verticalVelocity = -_fallSpeed;
        }

        _isGroundedPrev = isGrounded;
    }
        
        if(_characterController.isGrounded && silde) {
            playerSpeed = 6f;
            StartCoroutine(WaitSpeed());
        } 
     
        if(missio) {
            missioTime += Time.deltaTime;
        }
        if(missioTime >= 3.0f) {
            mission.MiSSIONCOUNT++;
            missio = false;
            missioTime = 0;
        }

        //スタミナ管理
        CountStamina();
        if(allGoal && !gameManager.GAMEOVER) {
            _playerInput.enabled = false;
            
        }
       else if(p){
            Debug.Log("壁");
            //プレイヤーの状態管理
            //d+=P_speed*Time.deltaTime;
            //_transform.position = pathCreator.path.GetPointAtDistance(d);
            //_gravity = 0f;
            _transform.rotation = Quaternion.Euler(0, 0, -90);
            //_transform.position = pathCreators.path.GetPointAtDistance(d); 
            MovePlayer();
        }
        else {
            MovePlayer();
        }
    }

    

    IEnumerator WaitSpeed() {
        yield return new WaitForSeconds(3.0f);
        playerSpeed = 4f;}

    //体力ゲージの表示がおかしくならない様にするために必要
    private void LateUpdate()
    {
        //UIを常にカメラに向かせる
        canvas.transform.LookAt(Camera.main.transform.position);
        //CameraRotation();
    }
    private bool IsCurrentDeviceMouse {
        get {
return _playerInput.currentControlScheme == "Gamepad";

        }
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax) {
        if(lfAngle < -360f) lfAngle += 360f;
        if(lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    bool silde = false;
    #region//ボタン処理
    //移動アクション
    public void OnMove(InputAction.CallbackContext context)
    {
        if(silde) {
            DOTween.KillAll();
            for(int u = 0; u < kaidan.Length; u++) {
                kaidan[u].enabled = true;
            }
            silde = false;
        }
       
        _inputMove = context.ReadValue<Vector2>();
        //移動量の入力
     
        //ベクトルの入力
       if(_inputMove.magnitude > 0)
           {
        //if(_inputMove.magnitude > 0.49) {
            anim.SetBool("InputVec", true);
        }

        else {
           
            anim.SetBool("InputVec",false);
        }
        //anim.SetFloat("InputGet", _inputMove.magnitude);

    }

    //ジャンプアクション
    public void OnJump(InputAction.CallbackContext context)
    {
        
        if(silde) {
            //DOTween.KillAll(); 
            _jumpSpeed = 20f;
            playerSpeed = 10f;
            _verticalVelocity = _jumpSpeed;
            for(int u = 0; u < kaidan.Length; u++) {
            kaidan[u].enabled = true;
            }
            silde = false;
        } else {
            _jumpSpeed = 10f;
            playerSpeed = 4f;

        }
       
        //地面にいる時且つ回避していない時だけ処理する
        if(!context.performed || !_characterController.isGrounded)
        {
           
            return;
        }

        if (avoiPlam != AvoidansingPlam.Doing)
        {
            _verticalVelocity = _jumpSpeed;
        }
        //トランポリン上のジャンプ力
        if (onTramporin)
        {
            _verticalVelocity = _jumpSpeed * 2.0f;
        }
    }

    

    //回避
    public void OnAvoidance(InputAction.CallbackContext context)
    {
        if(!context.performed || !_characterController.isGrounded) return;
        if(avoiPlam == AvoidansingPlam.Can)
        StartAvoidance();
    }
 
    //ダッシュ開始
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed || !_characterController.isGrounded || staminaPlam == StaminaPlam.NotUse) return;
        StartDash();
    }

    //ダッシュ終了
    public void EndDash(InputAction.CallbackContext context)
    {
        if(!context.performed || playrPlam != PlayerPlam.Dash) return;
        EndDash();
        //Debug.Log("入力バグ");
    }

    //しゃがみ実行、終了
    public void ChangeShit(InputAction.CallbackContext context)
    {
        if (!context.performed || !_characterController.isGrounded) return;
        if(playrPlam != PlayerPlam.Shit)
        {
            StartShit();
        }
        else
        {
            EndShit();
        }
    }
    #endregion

    #region//行動処理
    bool isGrounded;
    bool bigJump = false;
    bool getKey = false;
    public bool GETKEY {
        set {
            this.getKey = value;;
        }
        get {
            return this.getKey;
        }
    }
    //行動処理
    private void MovePlayer()
    {
        _buttonAction = _playerInput.actions.FindAction("Dash");
        isGrounded = _characterController.isGrounded;
        //Vector3 cameraFor = cameraC.transform.position;

        if (isGrounded && !_isGroundedPrev)
        {
            // 着地する瞬間に落下の初速を指定しておく
            _verticalVelocity = -_initFallSpeed;
        }

        else if (!isGrounded)
        {
            // 空中にいるときは、下向きに重力加速度を与えて落下させる
            _verticalVelocity -= _gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
            if (_verticalVelocity < -_fallSpeed)
            {
                _verticalVelocity = -_fallSpeed;
                //Debug.Log("上限知");
            }
        }

        //通常の移動
        if (playrPlam == PlayerPlam.Normal)
        {
              PlayerMove(playerSpeed);//,cameraFor   
        }

        //ダッシュ中
        else if (playrPlam == PlayerPlam.Dash)
        {
            //スティックの入力があるときのみ処理を行う
            if(_inputMove != Vector2.zero)
            {
                PlayerMove(dashSpeed);//,cameraFor
            }
            //ボタン入力ON、スティック入力NO＝＞スタミナ減少を解除してダッシュを終了する
            else
            {
                EndDash();
            }
        }

        //回避が入力されたら
        else if(avoiPlam == AvoidansingPlam.Doing && _inputMove != Vector2.zero)
        {
            PlayerMove(avoidanceSpeed);//,cameraFor
        }
        
        _isGroundedPrev = isGrounded;

        if (avoiPlam == AvoidansingPlam.CoolTime)
        {
            CountAvoidansing();
        }

        //回転の更新
        if (_inputMove != Vector2.zero)
        {
            // 移動入力がある場合は、振り向き動作も行う

            // 操作入力からy軸周りの目標角度[deg]を計算
            var targetAngleY = -Mathf.Atan2(_inputMove.y, _inputMove.x)
                * Mathf.Rad2Deg+90;
           
            // イージングしながら次の回転角度[deg]を計算
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
               targetAngleY,
               ref _turnVelocity,
                0.1f
          );

            // オブジェクトの回転を更新
            if(!p) {
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }
            
            
            /*
            if(GetSlope() <= 1f) {
                up = false;
                down = false;
            }
            if(up) {
                _gravity = 0f;
                _transform.rotation = Quaternion.Euler(-GetSlope(), 0, 0);
                //up = false;
            }
            else if(down) {
                _gravity = 15f;
                _transform.rotation = Quaternion.Euler(GetSlope(), 0, 0);
                //down = false;
            }
           else if(!up && !down){
                _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }
            */
        }

       
    }

    float GetSlope() {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit)) {
            return Vector3.Angle(Vector3.up, hit.normal);
        }

        return 0f;
    }



    //移動処理
    private void PlayerMove(float speed)//, Vector3 cameraVec
    {
        
            Vector3 moveVelocity = new Vector3(
             _inputMove.x * speed,
             _verticalVelocity,
             _inputMove.y * speed
         );
          var moveDelta = moveVelocity * Time.deltaTime;
        
       
        _characterController.Move(moveDelta);
        
    }

    //無敵判定をつける
    public void StartAvoidance()
    {
        isAvoidansing = true;
        avoiPlam = AvoidansingPlam.Doing;
        anim.SetTrigger("OnRoll");
    }

    //無敵判定を解除する
    public void EndAvoidance()
    {
        isAvoidansing = false;
        avoiPlam = AvoidansingPlam.CoolTime;
    }

    //ダッシュの開始
    public void StartDash()
    {
        playrPlam = PlayerPlam.Dash;
        anim.SetBool("OnDash", true);
    }

    //ダッシュの終了
    public void EndDash()
    {
        playrPlam = PlayerPlam.Normal;
        //ダッシュ終了時点でスタミナが残っている状態
        if(nowStamina > 0.0f)
        {
            staminaPlam = StaminaPlam.Recovery;
        }
        //スタミナを使い切った
        else
        {
            staminaPlam = StaminaPlam.NotUse;
        }
        anim.SetBool("OnDash",false);
    }

    //しゃがみ開始
    public void StartShit()
    {
        playrPlam = PlayerPlam.Shit;
    }

    //しゃがみ終了
    public void EndShit()
    {
        playrPlam = PlayerPlam.Normal;
    }

    
    //接触処理
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //雨が降っているなら
        if(isRain)
        {
            //groundHit = hit;
        }
        if (hit.gameObject.tag == "Goal" && canGoal)
        {
            allGoal = true;
            gameManager.NameChange();
        }

     

    }
 
    /*
    public void SwitchPath(int count) {
        switch(count) {
            case 1:
                Vector3[] path = new Vector3[4];
                path[0] = this.transform.position;
                path[1] = new Vector3(63.57f, 8.46f, -2.16f);
                path[2] = new Vector3(68.59f, 8.33f, -2.16f);
                path[3] = new Vector3(80.48f, -1.03f, -2.16f);
                MoveSlide(path);
                break;
            case 2:
                Vector3[] paths = new Vector3[3];
                paths[0] = this.transform.position;
                paths[1] = new Vector3(68.59f, 8.33f, -2.16f);
                paths[2] = new Vector3(80.48f, -1.03f, -2.16f);
                MoveSlide(paths);
                break;
            case 3:
                Vector3[] pathss = new Vector3[2];
                pathss[0] = this.transform.position;
                pathss[1] = new Vector3(80.48f, -1.03f, -2.16f);
                MoveSlide(pathss);
                break;
        }
        
    }
    */
    void MoveSlide(Vector3[] path) {
        this.transform.DOPath(path, 3.5f);
    }

    int co = 0;
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Ice") {

            silde = true;
            for(int u = 0; u < kaidan.Length; u++) {
                kaidan[u].enabled = false;
            }
        }
        if(col.tag == "Tramprin")
        {
            tranporin.SetBool("Tranporin",true);
            //_verticalVelocity = _jumpSpeed / 2;
            isGrounded = false;
            onTramporin = true;
        }
        if(col.tag == "hunn") {
            Debug.Log(col.gameObject);
        }
        if(col.tag == "holl") {
            falling = true;
            for(int u = 0; u < planeCol.Length; u++) {
                planeCol[u].enabled = false;
            }
            _playerInput.enabled = false;
            StartCoroutine(WaitChara());
        }
       
        if(col.tag == "up") {
            up = true;
           
        }
        
        if(col.tag == "p") {
            p = true;
        }
        
        if(col.tag == "P") {
            //p = false;
        }

        if(col.tag == "down") {
            down = true;
            
        }

        
            if(col.tag == "mission") {

            mission.KeyActive(mission.RADOMMISSIONCOUNT);
            if(mission.RADOMMISSIONCOUNT != 2) {
                mission.MISSIONVALUE[mission.RADOMMISSIONCOUNT]++;
            }
            if(mission.MiSSIONCOUNT != 3) {
                //StartCoroutine(WaitAddPoint());
                missio = true;
            }
            
                col.gameObject.SetActive(false);
            }
        

        if(col.tag == "Enemy") {
            //falling = true;
            co = 1;
            StartCoroutine(WaitFall());
            _playerInput.enabled = false;
            StartCoroutine(WaitChara());
        }

        if(col.tag == "Car") {
            if(gameManager.ManagerRemain != 0) {
                //gameManager.ManagerRemain--;
               
                falling = true;
                _playerInput.enabled = false;
            }
        }
        
    }
    
    IEnumerator WaitFall() {
        yield return null;

        while(co != 0) {

            DamagePanel.SetActive(false);


            yield return new WaitForSeconds(0.15f);


            DamagePanel.SetActive(true);


            yield return new WaitForSeconds(0.15f);
            co--;

        }
        DamagePanel.SetActive(false);
        falling = true;
        yield break;
    }

    IEnumerator WaitChara() {
        yield return new WaitForSeconds(1.0f);
        _characterController.enabled = false;
    }


    bool y = false;

    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Tramprin")
        {
            StartCoroutine(WaitJump());
            onTramporin = false;
        }
      
    }

    IEnumerator WaitJump() {
        yield return new WaitForSeconds(1f);
        tranporin.SetBool("Tranporin", false);
    }
   
    #endregion

    #region//数字関係


    //アイテム獲得
    public void GetItem(int getScore, string itemName)
    {
        //スコア加算
        gameManager.AddScore(getScore);
        switch(itemName)
        {
            case "Key":
                getKey = true;
                if(mission.MiSSIONCOUNT < 2) {
                    StartCoroutine(WaitKeyFlag());
                }
                if(KeyCount < 4) {
                    KeyCount++;
                    
                }
                
                else {
                    
                    KeyCount = 0;
                }

                if(KeyCount == 3) {
                    canGoal = true;
                }

                break;
            case "Gard":
                getedGuardian = true;
                break;
        }
    }
    
    IEnumerator WaitKeyFlag() {
        yield return new WaitForSeconds(3.0f);
        mission.STATMISSION = false;
        mission.YBUTTON = false;
    }

    //回避のクールタイム計算
    float _count = 0.0f;
    private void CountAvoidansing()
    {
        _count += 0.1f;
        //計算用変数が0以下になったら
        if(_count >= 1.0f)
        {
            _count = 0.0f;
            _runCounter--;
            if(_runCounter <= 0.0f)
            {
                _runCounter = coolTime;
                //回避可能状態にする。
                avoiPlam = AvoidansingPlam.Can;
            }
        }
    }

    [SerializeField] private float showStamina;
    private float runShowStamina = 0.0f;
    private bool finishRecover = false;
    //スタミナゲージの管理を行う
    private void CountStamina()
    {
        if (nowStamina >= maxStamina)
        {
            nowStamina = maxStamina;
        }

        if (nowStamina >= maxStamina + 5)
        {
            Debug.Log("超えた");
        }
        var rate = 0.0f;
        //ゲージ消去用のフラグ
        //スタミナがあり、ダッシュ中なら
        if(staminaPlam != StaminaPlam.NotUse && playrPlam == PlayerPlam.Dash)
        {
            if(finishRecover) finishRecover = false;
            //非表示にしていたスタミナゲージを表示する
            for(int count = 0; count < gutsGauge.Length; count++)
            {
                //非表示状態の時にのみ実行する
                if(!gutsGauge[count].enabled)
                gutsGauge[count].enabled = true;
            }

            //スタミナ減少処理・表示処理
            //nowStamina -= 0.02f;
            rate = nowStamina / maxStamina;
            gutsGauge[0].fillAmount = rate;
            gutsGauge[1].fillAmount = rate + 0.1f;

            //スタミナが切れたら
            if(nowStamina <= 0.0f)
            {
                //gutsGauge[0].enabled = false;
                EndDash();
            }
        }

        //スタミナが切れているなら
        else if(staminaPlam != StaminaPlam.CanUse)
        {
            if(staminaPlam == StaminaPlam.NotUse)
            {
                gutsGauge[0].enabled = false;
                //移動中の回復量
                if (_inputMove != Vector2.zero)
                {
                    nowStamina += 0.01f;
                }
                //停止中の回復量
                else
                {
                    nowStamina += 0.02f;
                }

                rate = nowStamina / maxStamina;
                gutsGauge[1].fillAmount = rate;

                //全回復完了
                if (rate >= 1)
                {
                    gutsGauge[0].enabled = true;
                    gutsGauge[0].fillAmount = rate;
                    staminaPlam = StaminaPlam.CanUse;
                    finishRecover = true;
                }
            }

            //スタミナが残っている状態なら
            else if(staminaPlam == StaminaPlam.Recovery)
            {
                //移動中の回復量
                if (_inputMove != Vector2.zero)
                {
                    nowStamina += 0.025f;
                }
                //停止中の回復量
                else
                {
                    nowStamina += 0.03f;
                }

                rate = nowStamina / maxStamina;
                gutsGauge[0].fillAmount = rate;
                gutsGauge[1].fillAmount = rate;

                //全回復した
                if (rate >= 1)
                {
                    staminaPlam = StaminaPlam.CanUse;
                    finishRecover = true;
                }
            }
        }

        //ゲージの消去
        if(finishRecover)
        {
            runShowStamina += 0.01f;
            if(runShowStamina >= showStamina)
            {
                for(int count = 0; count < gutsGauge.Length; count++)
                {
                    gutsGauge[count].enabled = false;
                    runShowStamina = 0.0f;
                }
            }
        }
    }
    #endregion

    #region//コルーチン関係
    #endregion
}
