using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource; //������ �� ������ҽ�
    public AudioClip failSound;//�ְ��� �ϴ� �����Ŭ��, (������ҽ��� Ŭ���� �ְ� ������Ѿ� ��)
    public AudioClip successSound;   

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



    public int cardCount = 16;//ī�� ��ü ����
    public int cardTryCount = 0;
    public int finalpoint = 0;

    float time = 60.0f;

    public Text name_Text;
    public Text Sname_Text;

    // 60�ʺ��� �ð� ����        
        
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
            timeTxt.color = Color.red;
            audioSource.pitch = 1.4f;
        }
        // 15�ʰ��Ǹ� ȿ���� ����� Ÿ�̸� �� ����

        if (time <= 0.0f)
        {            
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
            this.audioSource.Stop();//���� ����� �뷡 ����
        }    
        // 0�ʰ� �Ǹ� ���� ��
        SecondPick(); 
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
                point.GetComponent<Text>().text = (finalpoint + time) + "��";
                this.audioSource.Stop();
                // ���� Ŭ����� �õ�Ƚ���� ���� ����
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
            name_Text.gameObject.SetActive(true);

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

}
