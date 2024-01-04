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
    Vector3 worldAngle;
    float rY;
    float y = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        worldAngle = this.transform.eulerAngles;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(worldAngle.y <= 120f) { 
        worldAngle.y += y*Time.deltaTime;
            this.transform.eulerAngles = worldAngle;
        }
            Debug.Log("Šp“x"+worldAngle.y);
        if(!stop) { 
        d += speed*Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(d);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "p") {
            stop = true;
        }
    }
}
