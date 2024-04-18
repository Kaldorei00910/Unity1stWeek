using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public Sprite green_img;
    public Sprite red_img;
    public Sprite black_img;

    Image thisImg;

    // Start is called before the first frame update
    void Start()
    {
        thisImg = GetComponent<Image>();
    }

    // Update is called once per frame

    public void ChangeGreen()
    {
        thisImg.sprite = green_img;
    }
    public void ChangeRed()
    {
        thisImg.sprite = red_img;
    }
    public void ChangeBlack()
    {
        thisImg.sprite = black_img;
    }
}
