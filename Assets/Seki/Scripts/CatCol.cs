using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatCol : MonoBehaviour
{
    [SerializeField] int colCount = 0;
   
    [SerializeField] PlayerInput playerInput;
    CatController cat;
 

    // Start is called before the first frame update
    void Start()
    {
        cat = GetComponentInParent<CatController>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
           if(colCount != 3) {
                cat.DIRATIONCOUNT = colCount;
             
                playerInput.enabled = false;
           } 

        }
    }
}
