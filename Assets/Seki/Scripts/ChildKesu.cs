using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildKesu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyChildAlls(this.transform);
    }

    private void OnBecameVisible() {
        //DestroyChildAll(this.transform,0);
    }

    private void OnBecameInvisible() {
        //DestroyChildAll(this.transform, 1);
    }
    private void DestroyChildAlls(Transform root) {
        //自分の子供を全て調べる
        foreach(Transform child in root) {
            //自分の子供をDestroyする
            child.localScale = new Vector3(0,0,0);
        }
    }
    private void DestroyChildAll(Transform root,int c) {
        //自分の子供を全て調べる
        foreach(Transform child in root) {
            //自分の子供をDestroyする
            if(c == 0) {
                //child.SetActive(false);
            }
            if(c == 1) {
                //child.SetActive(true);
                Destroy(child.gameObject);
            }
        }
    }
}
