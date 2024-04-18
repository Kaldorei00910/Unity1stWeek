using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Card : MonoBehaviour
{
    public int idx = 0;
    public AudioSource audioSource; //음원이 될 오디오소스
    public AudioClip click;//넣고자 하는 오디오클립, (오디오소스에 클립을 넣고 재생시켜야 함)
    public GameObject front;
    public GameObject back;

    public Animator anim;

    public SpriteRenderer frontImage;

    public SpriteRenderer backColor;
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
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }   

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        audioSource.PlayOneShot(click);//오디오소스 재생


        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
            //5초 내 두 번째 카드 미선택시 뒤집는 함수
            GameManager.instance.CountFlip();
        }
        else
        {
            GameManager.instance.secondCard = this;
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
        GameManager.instance.name_Text.gameObject.SetActive(false);
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
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);//카드를 다시 원상태로 돌리고, 회전각을 기본으로 변경

        GameManager.instance.name_Text.gameObject.SetActive(false);

    }
    
    public void ChangeColor(){
        backColor.color = new Color( 29/ 255f,  179/ 255f,  172/ 255f);
    }
}
