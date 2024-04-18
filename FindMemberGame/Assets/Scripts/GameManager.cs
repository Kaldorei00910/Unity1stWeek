using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource; //À½¿øÀÌ µÉ ¿Àµğ¿À¼Ò½º
    public AudioClip failSound;//³Ö°íÀÚ ÇÏ´Â ¿Àµğ¿ÀÅ¬¸³, (¿Àµğ¿À¼Ò½º¿¡ Å¬¸³À» ³Ö°í Àç»ı½ÃÄÑ¾ß ÇÔ)
    public AudioClip successSound;
    public AudioClip stageclearSound;
    public AudioClip timeoverSound;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;

    public GameObject endTxt;
    public GameObject tryTimeTxt;
    public GameObject point;


    public GameObject countDown;
    public Text countDownTxt;
    
    private float startTime;
    bool secondPick = false;

    public bool isGameStart = false;

    public int cardCount = 16;//Ä«µå ÀüÃ¼ °¹¼ö
    public int cardTryCount = 0;
    public int finalpoint = 0;

    public float time = 40.0f;

    public Text name_Text;
    public Text Sname_Text;

    // 40ÃÊºÎÅÍ ½Ã°£ »õ±â

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

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
        if (isGameStart)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2");

            if (time < 15.0f)
            {
                timeTxt.color = Color.red;
                timeTxt.fontSize = 70;
                audioSource.pitch = 1.4f;
            }

            if (time <= 0.0f)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
                audioSource.PlayOneShot(timeoverSound); //Å¸ÀÓ¿À¹ö È¿°úÀ½
                StartCoroutine(StopAfterDelay(1.0f)); //°ÔÀÓ Á¾·á½Ã 1.5ÃÊÈÄ ³ë·¡ Á¤Áö
            }
            // 0ÃÊ°¡ µÇ¸é °ÔÀÓ ³¡
            SecondPick();

        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            Sname_Text.text = "±¹±â¿õ,ÀÌ¿µ´ë,ÀÌÀ¯½Å,±İÀçÀº";

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            audioSource.PlayOneShot(successSound);//¿Àµğ¿À¼Ò½º Àç»ı
            cardCount -= 2;
            cardTryCount += 1; //½ÃµµÈ½¼ö Ä«¿îÆ®
            finalpoint += 10; // ¸ÅÄª ¼º°ø Á¡¼ö

            if (cardCount == 0)
            {
                 endTxt.SetActive(true);
                 Time.timeScale = 0.0f;
                tryTimeTxt.SetActive(true);
                tryTimeTxt.GetComponent<Text>().text = "ÃÑ " + cardTryCount + "È¸ ½Ãµµ";
                point.SetActive(true);

                point.GetComponent<Text>().text = (finalpoint + time) + "??";
                audioSource.PlayOneShot(stageclearSound); // ?´ë¦¬?´íš¨ê³¼ìŒ
                StartCoroutine(StopAfterDelay(1.0f)); //?´ë¦¬?´ì‹œ?ë„ 1.5ì´ˆí›„ ?¸ë˜?•ì?
                                                          // ê²Œì„ ?´ë¦¬?´ì‹œ ?œë„?Ÿìˆ˜?€ ?ìˆ˜ ?±ì¥
            }

            Sname_Text.gameObject.SetActive(true); // ÀÌ¸§ text È°¼ºÈ­
            }
            else
            {
                name_Text.text = "½ÇÆĞ!!";
                firstCard.CloseCard();
                secondCard.CloseCard();
                audioSource.PlayOneShot(failSound);//¿Àµğ¿À¼Ò½º Àç»ı
                cardTryCount += 1; //½ÃµµÈ½¼ö Ä«¿îÆ®
                finalpoint -= 2; //¸ÅÄª ½ÇÆĞ Á¡¼ö
                firstCard.ChangeColor();
                secondCard.ChangeColor();
                time -= 2.0f;//½ÇÆĞÇßÀ» ½Ã ³²´Â½Ã°£ÀÌ ´õ ÁÙ¾îµé°Ô 
                name_Text.gameObject.SetActive(true); // ÀÌ¸§ text È°¼ºÈ­
            }

            StopCoroutine("CountDown"); //
            countDown.SetActive(false);
            firstCard = null;
            secondCard = null;
            name_Text.gameObject.SetActive(true); // ÀÌ¸§ text È°¼ºÈ­
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

    public void CountFlip()
    {
        if (secondPick == false)
        {
            StartCoroutine("CountDown");
        }
    }

    public void SecondPick()
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

    public void StartGame()//°ÔÀÓ ÇÃ·¹ÀÌ È­¸éÀ¸·Î ³Ñ°ÜÁÖ´Â ÄÚ·çÆ¾
    {
        StartCoroutine(GameCoroutine());
    }

    IEnumerator GameCoroutine()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("MainScene");

        while (loadScene != null)
        {
            yield return null;
        }
    }
    public void ToMainScreen()
    {
        isGameStart = false;
        StartCoroutine(MainScreenCoroutine());
    }

    IEnumerator MainScreenCoroutine()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("TitleScene");

        while (loadScene != null)
        {
            yield return null;
        }
    }

    IEnumerator StopAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(1);
        audioSource.Stop();
    }

}
