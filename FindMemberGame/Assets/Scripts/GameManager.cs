using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    AudioSource audioSource;
    public AudioClip Ringing;

    public Text timeTxt;
    public GameObject endTxt;
    public GameObject tryTimeTxt;
    public GameObject point;

    public int cardCount = 0;
    public int cardTryCount = 0;
    public int finalpoint = 0;

    float time = 40.0f; 
    // 40초부터 시간 새기        
        
    private void Awake()
    {
        if (instance == null)   
            instance = this;      
    }

    // Start is called before the first frame update
    void Start()
    {                
        Time.timeScale = 1.0f;
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
        
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time < 15.0f)
        {
            this.audioSource.Play();
            timeTxt.color = Color.red;
        }
        // 15초가되면 효과음 재생과 타이머 색 변경

        if (time <= 0.0f)
        {            
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
                
        // 0초가 되면 게임 끝
    }
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            cardTryCount += 1; //시도횟수 카운트
            finalpoint += 10; // 매칭 성공 점수

            if (cardCount == 0)
            {
                endTxt.SetActive(true); 
                Time.timeScale = 0.0f;                
                tryTimeTxt.SetActive(true);
                tryTimeTxt.GetComponent<Text>().text = "총 " + cardTryCount + "회 시도";
                point.SetActive(true);
                point.GetComponent<Text>().text = (finalpoint * time) + "점";                
                // 게임 클리어시 시도횟수와 점수 등장
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            cardTryCount += 1; //시도횟수 카운트
            finalpoint -= 2; //매칭 실패 점수
        }
        firstCard = null;
        secondCard = null;
    }

}
