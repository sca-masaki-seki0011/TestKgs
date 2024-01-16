using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAir : MonoBehaviour
{
    //[SerializeField] GameObject airplane;
    int count = 0;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("飛行機ミッションポイント"+count);
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            count++;
            boxCollider.enabled = false;
        }
    }
}
