using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource audioSource;
    public AudioSource timeoveraudioSource;
    public AudioClip failSound;
    public AudioClip successSound;
    public AudioClip stageclearSound;
    public AudioClip timeoverSound;

    public Card firstCard;
    public Card secondCard;
    public Card beforeFirst; //check unclosed card
    public Card beforeSecond;

    public Text timeTxt;
    public Text name_Text;
    public Text Sname_Text;
    public Text bestScore;

    public GameObject endTxt;
    public GameObject tryTimeTxt;
    public GameObject point;

    public GameObject firstTracker;
    public GameObject secondTracker;

    public GameObject switchScript;

    public GameObject countDown;
    public Text countDownTxt;

    bool secondPick = false;

    private float startTime;

    public bool isGameStart = false;
    public int cardCount = 16;//카드 전체 갯수
    public int cardTryCount = 0;
    public int finalpoint = 0;
    public int highScore = 0;
    public float time = 40.0f;



    // 40초부터 시간 새기



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        cardCount = 16;
        switchScript.GetComponent<SwitchColor>().resetList(); 
    }

    void Update()
    {
        if (isGameStart)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2");
            timeoveraudioSource.volume = 0.05f;
            // 타임오버 사운드 조절

            if (time > 15.0f)
            {
                audioSource.pitch = 1f;
            }
            //클리어하고 다시 시작했을때 소리 빨리 재생안되게 하기
            if (time < 15.0f)
            {
                timeTxt.color = Color.red;
                timeTxt.fontSize = 70;
                audioSource.pitch = 1.4f;                
            }

            if (time <= 0.0f)
            {
                Time.timeScale = 0.0f;
                endTxt.SetActive(true);                
                switchScript.GetComponent<SwitchColor>().resetList();
                StopCoroutine("CountDown");
                countDown.SetActive(false);
                timeoveraudioSource.PlayOneShot(timeoverSound); //타임오버 효과음
                StartCoroutine(StopAfterDelay(1.0f)); //게임 종료시 1.5초후 노래 정지
            }
            // 0초가 되면 게임 끝
            SecondPick();
        }
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

            Sname_Text.text = firstCard.nickname; //매칭 성공 시 출력되는 이름
            Sname_Text.gameObject.SetActive(true); // 성공시 이름 text 활성화 

            if (cardCount == 0)
            {
                endTxt.SetActive(true);
                tryTimeTxt.SetActive(true);
                tryTimeTxt.GetComponent<Text>().text = "총 " + cardTryCount + "회 시도";
                point.SetActive(true);
                //float time을 int time으로 변경(점수에서 소수점 단위 제외)
                int timeInt = Mathf.FloorToInt(time);

                point.GetComponent<Text>().text = (finalpoint + timeInt) + "점";
                if ((finalpoint + timeInt) > instance.highScore)//현재 최고점수보다 높을 경우
                {
                    highScore = finalpoint + timeInt;//최고점수에 현재 점수 도입
                }
                audioSource.PlayOneShot(stageclearSound); // ?�리?�효과음
                StartCoroutine(StopAfterDelay(1.0f)); //?�리?�시?�도 1.5초후 ?�래?��?
                                                      // 게임 ?�리?�시 ?�도?�수?� ?�수 ?�장

                switchScript.GetComponent<SwitchColor>().resetList();
                Sname_Text.gameObject.SetActive(true); // 이름 text 활성화                
            }
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
            name_Text.gameObject.SetActive(true); // 이름 text 활성화
            switchScript.GetComponent<SwitchColor>().switchColor();
        }
        StopCoroutine("CountDown"); 
        countDown.SetActive(false);
        beforeFirst = firstCard;
        beforeSecond = secondCard;
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

    public void StartGame()//게임 플레이 화면으로 넘겨주는 코루틴
    {
        StartCoroutine(GameCoroutine());
    }

    IEnumerator GameCoroutine()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("MainScene");

        while (loadScene != null)
        {
            yield return null;
        }
    }
    public void ToMainScreen()
    {
        isGameStart = false;
        StartCoroutine(MainScreenCoroutine());
    }

    IEnumerator MainScreenCoroutine()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("TitleScene");

        while (loadScene != null)
        {
            yield return null;
        }
    }

    IEnumerator StopAfterDelay(float delay)
    {

        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 0.0f;
        audioSource.Stop();
        audioSource.pitch = 1f;
    }

    public void close_name_Text()
    {
        name_Text.gameObject.SetActive(false);
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
        timeoveraudioSource.PlayOneShot(timeoverSound); // 까만색 두개 골랐을때도 실패효과음 재생
        StartCoroutine(StopAfterDelay(1.0f)); // 이때도 잠시 대기후 종료
    }
}
