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

    public AudioSource audioSource; //?�원?????�디?�소??
    public AudioClip failSound;//?�고???�는 ?�디?�클�? (?�디?�소?�에 ?�립???�고 ?�생?�켜????
    public AudioClip successSound;
    public AudioClip stageclearSound;
    public AudioClip timeoverSound;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public Text name_Text;
    public Text Sname_Text;

    public GameObject endTxt;
    public GameObject tryTimeTxt;
    public GameObject point;

    public GameObject firstTracker; //?�펙??카드추적)
    public GameObject secondTracker;

    public GameObject switchScript;

    public GameObject countDown;
    public Text countDownTxt;

    bool secondPick = false;
    
    private float startTime;

    public bool isGameStart = false;
    public int cardCount = 16;//ī�� ��ü ����
    public int cardTryCount = 0;
    public int finalpoint = 0;

    public float time = 40.0f;

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
        switchScript.GetComponent<SwitchColor>().resetList(); //?�깔리스??초기??
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
                timeTxt.color = Color.red;
            }

            if (time <= 0.0f)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
                switchScript.GetComponent<SwitchColor>().resetList();
                StopCoroutine("CountDown");
                countDown.SetActive(false);
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

            Debug.Log("�̸� Ȯ��" + firstCard.nickname);
            Sname_Text.text = firstCard.nickname; //��Ī ���� �� ��µǴ� �̸�
            Sname_Text.gameObject.SetActive(true); // ������ �̸� text Ȱ��ȭ 

            if (cardCount == 0)
            {
                endTxt.SetActive(true);
                tryTimeTxt.SetActive(true);
                tryTimeTxt.GetComponent<Text>().text = "�� " + cardTryCount + "ȸ �õ�";
                point.SetActive(true);
                //float time�� int time���� ����(�������� �Ҽ��� ���� ����)
                int timeInt = Mathf.FloorToInt(time);
                point.GetComponent<Text>().text = (finalpoint + timeInt) + "��";
                audioSource.PlayOneShot(stageclearSound); // ?�리?�효과음
                StartCoroutine(StopAfterDelay(1.0f)); //?�리?�시?�도 1.5초후 ?�래?��?
                                                      // 게임 ?�리?�시 ?�도?�수?� ?�수 ?�장
                switchScript.GetComponent<SwitchColor>().resetList();
                Sname_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ
            }
            firstTracker.SetActive(true);
            secondTracker.SetActive(true);
        }
        else
        {
            name_Text.text = "����!!";
            firstCard.CloseCard();
            secondCard.CloseCard();
            audioSource.PlayOneShot(failSound);//������ҽ� ���
            cardTryCount += 1; //�õ�Ƚ�� ī��Ʈ
            finalpoint -= 2; //��Ī ���� ����
            time -= 2.0f;//�������� �� ���½ð��� �� �پ��� 
            name_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ
            switchScript.GetComponent<SwitchColor>().switchColor();
        }
        StopCoroutine("CountDown"); //
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

    public void CountFlip() //�ι�° ī�� �̼��ý� ī��Ʈ�ٿ� ���� �Լ�
    {
        if (secondPick == false)
        {
            StartCoroutine("CountDown");
        }
    }

    public void SecondPick() //�ι�° ī������ Ȯ��
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

        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 0.0f;
        audioSource.Stop();
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
        switchScript.GetComponent<SwitchColor>().resetList(); //���򸮽�Ʈ �ʱ�ȭ    
    }

}
