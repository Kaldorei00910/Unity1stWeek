using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource; //음원이 될 오디오소스
    public AudioClip failSound;//넣고자 하는 오디오클립, (오디오소스에 클립을 넣고 재생시켜야 함)
    public AudioClip successSound;
    public AudioClip stageclearSound;
    public AudioClip timeoverSound;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;

    public GameObject endTxt;
    public GameObject tryTimeTxt;
    public GameObject point;


    public GameObject countDown;
    public Text countDownTxt;
    
    private float startTime;
    bool secondPick = false;

    public bool isGameStart = false;

    public int cardCount = 16;//카드 전체 갯수
    public int cardTryCount = 0;
    public int finalpoint = 0;

    public float time = 40.0f;

    public Text name_Text;
    public Text Sname_Text;

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
    }

    void Update()
    {
        if (isGameStart)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2");

            if (time < 15.0f)
            {
                timeTxt.color = Color.red;
                timeTxt.fontSize = 70;
                audioSource.pitch = 1.4f;
            }

            if (time <= 0.0f)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
                audioSource.PlayOneShot(timeoverSound); //타임오버 효과음
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

            if (cardCount == 0)
            {
                 endTxt.SetActive(true);
                 Time.timeScale = 0.0f;
                tryTimeTxt.SetActive(true);
                tryTimeTxt.GetComponent<Text>().text = "총 " + cardTryCount + "회 시도";
                point.SetActive(true);

                point.GetComponent<Text>().text = (finalpoint + time) + "??";
                audioSource.PlayOneShot(stageclearSound); // ?대━?댄슚怨쇱쓬
                StartCoroutine(StopAfterDelay(1.0f)); //?대━?댁떆?먮룄 1.5珥덊썑 ?몃옒?뺤?
                                                          // 寃뚯엫 ?대━?댁떆 ?쒕룄?잛닔?� ?먯닔 ?깆옣
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
                firstCard.ChangeColor();
                secondCard.ChangeColor();
                time -= 2.0f;//실패했을 시 남는시간이 더 줄어들게 
                name_Text.gameObject.SetActive(true); // 이름 text 활성화
            }

            StopCoroutine("CountDown"); //
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

    public void CountFlip()
    {
        if (secondPick == false)
        {
            StartCoroutine("CountDown");
        }
    }

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
    public void close_Sname_Text()
    {
        Sname_Text.gameObject.SetActive(false);
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
        audioSource.Stop();
    }

}
