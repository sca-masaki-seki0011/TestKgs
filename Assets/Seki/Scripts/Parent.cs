using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    //順序としては
    //①壁にぶつかった時にぶつかったオブジェクトの子にプレイヤーを設定する
    //②ぶつかった親の子を取得してplayerを子と同じく動かす


   

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
        //characterMove2.enabled = true;あとでコメントアウト外す
        //Display the parent's name in the console.
        //Debug.Log("Player's Parent: " + player.transform.parent.name);

        // Check if the new parent has a parent GameObject.

    }

    private void OnTriggerEnter(Collider other) {
        player = other.gameObject;
    }
}
