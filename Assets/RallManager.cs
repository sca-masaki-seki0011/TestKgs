using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallManager : MonoBehaviour
{
    [SerializeField] GameObject[] slopeCol;
    [SerializeField] KaidanC kaidanC;
    BoxCollider boxCollider;
    float speed = 4.0f;
    [SerializeField] Transform[] finishPos;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ストップ"+kaidanC.STOP);
        if(kaidanC.UP || kaidanC.DOWN) {
            for(int i = 0; i < slopeCol.Length; i++) {
                slopeCol[i].SetActive(false);
            }
        } 
        if(kaidanC.STOP) {
            
            kaidanC.UP = false;
            kaidanC.DOWN = false;
            StartCoroutine(WaitCol());
            kaidanC.STOP = false;
            kaidanC.ka = false;
            boxCollider.enabled = false;
            kaidanC.enabled = false;
            
        }
    }

    IEnumerator WaitCol() {
        yield return new WaitForSeconds(1.5f);
        for(int i = 0; i < slopeCol.Length; i++) {
            slopeCol[i].SetActive(true);
        }
    }
}
