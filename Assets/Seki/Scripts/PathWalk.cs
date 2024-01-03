using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathWalk : MonoBehaviour
{
    public PathCreator pathCreator;
    float speed = 5.0f;
    float d;
    bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop) { 
        d += speed*Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(d);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "p") {
            stop = true;
        }
    }
}
