using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject card;


    void Start()
    {
       
        int[] arr = {1,2,3,4,5,6,7,8};
        StartCoroutine(MakeBtn(arr));

    }

    void Update()
    {
        
    }
    IEnumerator MakeBtn(int[] arr)//카드 배열함수(코루틴)
    {
        int num = 0;
        for (int i = 0; i < 4; i++)
        {
            float x = 180.0f+380;
            float y = (400 / 1.6f) * i+350;

            for(int a = 0; a < 2; a++)
            {
                if (a != 0)
                {
                    x = -180 + 380;
                }
                num = num + 1;
                GameObject go = Instantiate(card, this.transform);
                go.transform.position = new Vector2(x, y);
                go.GetComponent<StageBtn>().SetStageBtn(num);
            }      
        }
        yield break;
    }
}
