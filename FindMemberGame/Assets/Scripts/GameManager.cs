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

    public AudioSource audioSource; //������ �� ������ҽ�
    public AudioClip failSound;//�ְ��� �ϴ� �����Ŭ��, (������ҽ��� Ŭ���� �ְ� ������Ѿ� ��)
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
    List<string> GreenPos = new List<string>(); //�׸�ī�� ��ġ ����Ʈ
    List<string> RedPos = new List<string>(); //����ī�� ��ġ ����Ʈ
    List<string> BlackPos = new List<string>(); //��ī�� ��ġ ����Ʈ

    public string fL; //ù��° ī�� ��ġ ���ڿ��� �ޱ�
    public string sL; //�ι�° ī�� ��ġ ���ڿ��� �ޱ�
    Vector2 fPos; //ù��° ī�� ��ġ
    Vector2 sPos; //�ι�° ī�� ��ġ


    public int cardCount = 16;//ī�� ��ü ����
    public int cardTryCount = 0;
    public int finalpoint = 0;

    float time = 50.0f;

    public Text name_Text;
    public Text Sname_Text;

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
        // 15�ʰ��Ǹ� ȿ���� ����� Ÿ�̸� �� ����

        if (time <= 0.0f)
        {            
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
            this.audioSource.Stop();//���� ����� �뷡 ����
            GreenPos.Clear(); //����Ʈ �ʱ�ȭ
            RedPos.Clear();
            BlackPos.Clear();
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
                point.GetComponent<Text>().text = (finalpoint * time) + "��";
                this.audioSource.Stop();
                // ���� Ŭ����� �õ�Ƚ���� ���� ����
                GreenPos.Clear(); //����Ʈ �ʱ�ȭ
                RedPos.Clear();
                BlackPos.Clear();
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
            time -= 2.0f;//�������� �� ���½ð��� �� �پ��� 
            name_Text.gameObject.SetActive(true); // ���� text Ȱ��ȭ
            
            fPos = firstCard.transform.position; //ù��° ī�� ��ġ
            fL = "'" + fPos + "'"; //��ġ�� ���ڿ��� ����
            sPos = secondCard.transform.position; //ù��° ī�� ��ġ
            sL = "'" + sPos + "'"; //��ġ�� ���ڿ��� ����

            //�� ī�� ���� ���,
            if (!GreenPos.Contains(fL) && !GreenPos.Contains(sL) && !RedPos.Contains(fL) && !RedPos.Contains(sL) && !BlackPos.Contains(fL) && !BlackPos.Contains(sL))
            {
                GreenPos.Add(fL); //ù��° ī����ġ �׸�����Ʈ�� �߰�
                firstCard.ChangeColor("green"); //ù ī�� �׸�����
                GreenPos.Add(sL); //�ι�° ī����ġ �׸�����Ʈ�� �߰�
                secondCard.ChangeColor("green");
            }
            else if (GreenPos.Contains(fL) && GreenPos.Contains(sL)) //�Ѵ� �׸�,
            {
                RedPos.Add(fL); //ù° ���帮��Ʈ�� �߰�
                GreenPos.Remove(fL);//ù° �׸�����Ʈ���� ����
                firstCard.ChangeColor("red"); 
                RedPos.Add(sL); //��° ���帮��Ʈ�� �߰�
                secondCard.ChangeColor("red");
                GreenPos.Remove(sL);
            }
            else if (RedPos.Contains(fL) && RedPos.Contains(sL)) //�Ѵ� ����,
            {
                BlackPos.Add(fL); //ù° ������Ʈ�� �߰�
                RedPos.Remove(fL);//ù° ���帮��Ʈ���� ����
                firstCard.ChangeColor("black"); 
                BlackPos.Add(sL); //��° ������Ʈ�� �߰�
                secondCard.ChangeColor("black");
                RedPos.Remove(sL);;
            }
            //ù°�� �׸�, ��°�� �׸��� �ƴ� ��
            else if (GreenPos.Contains(fL) && !GreenPos.Contains(sL)) 
            {
                RedPos.Add(fL); //ù°�� ���帮��Ʈ�� �߰�
                GreenPos.Remove(fL);
                firstCard.ChangeColor("red"); 
                if (!GreenPos.Contains(sL) && !RedPos.Contains(sL) && !BlackPos.Contains(sL)) //��°�� ����̸�,
                {
                    GreenPos.Add(sL);//�׸�����Ʈ�� �߰�
                    secondCard.ChangeColor("green");
                }
                else if (RedPos.Contains(sL)) //��°�� �����̸�,
                {
                    BlackPos.Add(sL);//������Ʈ�� �߰�
                    RedPos.Remove(sL);//���帮��Ʈ���� ����
                    secondCard.ChangeColor("black");
                }
            }

            //ù°�� �׸��� �ƴϰ�, ��°�� �׸��� ��
            else if (!GreenPos.Contains(fL) && GreenPos.Contains(sL)) 
            {
                RedPos.Add(sL); //��°�� ���帮��Ʈ�� �߰�
                GreenPos.Remove(sL);
                secondCard.ChangeColor("red"); 
                if (!GreenPos.Contains(fL) && !RedPos.Contains(fL) && !BlackPos.Contains(fL)) //ù°�� ����̸�,
                {
                    GreenPos.Add(fL);//�׸�����Ʈ�� �߰�
                    firstCard.ChangeColor("green");
                }
                else if (RedPos.Contains(fL)) //ù°�� �����̸�,
                {
                    BlackPos.Add(fL);//������Ʈ�� �߰�
                    RedPos.Remove(fL);//���帮��Ʈ���� ����
                    firstCard.ChangeColor("black");
                }
            }

            //ù° ����, ��°�� ���� �ƴ� ��
            else if (RedPos.Contains(fL) && !RedPos.Contains(sL)) 
            {
                BlackPos.Add(fL); //ù°�� ������Ʈ�� �߰�
                RedPos.Remove(fL);
                firstCard.ChangeColor("black"); 
                if (!GreenPos.Contains(sL) && !RedPos.Contains(sL) && !BlackPos.Contains(sL)) //��°�� ����̸�,
                {
                    GreenPos.Add(sL);//�׸�����Ʈ�� �߰�
                    secondCard.ChangeColor("green");
                }
                else if (GreenPos.Contains(sL)) //��°�� �׸��̸�,
                {
                    RedPos.Add(sL);//���帮��Ʈ�� �߰�
                    GreenPos.Remove(sL);//�׸�����Ʈ���� ����
                    secondCard.ChangeColor("red");
                }
            }

            //ù°�� ���尡 �ƴϰ�, ��°�� ������ ��
            else if (!RedPos.Contains(fL) && RedPos.Contains(sL)) 
            {
                BlackPos.Add(sL); //��°�� ������Ʈ�� �߰�
                RedPos.Remove(sL); //���帮��Ʈ���� ����
                secondCard.ChangeColor("black"); //��
                if (!GreenPos.Contains(fL) && !RedPos.Contains(fL) && !BlackPos.Contains(fL)) //ù°�� ����̸�,
                {
                    GreenPos.Add(fL);//�׸�����Ʈ�� �߰�
                    firstCard.ChangeColor("green");
                }
                else if (GreenPos.Contains(fL)) //ù°�� �׸��̸�,
                {
                    RedPos.Add(fL);//���帮��Ʈ�� �߰�
                    GreenPos.Remove(fL);//�׸�����Ʈ���� ����
                    firstCard.ChangeColor("red");//����
                }
            }
            //�� �� ���̸�, ��������
            else if (BlackPos.Contains(fL) && BlackPos.Contains(sL))
            {
                BlackOver();
            }
        }
        StopCoroutine("CountDown"); //ī��Ʈ�ٿ� �Լ� ����
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
    public void close_Sname_Text()
    {
        Sname_Text.gameObject.SetActive(false);
    }

    public void BlackOver()
    {
        endTxt.SetActive(true); 
        Time.timeScale = 0.0f;
        this.audioSource.Stop();
        GreenPos.Clear(); //����Ʈ �ʱ�ȭ
        RedPos.Clear();
        BlackPos.Clear();    
    }
}
