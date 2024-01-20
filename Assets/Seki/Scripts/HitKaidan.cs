using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKaidan : MonoBehaviour
{
    [SerializeField] KaidanC kaidanC;
    [SerializeField] int kaidanCount;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            Debug.Log("�q�b�g");
          
            //boxCollider.enabled = false;
            //StartCoroutine(WaitCol());
            kaidanC.DESTPOINT = kaidanCount;
        }
    }
}
