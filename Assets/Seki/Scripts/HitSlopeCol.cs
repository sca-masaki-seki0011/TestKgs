using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlopeCol : MonoBehaviour
{
    [SerializeField] KaidanC kaidanC;
    [SerializeField] int slopeCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            if(slopeCount == 1) {
                kaidanC.enabled = true;
                kaidanC.ka = true;
                kaidanC.UP = true;
                
            }
            if(slopeCount == 2) {
                kaidanC.enabled = true;
                kaidanC.ka = true;
                kaidanC.DOWN = true;
                
            }
        }
    }
}
