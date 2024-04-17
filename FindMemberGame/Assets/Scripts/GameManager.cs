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


    public GameObject countDown;
    public Text countDownTxt;

    bool secondPick = false;
    
    private float startTime;
    List<string> GreenPos = new List<string>(); //그린카드 위치 리스트
    List<string> RedPos = new List<string>(); //레드카드 위치 리스트
    List<string> BlackPos = new List<string>(); //블랙카드 위치 리스트

    public string fL; //첫번째 카드 위치 문자열로 받기
    public string sL; //두번째 카드 위치 문자열로 받기
    Vector2 fPos; //첫번째 카드 위치
    Vector2 sPos; //두번째 카드 위치


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
            GreenPos.Clear(); //리스트 초기화
            RedPos.Clear();
            BlackPos.Clear();
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
                point.GetComponent<Text>().text = (finalpoint * time) + "점";
                this.audioSource.Stop();
                // 게임 클리어시 시도횟수와 점수 등장
                GreenPos.Clear(); //리스트 초기화
                RedPos.Clear();
                BlackPos.Clear();
            }

            Sname_Text.gameObject.SetActive(true); // 이름 text 활성화
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
            
            fPos = firstCard.transform.position; //첫번째 카드 위치
            fL = "'" + fPos + "'"; //위치를 문자열로 변경
            sPos = secondCard.transform.position; //첫번째 카드 위치
            sL = "'" + sPos + "'"; //위치를 문자열로 변경

            //두 카드 전부 흰색,
            if (!GreenPos.Contains(fL) && !GreenPos.Contains(sL) && !RedPos.Contains(fL) && !RedPos.Contains(sL) && !BlackPos.Contains(fL) && !BlackPos.Contains(sL))
            {
                GreenPos.Add(fL); //첫번째 카드위치 그린리스트에 추가
                firstCard.ChangeColor("green"); //첫 카드 그린색상
                GreenPos.Add(sL); //두번째 카드위치 그린리스트에 추가
                secondCard.ChangeColor("green");
            }
            else if (GreenPos.Contains(fL) && GreenPos.Contains(sL)) //둘다 그린,
            {
                RedPos.Add(fL); //첫째 레드리스트에 추가
                GreenPos.Remove(fL);//첫째 그린리스트에서 삭제
                firstCard.ChangeColor("red"); 
                RedPos.Add(sL); //둘째 레드리스트에 추가
                secondCard.ChangeColor("red");
                GreenPos.Remove(sL);
            }
            else if (RedPos.Contains(fL) && RedPos.Contains(sL)) //둘다 레드,
            {
                BlackPos.Add(fL); //첫째 블랙리스트에 추가
                RedPos.Remove(fL);//첫째 레드리스트에서 삭제
                firstCard.ChangeColor("black"); 
                BlackPos.Add(sL); //둘째 블랙리스트에 추가
                secondCard.ChangeColor("black");
                RedPos.Remove(sL);;
            }
            //첫째는 그린, 둘째는 그린이 아닐 때
            else if (GreenPos.Contains(fL) && !GreenPos.Contains(sL)) 
            {
                RedPos.Add(fL); //첫째는 레드리스트에 추가
                GreenPos.Remove(fL);
                firstCard.ChangeColor("red"); 
                if (!GreenPos.Contains(sL) && !RedPos.Contains(sL) && !BlackPos.Contains(sL)) //둘째가 흰색이면,
                {
                    GreenPos.Add(sL);//그린리스트에 추가
                    secondCard.ChangeColor("green");
                }
                else if (RedPos.Contains(sL)) //둘째가 레드이면,
                {
                    BlackPos.Add(sL);//블랙리스트에 추가
                    RedPos.Remove(sL);//레드리스트에서 제거
                    secondCard.ChangeColor("black");
                }
            }

            //첫째는 그린이 아니고, 둘째는 그린일 때
            else if (!GreenPos.Contains(fL) && GreenPos.Contains(sL)) 
            {
                RedPos.Add(sL); //둘째는 레드리스트에 추가
                GreenPos.Remove(sL);
                secondCard.ChangeColor("red"); 
                if (!GreenPos.Contains(fL) && !RedPos.Contains(fL) && !BlackPos.Contains(fL)) //첫째가 흰색이면,
                {
                    GreenPos.Add(fL);//그린리스트에 추가
                    firstCard.ChangeColor("green");
                }
                else if (RedPos.Contains(fL)) //첫째가 레드이면,
                {
                    BlackPos.Add(fL);//블랙리스트에 추가
                    RedPos.Remove(fL);//레드리스트에서 제거
                    firstCard.ChangeColor("black");
                }
            }

            //첫째 레드, 둘째는 레드 아닐 때
            else if (RedPos.Contains(fL) && !RedPos.Contains(sL)) 
            {
                BlackPos.Add(fL); //첫째는 블랙리스트에 추가
                RedPos.Remove(fL);
                firstCard.ChangeColor("black"); 
                if (!GreenPos.Contains(sL) && !RedPos.Contains(sL) && !BlackPos.Contains(sL)) //둘째가 흰색이면,
                {
                    GreenPos.Add(sL);//그린리스트에 추가
                    secondCard.ChangeColor("green");
                }
                else if (GreenPos.Contains(sL)) //둘째가 그린이면,
                {
                    RedPos.Add(sL);//레드리스트에 추가
                    GreenPos.Remove(sL);//그린리스트에서 제거
                    secondCard.ChangeColor("red");
                }
            }

            //첫째는 레드가 아니고, 둘째는 레드일 때
            else if (!RedPos.Contains(fL) && RedPos.Contains(sL)) 
            {
                BlackPos.Add(sL); //둘째는 블랙리스트에 추가
                RedPos.Remove(sL); //레드리스트에서 제거
                secondCard.ChangeColor("black"); //블랙
                if (!GreenPos.Contains(fL) && !RedPos.Contains(fL) && !BlackPos.Contains(fL)) //첫째가 흰색이면,
                {
                    GreenPos.Add(fL);//그린리스트에 추가
                    firstCard.ChangeColor("green");
                }
                else if (GreenPos.Contains(fL)) //첫째가 그린이면,
                {
                    RedPos.Add(fL);//레드리스트에 추가
                    GreenPos.Remove(fL);//그린리스트에서 제거
                    firstCard.ChangeColor("red");//레드
                }
            }
            //둘 다 블랙이면, 게임종료
            else if (BlackPos.Contains(fL) && BlackPos.Contains(sL))
            {
                BlackOver();
            }
        }
        StopCoroutine("CountDown"); //카운트다운 함수 중지
        countDown.SetActive(false);
        firstCard = null;
        secondCard = null;
        name_Text.gameObject.SetActive(true); // 이름 text 활성화
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
        GreenPos.Clear(); //리스트 초기화
        RedPos.Clear();
        BlackPos.Clear();    
    }
}
