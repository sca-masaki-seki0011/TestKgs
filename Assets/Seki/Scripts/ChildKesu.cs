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
        //�����̎q����S�Ē��ׂ�
        foreach(Transform child in root) {
            //�����̎q����Destroy����
            child.localScale = new Vector3(0,0,0);
        }
    }
    private void DestroyChildAll(Transform root,int c) {
        //�����̎q����S�Ē��ׂ�
        foreach(Transform child in root) {
            //�����̎q����Destroy����
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
