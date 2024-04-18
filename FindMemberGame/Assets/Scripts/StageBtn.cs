using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{
    public GameObject StagePanel;
    public Text StageTxt;
    public void GoToGame()
    {
        GameManager.instance.StartGame();//���� �� ��ȯ �� ���� ��������
    }

    public void OpenPanal()
    {
        StagePanel.SetActive(true);
    }

    public void SetStageBtn(int number)
    {
        StageTxt.text = "Stage" + number;
    }
}
