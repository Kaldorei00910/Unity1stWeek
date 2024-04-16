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
    float time = 0.0f;

    public Text name_Text;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
    }
    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            audioSource.PlayOneShot(successSound);//������ҽ� ���
        }
        else
        {
            name_Text.text = "����!!";

            firstCard.CloseCard();
            secondCard.CloseCard();
            audioSource.PlayOneShot(failSound);//������ҽ� ���
        }
        firstCard = null;
        secondCard = null;

        name_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ

    }

    public void close_nameText()
    {
        name_Text.gameObject.SetActive(false);
    }

}
