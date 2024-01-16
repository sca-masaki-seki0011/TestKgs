using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeC : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*�Q�[���p�b�h�i�f�o�C�X�擾�j
        var gamepad = Gamepad.current;
        if(gamepad == null) return;

        // �Q�[���p�b�h�̍��E�̃X�e�B�b�N�̓��͒l���擾
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();

        var vertical = leftStick*-1.0f;
        var horizontal = rightStick * -1.0f;
        var direction = new Vector3(horizontal, 0.0f, vertical);
        */

        var vertical = Input.GetAxis("Vertical") * -1.0f;
        var horizontal = Input.GetAxis("Horizontal") * -1.0f;
        var direction = new Vector3(horizontal, 0.0f, vertical);

        transform.localPosition += direction * (moveSpeed * Time.deltaTime);
    }

 
}
