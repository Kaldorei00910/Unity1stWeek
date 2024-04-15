using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    float time = 0.0f;

    private void Awake()
    {
        if (instance == null)   
            instance = this;
    }
=======

public class GameManager : MonoBehaviour
{
    public Text timeTxt;
    float time = 0.00f;
>>>>>>> 5f736782638a58b7c466cd51a727e5d3571deafe

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
<<<<<<< HEAD
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

=======
>>>>>>> 5f736782638a58b7c466cd51a727e5d3571deafe
}
