using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerC : MonoBehaviour
{
    #region//�ҏW�\�ϐ�
    [Header("�ʏ�̑��x"), SerializeField]
    private float playerSpeed;
    [Header("������̑��x"), SerializeField]
    private float avoidanceSpeed;
    [Header("�_�b�V�����̑��x"), SerializeField]
    private float dashSpeed;   
    [Header("���Ⴊ�ݒ��̑��x"), SerializeField] 
    private float shitSpeed;
    [Header("�����̏���"), SerializeField]
    private float _initFallSpeed;
    [Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    private float _fallSpeed;
    [Header("�d�͉����x"), SerializeField]
    private float _gravity;
    [Header("�W�����v����u�Ԃ̑���(�W�����v��)"), SerializeField]
    private float _jumpSpeed;
    [Header("�̗̓Q�[�W�̍ő�l"),SerializeField]
    private float maxStamina;

    [Header("����̃N�[���^�C��"), SerializeField]
    private int  coolTime;
    [Header("�c�@�𑝂₷���̏������z"), SerializeField]
    private int maxCoins;

    [Header("�̗̓Q�[�W�i�΁A�ԁA���̏��ԁj"), SerializeField]
    private Image[] gutsGauge;
    [Header("�̗͗pCanvas"),SerializeField]
    private Canvas canvas;
    [Header("�J����"), SerializeField]
    private GameObject cameraObject;
    //[Header("�J����"), SerializeField]
    //private GameObject cameraC;
    [Header("�Q�[�}�l"),SerializeField]
    private GameManager gameManager;
    [Header("�A�j���[�V����"),SerializeField]
    private Animator anim = null;

    [SerializeField] MeshCollider[] planeCol;

    #endregion

    #region//�ϐ�
    //�J�����̉E�����̃x�N�g���i���ړ��p�j
    private Vector3 cameraRightVec = Vector3.zero;
    //�J�����̐��ʕ����̃x�N�g���i�c�ړ��p�j
    private Vector3 cameraForVec = Vector3.zero;
    //���͂��ꂽ�x�N�g��
    private Vector2 _inputMove;
    //���݂̌����ƍ��W
    private Transform _transform;
    //������̃x�N�g��
    private float _verticalVelocity;
    //��]
    private float _turnVelocity;
    //���ۂɌv�Z���s���ϐ�
    private float _runCounter;
    //�ŐV�̗̑͏�
    private float nowStamina = 0.0f;
    //�J�~���Ă��锻��
    private bool isRain = true;
    //�ڒn����
    private bool _isGroundedPrev;
    //�g�����|�����̐ڐG����
    private bool onTramporin = false;
    //���G����
    private bool isAvoidansing = false;
    //�������������Ă�����
    private bool getedGuardian = false;
    //�S�[���\���
    private bool canGoal = false;

    //���S�ȃS�[������t���O
    bool allGoal = false;

    public bool ALLGOAL {
        set {
            this.allGoal = value;
        }
        get {
            return this.allGoal;
        }
    }
    //�c�@
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

    private GameObject _mainCamera;

    //�X�e�B�b�N�̓���
    [SerializeField] private PlayerInput _playerInput;
    //
    private InputAction _buttonAction;
    private CharacterController _characterController;
    #endregion

    //�v���C���[�̏�ԁi�ʏ��ԁA�_�b�V�����A���Ⴊ�ݒ��j
    private enum PlayerPlam
    {
        Normal,
        Dash,
        Shit//,
        //Avoidansing
    };

    private PlayerPlam playrPlam = PlayerPlam.Normal;

    //����̏�ԁi�g�p�\�A��𒆁A�N�[���^�C�����j
    private enum AvoidansingPlam
    {
        Can,
        Doing,
        CoolTime
    };

    private AvoidansingPlam avoiPlam = AvoidansingPlam.Can;

    //�X�^�~�i�̏�ԁi�g����A�g�p���A�񕜒��A�g�p�s�j
    private enum StaminaPlam
    {
        CanUse,
        Using,
        Recovery,
        NotUse
    };

    private StaminaPlam staminaPlam = StaminaPlam.CanUse;

    #region//�v���p�e�B
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

    private void Awake()
    {
        mission = mission.GetComponent<MissionManager>();
        //���C���J�������擾����
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
    private StarterAssetsInputs _input;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        for(int u = 0; u < planeCol.Length; u++) {
            planeCol[u] = planeCol[u].GetComponentInChildren<MeshCollider>();
            planeCol[u].enabled = true;
        }
        
        for(int count = 0; count < gutsGauge.Length; count++)
        {
            gutsGauge[count].enabled = false;
        }
    }

    bool missio = false;
    float missioTime;

    void Update()
    {
        if(missio) {
            missioTime += Time.deltaTime;
        }
        if(missioTime >= 3.0f) {
            mission.MiSSIONCOUNT++;
            missio = false;
            missioTime = 0;
        }

        //�X�^�~�i�Ǘ�
        CountStamina();
        if(allGoal && !gameManager.GAMEOVER) {
            _playerInput.enabled = false;
            
        }
       else{
            //�v���C���[�̏�ԊǗ�
            MovePlayer();
        }
    }

    //�̗̓Q�[�W�̕\�������������Ȃ�Ȃ��l�ɂ��邽�߂ɕK�v
    private void LateUpdate()
    {
        //UI����ɃJ�����Ɍ�������
        canvas.transform.LookAt(Camera.main.transform.position);
        //CameraRotation();
    }
    private bool IsCurrentDeviceMouse {
        get {
return _playerInput.currentControlScheme == "Gamepad";

        }
    }

    /*
    //�J�������Q�[���p�b�h�̉E�X�e�B�b�N�œ����ׂ̊֐�
    public void CameraRotation() {


        //���͂�����Ƃ��ɃJ�������Œ肳��Ă��Ȃ��ꍇ
        if(_input.look.sqrMagnitude >= _threshold && !LockCameraPosition) {

            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }

        // ��]��360�x�ɐ�������
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine�̓��e�ɔ��f����
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }
    */
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax) {
        if(lfAngle < -360f) lfAngle += 360f;
        if(lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    #region//�{�^������
    //�ړ��A�N�V����
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
        //�ړ��ʂ̓���
     
        //�x�N�g���̓���
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

    //�W�����v�A�N�V����
    public void OnJump(InputAction.CallbackContext context)
    {
        //�n�ʂɂ��鎞��������Ă��Ȃ���������������
        if(!context.performed || !_characterController.isGrounded)
        {
            return;
        }

        if (avoiPlam != AvoidansingPlam.Doing)
        {
            _verticalVelocity = _jumpSpeed;
        }
        //�g�����|������̃W�����v��
        if (onTramporin)
        {
            _verticalVelocity = _jumpSpeed * 2.0f;
        }
    }

    //���
    public void OnAvoidance(InputAction.CallbackContext context)
    {
        if(!context.performed || !_characterController.isGrounded) return;
        if(avoiPlam == AvoidansingPlam.Can)
        StartAvoidance();
    }
 
    //�_�b�V���J�n
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed || !_characterController.isGrounded || staminaPlam == StaminaPlam.NotUse) return;
        StartDash();
    }

    //�_�b�V���I��
    public void EndDash(InputAction.CallbackContext context)
    {
        if(!context.performed || playrPlam != PlayerPlam.Dash) return;
        EndDash();
        //Debug.Log("���̓o�O");
    }

    //���Ⴊ�ݎ��s�A�I��
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

    #region//�s������

    //�s������
    private void MovePlayer()
    {
        _buttonAction = _playerInput.actions.FindAction("Dash");
        var isGrounded = _characterController.isGrounded;
        //Vector3 cameraFor = cameraC.transform.position;

        if (isGrounded && !_isGroundedPrev)
        {
            // ���n����u�Ԃɗ����̏������w�肵�Ă���
            _verticalVelocity = -_initFallSpeed;
        }

        else if (!isGrounded)
        {
            // �󒆂ɂ���Ƃ��́A�������ɏd�͉����x��^���ė���������
            _verticalVelocity -= _gravity * Time.deltaTime;

            // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
            if (_verticalVelocity < -_fallSpeed)
            {
                _verticalVelocity = -_fallSpeed;
                //Debug.Log("����m");
            }
        }

        //�ʏ�̈ړ�
        if (playrPlam == PlayerPlam.Normal)
        {
              PlayerMove(playerSpeed);//,cameraFor   
        }

        //�_�b�V����
        else if (playrPlam == PlayerPlam.Dash)
        {
            //�X�e�B�b�N�̓��͂�����Ƃ��̂ݏ������s��
            if(_inputMove != Vector2.zero)
            {
                PlayerMove(dashSpeed);//,cameraFor
            }
            //�{�^������ON�A�X�e�B�b�N����NO�����X�^�~�i�������������ă_�b�V�����I������
            else
            {
                EndDash();
            }
        }

        //��������͂��ꂽ��
        else if(avoiPlam == AvoidansingPlam.Doing && _inputMove != Vector2.zero)
        {
            PlayerMove(avoidanceSpeed);//,cameraFor
        }
        
        _isGroundedPrev = isGrounded;

        if (avoiPlam == AvoidansingPlam.CoolTime)
        {
            CountAvoidansing();
        }

        //��]�̍X�V
        if (_inputMove != Vector2.zero)
        {
            // �ړ����͂�����ꍇ�́A�U�����������s��

            // ������͂���y������̖ڕW�p�x[deg]���v�Z
            var targetAngleY = - Mathf.Atan2(_inputMove.y, _inputMove.x)
                * Mathf.Rad2Deg + 90;

            // �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngleY,
                ref _turnVelocity,
                0.1f
            );

            // �I�u�W�F�N�g�̉�]���X�V
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }

    //�ړ�����
    private void PlayerMove(float speed)//, Vector3 cameraVec
    {
        //�J�����̌������擾����
        //cameraForVec = Camera.main.transform.TransformDirection(Vector3.forward);
        //cameraRightVec = Camera.main.transform.TransformDirection(Vector3.right);

        //�J�����̊p�x���擾����
        // float cameraVecF = cameraVec.z;
        Vector3 moveVelocity = new Vector3(
               _inputMove.x * speed,
               _verticalVelocity,
               _inputMove.y * speed
           ) ;
        // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
        var moveDelta = moveVelocity * Time.deltaTime;//* cameraVecF 
        //�J���~���Ă�����
        //if (isRain)
        //{
           // moveDelta = Vector3.Lerp(moveDelta, groundHit.normal * playerSpeed / 5.0f, Time.deltaTime);
        //}
        // CharacterController�Ɉړ��ʂ��w�肵�A�I�u�W�F�N�g�𓮂���
        _characterController.Move(moveDelta);
        //transform.position = new Vector3(moveDelta.x, moveDelta.y, moveDelta.z);
    }

    //���G���������
    public void StartAvoidance()
    {
        isAvoidansing = true;
        avoiPlam = AvoidansingPlam.Doing;
        anim.SetTrigger("OnRoll");
    }

    //���G�������������
    public void EndAvoidance()
    {
        isAvoidansing = false;
        avoiPlam = AvoidansingPlam.CoolTime;
    }

    //�_�b�V���̊J�n
    public void StartDash()
    {
        playrPlam = PlayerPlam.Dash;
        anim.SetBool("OnDash", true);
    }

    //�_�b�V���̏I��
    public void EndDash()
    {
        playrPlam = PlayerPlam.Normal;
        //�_�b�V���I�����_�ŃX�^�~�i���c���Ă�����
        if(nowStamina > 0.0f)
        {
            staminaPlam = StaminaPlam.Recovery;
        }
        //�X�^�~�i���g���؂���
        else
        {
            staminaPlam = StaminaPlam.NotUse;
        }
        anim.SetBool("OnDash",false);
    }

    //���Ⴊ�݊J�n
    public void StartShit()
    {
        playrPlam = PlayerPlam.Shit;
    }

    //���Ⴊ�ݏI��
    public void EndShit()
    {
        playrPlam = PlayerPlam.Normal;
    }

    //�ڐG����
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //�J���~���Ă���Ȃ�
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



    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Tramporin")
        {
            _verticalVelocity = _jumpSpeed / 2;
            onTramporin = true;
        }
        if(col.tag == "hunn") {
            Debug.Log(col.gameObject);
        }
        if(col.tag == "holl") {
            if(gameManager.ManagerRemain != 0) {
                gameManager.ManagerRemain--;
                if(gameManager.ManagerRemain <= 0) {
                    gameManager.GAMEOVER = true;
                }
            }
            falling = true;
            for(int u = 0; u < planeCol.Length; u++) {
                planeCol[u].enabled = false;
            }
            _playerInput.enabled = false;
           
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
            falling = true;
            _playerInput.enabled = false;
            if(gameManager.ManagerRemain != 0) {
                gameManager.ManagerRemain--;
                if(gameManager.ManagerRemain <= 0) {
                    gameManager.GAMEOVER = true;
                } 
            }
        }

        if(col.tag == "Car") {
            if(gameManager.ManagerRemain != 0) {
                gameManager.ManagerRemain--;
                if(gameManager.ManagerRemain <= 0) {
                    gameManager.GAMEOVER = true;
                }
                falling = true;
                _playerInput.enabled = false;
            }
        }

      

        if(col.tag == "CheckPoint") {

            gameManager.RECOUNT++;
            gameManager.DESSPOS =  col.transform.position;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Tramporin")
        {
            onTramporin = false;
        }
    }
    #endregion

    #region//�����֌W
 

    //�A�C�e���l��
    public void GetItem(int getScore, string itemName)
    {
        //�X�R�A���Z
        gameManager.AddScore(getScore);
        switch(itemName)
        {
            case "Key":
                if(mission.MiSSIONCOUNT < 2) {
                    mission.STATMISSION = false;
                    mission.YBUTTON = false;
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

    //����̃N�[���^�C���v�Z
    float _count = 0.0f;
    private void CountAvoidansing()
    {
        _count += 0.1f;
        //�v�Z�p�ϐ���0�ȉ��ɂȂ�����
        if(_count >= 1.0f)
        {
            _count = 0.0f;
            _runCounter--;
            if(_runCounter <= 0.0f)
            {
                _runCounter = coolTime;
                //����\��Ԃɂ���B
                avoiPlam = AvoidansingPlam.Can;
            }
        }
    }

    [SerializeField] private float showStamina;
    private float runShowStamina = 0.0f;
    private bool finishRecover = false;
    //�X�^�~�i�Q�[�W�̊Ǘ����s��
    private void CountStamina()
    {
        if (nowStamina >= maxStamina)
        {
            nowStamina = maxStamina;
        }

        if (nowStamina >= maxStamina + 5)
        {
            Debug.Log("������");
        }
        var rate = 0.0f;
        //�Q�[�W�����p�̃t���O
        //�X�^�~�i������A�_�b�V�����Ȃ�
        if(staminaPlam != StaminaPlam.NotUse && playrPlam == PlayerPlam.Dash)
        {
            if(finishRecover) finishRecover = false;
            //��\���ɂ��Ă����X�^�~�i�Q�[�W��\������
            for(int count = 0; count < gutsGauge.Length; count++)
            {
                //��\����Ԃ̎��ɂ̂ݎ��s����
                if(!gutsGauge[count].enabled)
                gutsGauge[count].enabled = true;
            }

            //�X�^�~�i���������E�\������
            //nowStamina -= 0.02f;
            rate = nowStamina / maxStamina;
            gutsGauge[0].fillAmount = rate;
            gutsGauge[1].fillAmount = rate + 0.1f;

            //�X�^�~�i���؂ꂽ��
            if(nowStamina <= 0.0f)
            {
                //gutsGauge[0].enabled = false;
                EndDash();
            }
        }

        //�X�^�~�i���؂�Ă���Ȃ�
        else if(staminaPlam != StaminaPlam.CanUse)
        {
            if(staminaPlam == StaminaPlam.NotUse)
            {
                gutsGauge[0].enabled = false;
                //�ړ����̉񕜗�
                if (_inputMove != Vector2.zero)
                {
                    nowStamina += 0.01f;
                }
                //��~���̉񕜗�
                else
                {
                    nowStamina += 0.02f;
                }

                rate = nowStamina / maxStamina;
                gutsGauge[1].fillAmount = rate;

                //�S�񕜊���
                if (rate >= 1)
                {
                    gutsGauge[0].enabled = true;
                    gutsGauge[0].fillAmount = rate;
                    staminaPlam = StaminaPlam.CanUse;
                    finishRecover = true;
                }
            }

            //�X�^�~�i���c���Ă����ԂȂ�
            else if(staminaPlam == StaminaPlam.Recovery)
            {
                //�ړ����̉񕜗�
                if (_inputMove != Vector2.zero)
                {
                    nowStamina += 0.025f;
                }
                //��~���̉񕜗�
                else
                {
                    nowStamina += 0.03f;
                }

                rate = nowStamina / maxStamina;
                gutsGauge[0].fillAmount = rate;
                gutsGauge[1].fillAmount = rate;

                //�S�񕜂���
                if (rate >= 1)
                {
                    staminaPlam = StaminaPlam.CanUse;
                    finishRecover = true;
                }
            }
        }

        //�Q�[�W�̏���
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

    #region//�R���[�`���֌W
    #endregion
}
