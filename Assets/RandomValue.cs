using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomValue : MonoBehaviour
{
    int start = 1;
    int end = 10;

    List<int> numbers = new List<int>();

    Text valueText;

    // Start is called before the first frame update
    void Start()
    {
        valueText= this.GetComponent<Text>();
        for(int i = start; i <= end; i++) {
            numbers.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        
        if(Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("ƒiƒ“ƒo["+numbers.Count);

            if(numbers.Count != 0) {

                int index = Random.Range(0, numbers.Count);

                int ransu = numbers[index];
                valueText.text = ransu.ToString();
                Debug.Log(ransu);

                numbers.RemoveAt(index);
            }
        }
            
        
    }
}
