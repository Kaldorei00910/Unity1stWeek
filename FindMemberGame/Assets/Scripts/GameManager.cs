using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource; //음원이 될 오디오소스
    public AudioClip failSound;//넣고자 하는 오디오클립, (오디오소스에 클립을 넣고 재생시켜야 함)
    public AudioClip successSound;
    public AudioClip warningsound;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;

    public GameObject endTxt;
    public GameObject tryTimeTxt;
    public GameObject point;

    public GameObject firstTracker; //이펙트(카드추적)
    public GameObject secondTracker;

    public GameObject switchScript;

    public GameObject countDown;
    public Text countDownTxt;

    bool secondPick = false;
    
    private float startTime;

    public int cardCount = 16;//카드 전체 갯수
    public int cardTryCount = 0;
    public int finalpoint = 0;

    float time = 50.0f;

    public Text name_Text;
    public Text Sname_Text;


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
        cardCount = 16;
        switchScript.GetComponent<SwitchColor>().resetList(); //색깔리스트 초기화
    }
        
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time < 15.0f)
        {
            this.audioSource.PlayOneShot(warningsound);
            timeTxt.color = Color.red;
        }
        // 15초가되면 효과음 재생과 타이머 색 변경

        if (time <= 0.0f)
        {            
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
            this.audioSource.Stop();//게임 종료시 노래 정지
            switchScript.GetComponent<SwitchColor>().resetList(); //색깔리스트 초기화
            StopCoroutine("CountDown"); //카운트다운 함수 중지
            countDown.SetActive(false);
        }    
        // 0초가 되면 게임 끝
        SecondPick(); 
    }
    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            Sname_Text.text = "국기웅,이영대,이유신,금재은";
            
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            audioSource.PlayOneShot(successSound);//오디오소스 재생
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
                //float time을 int time으로 변경(점수에서 소수점 단위 제외)
                int timeInt = Mathf.FloorToInt(time);
                point.GetComponent<Text>().text = (finalpoint + timeInt) + "점";
                this.audioSource.Stop();
                // 게임 클리어시 시도횟수와 점수 등장
                switchScript.GetComponent<SwitchColor>().resetList(); //색깔리스트 초기화
            }
            Sname_Text.gameObject.SetActive(true); // 이름 text 활성화
            firstTracker.SetActive(true);
            secondTracker.SetActive(true);
        }
        else
        {
            name_Text.text = "실패!!";
            firstCard.CloseCard();
            secondCard.CloseCard();
            audioSource.PlayOneShot(failSound);//오디오소스 재생
            cardTryCount += 1; //시도횟수 카운트
            finalpoint -= 2; //매칭 실패 점수
            time -= 2.0f;//실패했을 시 남는시간이 더 줄어들게 
            name_Text.gameObject.SetActive(true); // 실패 text 활성화
            switchScript.GetComponent<SwitchColor>().switchColor();
        }
        StopCoroutine("CountDown"); //카운트다운 함수 중지
        countDown.SetActive(false);
        firstCard = null;
        secondCard = null;        
    }

    public void close_nameText()
    {
        name_Text.gameObject.SetActive(false);
    }

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

    public void CountFlip() //두번째 카드 미선택시 카운트다운 시작 함수
    {
        if (secondPick == false)
        {
            StartCoroutine("CountDown");
        }
    }

    public void SecondPick() //두번째 카드유무 확인
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
    public void close_Sname_Text()
    {
        Sname_Text.gameObject.SetActive(false);
    }

    public void BlackOver()
    {
        endTxt.SetActive(true); 
        Time.timeScale = 0.0f;
        this.audioSource.Stop();
        switchScript.GetComponent<SwitchColor>().resetList(); //색깔리스트 초기화    
    }

}
