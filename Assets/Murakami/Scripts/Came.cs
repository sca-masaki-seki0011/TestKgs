using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Came : MonoBehaviour
{
    public GameObject followObject = null;     // ���_�ƂȂ�I�u�W�F�N�g
    private Vector3 lookPos = Vector3.zero;     // ���ۂɃJ��������������W
    public float lookPlayDistance;   // ���_�̗V��
    public float followSmooth;       // �ǂ�������Ƃ��̑��x
    public float cameraDistance;        // ���_����J�����܂ł̋���
    public float cameraHeight;          // �f�t�H���g�̃J�����̍���
    public float currentCameraHeight;          // ���݂̃J�����̍���

    public float cameraPlayDiatance;    // ���_����J�����܂ł̋����̗V��
    public float leaveSmooth;          // �����Ƃ��̑��x

    public float cameraHeightMin;    // �J�����̍Œ�̍���
    public float cameraHeightMax;    // �J�����̍ő�̍���

    [SerializeField] PlayerC player;

    float rate = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followObject == null) return;
        if(!player.FALLING) {
            UpdateLookPosition();
            UpdateCameraPosition();

            transform.LookAt(lookPos);
        } 
    }

    void UpdateLookPosition()
    {
        // �ڕW�̎��_�ƌ��݂̎��_�̋��������߂�
        Vector3 vec = followObject.transform.position - lookPos;
        float distance = vec.magnitude;

        if (distance > lookPlayDistance)
        {   // �V�т̋����𒴂��Ă�����ڕW�̎��_�ɋ߂Â���
            float move_distance = (distance - lookPlayDistance) * (Time.deltaTime * followSmooth);
            lookPos += vec.normalized * move_distance;
        }
    }

    void UpdateCameraPosition()
    {
        // XZ���ʂɂ�����J�����Ǝ��_�̋������擾����
        Vector3 xz_vec = followObject.transform.position - transform.position;
        xz_vec.y = 0;
        float distance = xz_vec.magnitude;

        // �J�����̈ړ����������߂�
        float move_distance = 0;
        if (distance > cameraDistance + cameraPlayDiatance)
        {   // �J�������V�т𒴂��ė��ꂽ��ǂ�������
            move_distance = distance - (cameraDistance + cameraPlayDiatance);
            move_distance *= Time.deltaTime * followSmooth;
        }
        else if (distance < cameraDistance - cameraPlayDiatance)
        {   // �J�������V�т𒴂��ċ߂Â����痣���
            move_distance = distance - (cameraDistance - cameraPlayDiatance);
            move_distance *= Time.deltaTime * leaveSmooth;
        }

        // �V�����J�����̈ʒu�����߂�
        Vector3 camera_pos = transform.position + (xz_vec.normalized * move_distance);

        // �����͏�Ɍ��݂̎��_����̈��̍������ێ�����
        camera_pos.y = lookPos.y + currentCameraHeight;

        transform.position = camera_pos;
    }

    /*
    public void Roll(float x, float y) {
        // �ړ��O�̋�����ۑ�����
        float prev_distance = Vector3.Distance(followObject.transform.position, transform.position);
        Vector3 pos = transform.position;

        // ���Ɉړ�����
        pos += transform.right * x;

        // �c�Ɉړ�����
        currentCameraHeight = Mathf.Clamp(currentCameraHeight + y, cameraHeightMin, cameraHeightMax);
        pos.y = lookPos.y + currentCameraHeight;

        // �ړ���̋������擾����
        float after_distance = Vector3.Distance(followObject.transform.position, pos);

        // ���_��ΏۂɌ����ċ߂Â���i�V�т��Ȃ����j
        lookPos = Vector3.Lerp(lookPos, followObject.transform.position, 0.1f);

        // �J�����̍X�V
        transform.position = pos;
        transform.LookAt(lookPos);

        // ���s�ړ��ɂ��኱�������ς��̂ŕ␳����
        transform.position += transform.forward * (after_distance - prev_distance);
    }
    */

    public void Reset(float rate = 1)
    {
        // ���_�Ώۂɋ߂Â���
        lookPos = Vector3.Lerp(lookPos, followObject.transform.position, rate);

        // �������f�t�H���g�ɋ߂Â���
        currentCameraHeight = Mathf.Lerp(currentCameraHeight, cameraHeight, rate);

        // �J��������{�ʒu�ɋ߂Â���
        Vector3 pos_goal = followObject.transform.position;
        pos_goal -= followObject.transform.forward * cameraDistance;
        pos_goal.y = followObject.transform.position.y + currentCameraHeight;
        transform.position = Vector3.Lerp(transform.position, pos_goal, rate);

        // �������X�V����
        transform.LookAt(lookPos);
    }
}
