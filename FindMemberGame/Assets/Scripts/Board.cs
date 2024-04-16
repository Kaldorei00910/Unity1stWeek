using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        StartCoroutine(WaitForIt(arr));//코루틴을 사용한 카드의 순차적 배열
        /*
        for (int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);

            // 남은 카드 갯수 

        }*/

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator WaitForIt(int[] arr)//카드 배열함수(코루틴)
    {
        for (int i = 0; i < 16; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f + 4.63f;
            float y = (i / 4) * 1.4f - 3.0f + 4.63f;

            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
}
