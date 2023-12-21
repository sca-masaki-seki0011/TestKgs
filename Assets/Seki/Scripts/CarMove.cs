using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CarMove : MonoBehaviour
{
    [SerializeField] GameObject DangerImage;
    [SerializeField] PlayerSurch playersurch;
    [SerializeField] BoxCollider childBox;
    private NavMeshAgent agent;

    [SerializeField] private Transform[] points;
    private int destPoint = 0;

    Vector3 myPos = new Vector3(-16.65f,0f,0f);
    DestroyCar des;
    
    bool carMove = false;
    BoxCollider myBox;
    int co = 0;

    [SerializeField] PauseManager pause;
    [SerializeField] MissionManager mission;

    // Start is called before the first frame update
    void Start()
    {
        myBox = this.GetComponent<BoxCollider>();
        des = GetComponentInParent<DestroyCar>();
        myPos = this.transform.position;
        agent = GetComponent<NavMeshAgent>();
        DangerImage.SetActive(false);
        playersurch = playersurch.GetComponent<PlayerSurch>();
        childBox = childBox.GetComponent<BoxCollider>();
    }

    bool enter = false;

    // Update is called once per frame
    void Update()
    {
        if(!pause.PAUSE && !mission.MISSIONFLAG) {
            agent.speed = 3.5f;
            if(des.ARRIVE) {
                co = 0;
                this.transform.position = myPos;
                StartCoroutine(WaitCol());
                myBox.isTrigger = false;
                agent.enabled = false;
                des.ARRIVE = false;
            }

            if(playersurch.HITPLAYER) {
                co = 4;
                //if(dis < 20.6f) {
                myBox.isTrigger = true;
                agent.enabled = true;
                childBox.enabled = false;
                StartCoroutine(ImageDangerTenmetu());
                playersurch.HITPLAYER = false;
            }

            if(carMove) {
                if(!agent.pathPending && agent.remainingDistance < 0.5f) {

                    GotoNextPoint();
                }
                carMove = false;
            }
        } else {
            agent.speed = 0f;
            myBox.isTrigger = false;
            agent.enabled = false;
            des.ARRIVE = false;
        }
                            //Debug.Log("プレイヤーヒット!!"+ playersurch.HITPLAYER);
        
       
    }

    IEnumerator WaitCol() {
        yield return new WaitForSeconds(3.0f);
        childBox.enabled = true;
    }

    IEnumerator ImageDangerTenmetu() {
        while(co != 0) {

            DangerImage.SetActive(false);


            yield return new WaitForSeconds(0.3f);


            DangerImage.SetActive(true);


            yield return new WaitForSeconds(0.3f);
            co--;

        }
        DangerImage.SetActive(false);
        carMove = true;
        yield break;
    }

    void GotoNextPoint() {

        if(points.Length == 0) {
            
            return;
        }

        //ene.SetBool("walk", false);
        agent.destination = points[destPoint].position;
        //destPoint = (destPoint + 1) % points.Length;
        //Destroy(this.gameObject);
        //

    }

   
}
