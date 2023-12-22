using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMarker : MonoBehaviour
{
    // �I�u�W�F�N�g���f���J����
    [SerializeField] private Camera _targetCamera;

    // UI��\��������ΏۃI�u�W�F�N�g
    [SerializeField] private Transform _target;

    [SerializeField] Image targetObj;
    // �\������UI
    private Transform _targetUI;

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    [SerializeField] private Vector3 _worldOffset;

    private RectTransform _parentUI;
    [SerializeField] PlayerC playerC;
    GameManager gameManager;
    bool active = false;

    // ���������\�b�h�iPrefab���琶�����鎞�ȂǂɎg���j
    public void Initialize(Transform target, Camera targetCamera = null) {
        _target = target;
        _targetCamera = targetCamera != null ? targetCamera : Camera.main;

        OnUpdatePosition();
    }

    private void Awake() {
        _targetUI = targetObj.GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if(_targetCamera == null)
            _targetCamera = Camera.main;

        // �eUI��RectTransform��ێ�
        _parentUI = _targetUI.parent.GetComponent<RectTransform>();
    }

    // UI�̈ʒu�𖈃t���[���X�V
    private void Update() {
        Debug.Log("�A�N�e�B�u"+active);
        if(playerC.FALLING && !gameManager.GAMEOVER) {
            active = true;
            targetObj.enabled = false;
        }
        if(!gameManager.GAMEOVER && !active) {
            OnUpdatePosition();
        }
        if(active) {
            
            StartCoroutine(WaitFlag());
        }
        if(gameManager.GAMEOVER) {
            targetObj.enabled = false;
        }
    }

    IEnumerator WaitFlag() {
        yield return new WaitForSeconds(4.0f);
        active = false;
        targetObj.enabled = true;
    }

    // UI�̈ʒu���X�V����
    private void OnUpdatePosition() {
        var cameraTransform = _targetCamera.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;
        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = _target.position + _worldOffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        _targetUI.gameObject.SetActive(isFront);
        if(!isFront) return;

        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        _targetUI.localPosition = uiLocalPos;
    }
}
