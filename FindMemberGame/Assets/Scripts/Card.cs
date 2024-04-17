using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Card : MonoBehaviour
{
    public int idx = 0;
    public AudioSource audioSource; //������ �� ������ҽ�
    public AudioClip click;//�ְ��� �ϴ� �����Ŭ��, (������ҽ��� Ŭ���� �ְ� ������Ѿ� ��)
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

        audioSource.PlayOneShot(click);//������ҽ� ���


        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
            //5�� �� �� ��° ī�� �̼��ý� ������ �Լ�
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
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);//ī�带 �ٽ� �����·� ������, ȸ������ �⺻���� ����


        //�Լ��� �ٷ� �ҷ�����
        //GameManager.instance.close_nameText();

        //�ؽ�Ʈ�� �ٷ� �������� ���
        GameManager.instance.name_Text.gameObject.SetActive(false);

    }

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
