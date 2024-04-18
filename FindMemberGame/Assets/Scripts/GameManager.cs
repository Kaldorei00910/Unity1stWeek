using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource; //?Œì›?????¤ë””?¤ì†Œ??
    public AudioClip failSound;//?£ê³ ???˜ëŠ” ?¤ë””?¤í´ë¦? (?¤ë””?¤ì†Œ?¤ì— ?´ë¦½???£ê³  ?¬ìƒ?œì¼œ????
    public AudioClip successSound;
    public AudioClip stageclearSound;
    public AudioClip timeoverSound;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public Text name_Text;
    public Text Sname_Text;

    public GameObject endTxt;
    public GameObject tryTimeTxt;
    public GameObject point;

    public GameObject firstTracker; //?´í™??ì¹´ë“œì¶”ì )
    public GameObject secondTracker;

    public GameObject switchScript;

    public GameObject countDown;
    public Text countDownTxt;

    bool secondPick = false;
    
    private float startTime;

    public bool isGameStart = false;
    public int cardCount = 16;//Ä«µå ÀüÃ¼ °¹¼ö
    public int cardTryCount = 0;
    public int finalpoint = 0;

    public float time = 40.0f;

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
        switchScript.GetComponent<SwitchColor>().resetList(); //?‰ê¹”ë¦¬ìŠ¤??ì´ˆê¸°??
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
                timeTxt.color = Color.red;
            }

            if (time <= 0.0f)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
                switchScript.GetComponent<SwitchColor>().resetList();
                StopCoroutine("CountDown");
                countDown.SetActive(false);
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

            Debug.Log("ÀÌ¸§ È®ÀÎ" + firstCard.nickname);
            Sname_Text.text = firstCard.nickname; //¸ÅÄª ¼º°ø ½Ã Ãâ·ÂµÇ´Â ÀÌ¸§
            Sname_Text.gameObject.SetActive(true); // ¼º°ø½Ã ÀÌ¸§ text È°¼ºÈ­ 

            if (cardCount == 0)
            {
                endTxt.SetActive(true);
                tryTimeTxt.SetActive(true);
                tryTimeTxt.GetComponent<Text>().text = "ÃÑ " + cardTryCount + "È¸ ½Ãµµ";
                point.SetActive(true);
                //float timeÀ» int timeÀ¸·Î º¯°æ(Á¡¼ö¿¡¼­ ¼Ò¼öÁ¡ ´ÜÀ§ Á¦¿Ü)
                int timeInt = Mathf.FloorToInt(time);
                point.GetComponent<Text>().text = (finalpoint + timeInt) + "Á¡";
                audioSource.PlayOneShot(stageclearSound); // ?´ë¦¬?´íš¨ê³¼ìŒ
                StartCoroutine(StopAfterDelay(1.0f)); //?´ë¦¬?´ì‹œ?ë„ 1.5ì´ˆí›„ ?¸ë˜?•ì?
                                                      // ê²Œì„ ?´ë¦¬?´ì‹œ ?œë„?Ÿìˆ˜?€ ?ìˆ˜ ?±ì¥
                switchScript.GetComponent<SwitchColor>().resetList();
                Sname_Text.gameObject.SetActive(true); // ÀÌ¸§ text È°¼ºÈ­
            }
            firstTracker.SetActive(true);
            secondTracker.SetActive(true);
        }
        else
        {
            name_Text.text = "½ÇÆĞ!!";
            firstCard.CloseCard();
            secondCard.CloseCard();
            audioSource.PlayOneShot(failSound);//¿Àµğ¿À¼Ò½º Àç»ı
            cardTryCount += 1; //½ÃµµÈ½¼ö Ä«¿îÆ®
            finalpoint -= 2; //¸ÅÄª ½ÇÆĞ Á¡¼ö
            time -= 2.0f;//½ÇÆĞÇßÀ» ½Ã ³²´Â½Ã°£ÀÌ ´õ ÁÙ¾îµé°Ô 
            name_Text.gameObject.SetActive(true); // ÀÌ¸§ text È°¼ºÈ­
            switchScript.GetComponent<SwitchColor>().switchColor();
        }
        StopCoroutine("CountDown"); //
        countDown.SetActive(false);
        firstCard = null;
        secondCard = null;
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

    public void CountFlip() //µÎ¹øÂ° Ä«µå ¹Ì¼±ÅÃ½Ã Ä«¿îÆ®´Ù¿î ½ÃÀÛ ÇÔ¼ö
    {
        if (secondPick == false)
        {
            StartCoroutine("CountDown");
        }
    }

    public void SecondPick() //µÎ¹øÂ° Ä«µåÀ¯¹« È®ÀÎ
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

        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 0.0f;
        audioSource.Stop();
    }

    public void close_name_Text()
    {
        name_Text.gameObject.SetActive(false);
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
        switchScript.GetComponent<SwitchColor>().resetList(); //»ö±ò¸®½ºÆ® ÃÊ±âÈ­    
    }

}
