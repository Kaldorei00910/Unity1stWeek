using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;

    public GameObject countDown;
    public Text countDownTxt;
    
    private float startTime;
    bool secondPick = false;

    float time = 0.0f;

    private void Awake()
    {
        if (instance == null)   
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        SecondPick(); //두번째 카드가 있는지 확인하는 함수
    }
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            //한번 뒤집은 카드 색상 변경
            firstCard.ChangeColor();
            secondCard.ChangeColor();
            //매칭실패로 남은 시간 2초 감소
            time -= 2.0f;
        }
        StopCoroutine("CountDown"); //카운트 다운 함수 중지
        countDown.SetActive(false);
        firstCard = null;
        secondCard = null;
    }
    //5초 카운트 다운 후 뒤집는 함수
    public IEnumerator CountDown()
    {
        countDown.SetActive(true);
        countDownTxt.text = "5";
        startTime = Time.realtimeSinceStartup;
        yield return new WaitForSecondsRealtime(1);
        countDownTxt.text = "4";
        yield return new WaitForSecondsRealtime(1);
        countDownTxt.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countDownTxt.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countDownTxt.text = "1";
        yield return new WaitForSecondsRealtime(1);
        firstCard.CloseCard();
        countDown.SetActive(false);
        firstCard = null;
    }

    //두 번째 카드 미선택시 뒤집는 함수를 실행하는 함수
    public void CountFlip()
    {
        if (secondPick == false)
        {
            StartCoroutine("CountDown");
        }
    }

    //두번째 카드의 존재유무를 말해주는 함수
    public void SecondPick()
    {
        if (secondCard == null)
        {
            secondPick = false;
        }
        else
        {
            secondPick = true;
        }
    }

}
