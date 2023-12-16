using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionDistanceManager : MonoBehaviour
{
    [SerializeField] GameObject[] MissionObject;

    [SerializeField] TMP_Text[] DistanceText;

    [SerializeField] MissionManager mission;

    [SerializeField] GameObject player;

    float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int intDistance;

    // Update is called once per frame
    void Update()
    {
        if(mission.RADOMMISSIONCOUNT != -1 && mission.RADOMMISSIONCOUNT != 3) {
            distance = Vector3.Distance(player.transform.position,MissionObject[mission.RADOMMISSIONCOUNT].transform.position);
            intDistance = (int)distance;
        
            DistanceText[mission.RADOMMISSIONCOUNT].text = intDistance.ToString() + "m";
            
        }
    }
}
