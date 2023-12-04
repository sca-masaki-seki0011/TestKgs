using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinC : MonoBehaviour
{
    [Header("�R�C���̉��l"),SerializeField] private int coinPrice;
    [Header("�~�b�V�������̃R�C��"),SerializeField] private int missionPrice;
    [Header("�X�R�A"),SerializeField] private int getScore;
    [Header("�G�t�F�N�g"), SerializeField] private ParticleSystem partical;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Material[] cainMaterial;
    [SerializeField] private Renderer coinRenderer;
    private float count = 0.0f;
    //[SerializeField] private SlideUIControl uiCont;
    [SerializeField] GameManager gamemanager;

    [SerializeField] MissionManager mission;

    // Start is called before the first frame update
    void Start()
    {
        mission = mission.GetComponent<MissionManager>();
        gamemanager = gamemanager.GetComponent<GameManager>();
        SelectMaterial();
        //StartCoroutine("Title");
    }

    // Update is called once per frame
    void Update()
    {
        //TurnCoin();
    }

    //�F�I��
    private void SelectMaterial()
    {
        switch(coinPrice)
        {
            case 10:
                this.coinRenderer.material = cainMaterial[0];
                break;
            case 100:
                this.coinRenderer.material = cainMaterial[1];
                break;
            case 500:
                this.coinRenderer.material = cainMaterial[2];
                break;
        }
    }

    //�R�C���̐���
    private void TurnCoin()
    {
        //count += 0.1f;
        //transform.position += new Vector3(0, (Mathf.Sin(count / 100)) * (Time.deltaTime), 0);
        //transform.Rotate(0, rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        //�v���C���[�̏ꍇ
        if(col.tag == "Player")
        {
            gamemanager.WINDOWUP = true;
            gamemanager.AddCoin(coinPrice);
            gamemanager.AddScore(getScore);
            if(mission.RADOMMISSIONCOUNT == 2) {
                mission.MISSIONVALUE[mission.RADOMMISSIONCOUNT]+=500;
            } 
            //�G�t�F�N�g�Đ�
            ParticleSystem newPar = Instantiate(partical);
            newPar.transform.position = this.transform.position;
            newPar.Play();

            Destroy(this.gameObject);
            //Debug.Log("��������");
        }
    }


    /*
    private IEnumerator Title()
    {
        yield return new WaitForSeconds(1.0f);
        uiCont.state = 1;
        //yield return new WaitForSeconds(1.0f);
        //uiCont.state = 2;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Title");
    }
    */
}
