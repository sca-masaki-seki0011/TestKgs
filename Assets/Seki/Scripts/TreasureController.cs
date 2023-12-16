using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreasureController : MonoBehaviour
{
    [SerializeField] Animator treasure;
    [SerializeField] Animator item;
    [SerializeField] GameObject treasureObj;
    [SerializeField] int treasureCount;
    [SerializeField] MissionManager mission;
    // Start is called before the first frame update
    void Start()
    {
        treasure.enabled = false;
        item.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            treasure.enabled = true;
            item.enabled = true;
            
            StartCoroutine(WaitNotActive());
        }
    }

    IEnumerator WaitNotActive() {
        yield return new WaitForSeconds(4.0f);
        if(treasureCount == 1) {
            mission.MISSIONVALUE[mission.RADOMMISSIONCOUNT]++;
            mission.KeyActive(mission.RADOMMISSIONCOUNT);
        }
        treasureObj.SetActive(false);
    }
}
