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
    // 40�ʺ��� �ð� ����        
        
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
        // 15�ʰ��Ǹ� ȿ���� ����� Ÿ�̸� �� ����

        if (time <= 0.0f)
        {            
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
                
        // 0�ʰ� �Ǹ� ���� ��
    }
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
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
                point.GetComponent<Text>().text = (finalpoint * time) + "��";                
                // ���� Ŭ����� �õ�Ƚ���� ���� ����
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            cardTryCount += 1; //�õ�Ƚ�� ī��Ʈ
            finalpoint -= 2; //��Ī ���� ����
        }
        firstCard = null;
        secondCard = null;
    }

}
