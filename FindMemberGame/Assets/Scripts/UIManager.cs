using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void OnEnable()
    {
        GameUIChange();
        Debug.Log("UI 변경");
        GameManager.instance.time = 60.0f;
        GameManager.instance.cardCount = 16;
        GameManager.instance.cardTryCount = 0;
        GameManager.instance.finalpoint = 0;
        GameManager.instance.audioSource = GameManager.instance.gameObject.GetComponent<AudioSource>();

        GameManager.instance.bestScore.GetComponent<Text>().text = "BestScore : " + GameManager.instance.highScore;
        Debug.Log("초기화 완료");
        GameManager.instance.isGameStart = true;

        if (!GameManager.instance.audioSource.isPlaying)
        {
            GameManager.instance.audioSource.Play();
        }
        

        Debug.Log("불값 변경true(uimanager)");
        Time.timeScale = 1.0f;
    }

    public void GameUIChange()
    {
        GameManager.instance.timeTxt = GameObject.Find("TimeTxt").GetComponent<Text>();
        GameManager.instance.countDown = GameObject.Find("CountEmpty").transform.Find("CountDown").gameObject;
        GameManager.instance.countDown.SetActive(true);
        GameManager.instance.countDownTxt = GameObject.Find("CountDownTxt").GetComponent<Text>();
        GameManager.instance.countDown.SetActive(false);
        GameManager.instance.point = GameObject.Find("Canvas").transform.Find("Point").gameObject;
        GameManager.instance.endTxt = GameObject.Find("Canvas").transform.Find("EndTxt").gameObject;
        GameManager.instance.name_Text = GameObject.Find("Canvas").transform.Find("nameTxt").GetComponent<Text>();
        GameManager.instance.Sname_Text = GameObject.Find("Canvas").transform.Find("Sname_Text").GetComponent<Text>();
        GameManager.instance.tryTimeTxt = GameObject.Find("Canvas").transform.Find("TryTimeTxt").gameObject;

        GameManager.instance.firstTracker = GameObject.Find("Tracker").transform.Find("FirstTracker").gameObject;
        GameManager.instance.secondTracker = GameObject.Find("Tracker").transform.Find("SecondTracker").gameObject;

        GameManager.instance.bestScore = GameObject.Find("Canvas").transform.Find("BestScore").GetComponent<Text>();

    }


}
