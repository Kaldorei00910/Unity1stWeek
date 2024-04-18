using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Card : MonoBehaviour
{
    public int idx = 0;
    public AudioSource audioSource; //?�원?????�디?�소??
    public AudioClip click;//?�고???�는 ?�디?�클�? (?�디?�소?�에 ?�립???�고 ?�생?�켜????
    public GameObject front;
    public GameObject back;

    public Animator anim;

    public SpriteRenderer frontImage;

    public SpriteRenderer backColor;

    public string nickname; // ������ �̸�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setting(int number) 
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"easy{idx}");

        switch (number)
        {
            case 0:
                nickname = "��ȣ�� ������";
                break;

            case 1:
                nickname = "������";
                break;

            case 2:
                nickname = "��ȣ�� �����";
                break;

            case 3:
                nickname = "�����";
                break;

            case 4:
                nickname = "������ �̿���";
                break;

            case 5:
                nickname = "�̿���";
                break;

            case 6:
                nickname = "�������� ������";
                break;

            case 7:
                nickname = "������";
                break;

            default: //1~7�� �ƴ� ��� ����� ����.
                nickname = "�ƹ���";
                break;
        }


    }   

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        audioSource.PlayOneShot(click);//?�디?�소???�생


        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
            //5�?????번째 카드 미선?�시 ?�집???�수
            GameManager.instance.CountFlip();
            Vector2 firstPos = GameManager.instance.firstCard.transform.position; //첫번�?카드 ?�치
            GameManager.instance.firstTracker.transform.position = firstPos; //첫번�?카드 ?�치�??�펙???�동
            GameManager.instance.firstTracker.SetActive(false);
            GameManager.instance.secondTracker.SetActive(false);
        }
        else
        {
            GameManager.instance.secondCard = this;
            Vector2 secondPos = GameManager.instance.secondCard.transform.position; //?�번�?카드 ?�치
            GameManager.instance.secondTracker.transform.position = secondPos; //첫번�?카드 ?�치�??�펙???�동
            GameManager.instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke",1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
        GameManager.instance.Sname_Text.gameObject.SetActive(false);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke",1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);//카드�??�시 ?�상?�로 ?�리�? ?�전각을 기본?�로 변�?


        //?�수�?바로 불러?�기
        //GameManager.instance.close_nameText();

        //?�스?��? 바로 가?�오??방법
        GameManager.instance.name_Text.gameObject.SetActive(false);
        Debug.Log("���� ������� ��");

    }

    //ī�� �޸� ���󺯰� 
    //

    public void ChangeColor(string Backcolor)
    {
        if (Backcolor == "green")
        {
            backColor.color = new Color( 29f/ 255f, 179f/ 255f, 172f/ 255f);
        }
        else if (Backcolor == "red")
        {
            backColor.color = new Color( 227f/ 255f, 51f/ 255f, 51f/ 255f);
        }
        else if (Backcolor == "black")
        {
            backColor.color = new Color( 1f/ 255f, 1f/ 255f, 1f/ 255f);
        }
        

    }
}
