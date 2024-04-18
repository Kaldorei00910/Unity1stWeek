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
        Debug.Log("UI ����");
        GameManager.instance.time = 100.0f;
        GameManager.instance.cardCount = 16;
        GameManager.instance.cardTryCount = 0;
        GameManager.instance.finalpoint = 0;
        GameManager.instance.audioSource = GameManager.instance.gameObject.GetComponent<AudioSource>();
        GameManager.instance.audioSource.GetComponent<AudioSource>().Play();
        GameManager.instance.bestScore.GetComponent<Text>().text = "BestScore : " + GameManager.instance.highScore;
        Debug.Log("�ʱ�ȭ �Ϸ�");
        GameManager.instance.isGameStart = true;
        Debug.Log("�Ұ� ����true(uimanager)");
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
