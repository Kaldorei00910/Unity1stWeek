using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{
    public GameObject StagePanel;

    public void GoToGame()
    {
        GameManager.instance.StartGame();//���� �� ��ȯ �� ���� ��������
    }

    public void OpenPanal()
    {
        StagePanel.SetActive(true);
    }
}
