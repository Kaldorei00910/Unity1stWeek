using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{
    public GameObject StagePanel;

    public void GoToGame()
    {
        GameManager.instance.StartGame();//메인 신 전환 및 각종 사전세팅
    }

    public void OpenPanal()
    {
        StagePanel.SetActive(true);
    }
}
