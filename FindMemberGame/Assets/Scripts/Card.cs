using System.Collections;
using System.Collections.Generic;
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

    //ī�� �޸� ���󺯰� 
    public void ChangeColor(){
        backColor.color = new Color( 29/ 255f,  179/ 255f,  172/ 255f);
    }
}
