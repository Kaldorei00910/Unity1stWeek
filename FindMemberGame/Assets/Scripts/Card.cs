using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Card : MonoBehaviour
{
    public int idx = 0;
    public AudioSource audioSource;
    public AudioClip click;
    public GameObject front;
    public GameObject back;

    public Animator anim;

    public SpriteRenderer frontImage;

    public SpriteRenderer backColor;
    public GameObject ImageScript;

    public string nickname; // 팀원들 이름
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setting(int number) //이미지 붙이기 함수
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"easy{idx}"); //이미지 가져오기

        switch (number)
        {
            case 0:
                nickname = "수호자 금재은";
                break;

            case 1:
                nickname = "금재은";
                break;

            case 2:
                nickname = "옹호자 국기웅";
                break;

            case 3:
                nickname = "국기웅";
                break;

            case 4:
                nickname = "예술가 이영대";
                break;

            case 5:
                nickname = "이영대";
                break;

            case 6:
                nickname = "논리주의자 이유신";
                break;

            case 7:
                nickname = "이유신";
                break;

            default: //1~7이 아닐 경우 여기로 들어옴.
                nickname = "아무나";
                break;
        }


    }   

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        audioSource.PlayOneShot(click);


        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;

            GameManager.instance.CountFlip();
            Vector2 firstPos = GameManager.instance.firstCard.transform.position; 
            GameManager.instance.firstTracker.transform.position = firstPos; 
            GameManager.instance.firstTracker.SetActive(false);
            GameManager.instance.secondTracker.SetActive(false);
            //check unclosed cards
            if (GameManager.instance.beforeFirst != null && GameManager.instance.beforeSecond != null) 
            {
                GameManager.instance.beforeFirst.CloseCardNow(); 
                GameManager.instance.beforeSecond.CloseCardNow();
            }
        }
        else
        {
            GameManager.instance.secondCard = this;

            Vector2 secondPos = GameManager.instance.secondCard.transform.position; 
            GameManager.instance.secondTracker.transform.position = secondPos;
            GameManager.instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke",0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
        GameManager.instance.Sname_Text.gameObject.SetActive(false); //성공Txt 닫기
        GameManager.instance.firstTracker.SetActive(true); //성공 이펙트 켜기
        GameManager.instance.secondTracker.SetActive(true);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke",0.5f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        GameManager.instance.FailTxt.gameObject.SetActive(false); //실패Txt 닫기

    }

    public void CloseCardNow() //check unclosed card
    {
        Invoke("CloseCardInvoke",0f);
    }

    //카드 뒷면 색상변경 
    //

    public void ChangeColor(string Backcolor)
    {
        if (Backcolor == "green")
        {
            ImageScript.GetComponent<ImageChange>().ChangeGreen();
        }
        else if (Backcolor == "red")
        {
            ImageScript.GetComponent<ImageChange>().ChangeRed();
        }
        else if (Backcolor == "black")
        {
            ImageScript.GetComponent<ImageChange>().ChangeBlack();
        }
    }
}
