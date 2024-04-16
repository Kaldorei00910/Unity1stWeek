using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource; //음원이 될 오디오소스
    public AudioClip failSound;//넣고자 하는 오디오클립, (오디오소스에 클립을 넣고 재생시켜야 함)
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
            audioSource.PlayOneShot(successSound);//오디오소스 재생
        }
        else
        {
            name_Text.text = "실패!!";

            firstCard.CloseCard();
            secondCard.CloseCard();
            audioSource.PlayOneShot(failSound);//오디오소스 재생
        }
        firstCard = null;
        secondCard = null;

        name_Text.gameObject.SetActive(true); // 이름 text 활성화

    }

    public void close_nameText()
    {
        name_Text.gameObject.SetActive(false);
    }

}
