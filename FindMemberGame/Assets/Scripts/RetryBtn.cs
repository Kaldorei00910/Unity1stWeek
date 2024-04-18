using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public void Retry()
    {
        //GameManager.instance.StartGame();
        GameManager.instance.ToMainScreen();
        //SceneManager.LoadScene("MainScene");
    }
}
