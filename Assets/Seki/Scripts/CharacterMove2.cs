using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove2 : MonoBehaviour
{
    Transform target;
    Parent p;
    //[SerializeField] private Transform target;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        p = this.GetComponent<Parent>();
        target = p.Child;
        p.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        var direction = (target.localPosition - transform.localPosition).normalized;
        var d = target.localPosition - transform.localPosition;
 
        if(d.magnitude >= 0.5f) {
            transform.localRotation = Quaternion.LookRotation(direction);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
            
        
        
        //if(direction.magnitude == 1.0f) {
            
        //}
        //Debug.Log("‹——£"+direction.magnitude);
    }

  
}
