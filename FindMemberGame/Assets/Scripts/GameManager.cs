using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource; //������ �� ������ҽ�
    public AudioClip failSound;//�ְ��� �ϴ� �����Ŭ��, (������ҽ��� Ŭ���� �ְ� ������Ѿ� ��)
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

    public int cardCount = 16;//ī�� ��ü ����
    public int cardTryCount = 0;
    public int finalpoint = 0;

    public float time = 40.0f;

    public Text name_Text;
    public Text Sname_Text;

    // 40�ʺ��� �ð� ����

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
                audioSource.PlayOneShot(timeoverSound); //Ÿ�ӿ��� ȿ����
                StartCoroutine(StopAfterDelay(1.0f)); //���� ����� 1.5���� �뷡 ����
            }
            // 0�ʰ� �Ǹ� ���� ��
            SecondPick();

        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            Sname_Text.text = "�����,�̿���,������,������";

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            audioSource.PlayOneShot(successSound);//������ҽ� ���
            cardCount -= 2;
            cardTryCount += 1; //�õ�Ƚ�� ī��Ʈ
            finalpoint += 10; // ��Ī ���� ����

            if (cardCount == 0)
            {
                 endTxt.SetActive(true);
                 Time.timeScale = 0.0f;
                tryTimeTxt.SetActive(true);
                tryTimeTxt.GetComponent<Text>().text = "�� " + cardTryCount + "ȸ �õ�";
                point.SetActive(true);

                point.GetComponent<Text>().text = (finalpoint + time) + "??";
                audioSource.PlayOneShot(stageclearSound); // ?�리?�효과음
                StartCoroutine(StopAfterDelay(1.0f)); //?�리?�시?�도 1.5초후 ?�래?��?
                                                          // 게임 ?�리?�시 ?�도?�수?� ?�수 ?�장
            }

            Sname_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ
            }
            else
            {
                name_Text.text = "����!!";
                firstCard.CloseCard();
                secondCard.CloseCard();
                audioSource.PlayOneShot(failSound);//������ҽ� ���
                cardTryCount += 1; //�õ�Ƚ�� ī��Ʈ
                finalpoint -= 2; //��Ī ���� ����
                firstCard.ChangeColor();
                secondCard.ChangeColor();
                time -= 2.0f;//�������� �� ���½ð��� �� �پ��� 
                name_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ
            }

            StopCoroutine("CountDown"); //
            countDown.SetActive(false);
            firstCard = null;
            secondCard = null;
            name_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ
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

    public void StartGame()//���� �÷��� ȭ������ �Ѱ��ִ� �ڷ�ƾ
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
