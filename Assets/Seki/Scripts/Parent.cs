using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    //�����Ƃ��Ă�
    //�@�ǂɂԂ��������ɂԂ������I�u�W�F�N�g�̎q�Ƀv���C���[��ݒ肷��
    //�A�Ԃ������e�̎q���擾����player���q�Ɠ�����������


   

    [SerializeField]
    GameObject player;

    public Transform Child {
        get {
            return player.transform.GetChild(0).transform;
        }
    }

    CharacterMove2 characterMove2;
    void Start() {
        characterMove2 = this.GetComponent<CharacterMove2>();
        characterMove2.enabled = false;
        //CharacterMove2.target = player.transform.GetChild(0).transform;
    }
    private void Update() {
        
    
        SetParent(player);
    }

    //Invoked when a button is pressed.
    public void SetParent(GameObject newParent) {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        this.transform.parent = newParent.transform;
        Vector3 direction = new Vector3(0f,180f,0f);
        transform.localRotation = Quaternion.Euler(direction);
        //characterMove2.enabled = true;���ƂŃR�����g�A�E�g�O��
        //Display the parent's name in the console.
        //Debug.Log("Player's Parent: " + player.transform.parent.name);

        // Check if the new parent has a parent GameObject.

    }

    private void OnTriggerEnter(Collider other) {
        player = other.gameObject;
    }
}
