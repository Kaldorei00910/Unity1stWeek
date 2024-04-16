using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    float time = 0.0f;

    public Text name_Text;
    public Text Sname_Text;

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
            Sname_Text.text = "�����,�̿���,������,������";

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            Sname_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ
        }
        else
        {
            name_Text.text = "����!!";

            firstCard.CloseCard();
            secondCard.CloseCard();
            name_Text.gameObject.SetActive(true); // ���� text Ȱ��ȭ
        }
        firstCard = null;
        secondCard = null;

    }

    public void close_nameText()
    {
        name_Text.gameObject.SetActive(false);
    }

    public void close_Sname_Text()
    {
        Sname_Text.gameObject.SetActive(false);
    }

}
