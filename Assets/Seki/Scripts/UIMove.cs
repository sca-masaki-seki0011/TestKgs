using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMove : MonoBehaviour
{
    //RectTransform rec_my;
    [SerializeField] RectTransform ene;
    
    [SerializeField] RectTransform canvasRectTrans;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = Vector2.zero;
        var camera = Camera.main;
        var screenPos = RectTransformUtility.WorldToScreenPoint(camera, this.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTrans, screenPos, camera, out newPos);
        
    }
}
