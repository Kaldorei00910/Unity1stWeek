using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Card : MonoBehaviour
{
    public int idx = 0;
    public AudioSource audioSource; //?Œì›?????¤ë””?¤ì†Œ??
    public AudioClip click;//?£ê³ ???˜ëŠ” ?¤ë””?¤í´ë¦? (?¤ë””?¤ì†Œ?¤ì— ?´ë¦½???£ê³  ?¬ìƒ?œì¼œ????
    public GameObject front;
    public GameObject back;

    public Animator anim;

    public SpriteRenderer frontImage;

    public SpriteRenderer backColor;

    public string nickname; // ÆÀ¿øµé ÀÌ¸§
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setting(int number) 
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"easy{idx}");

        switch (number)
        {
            case 0:
                nickname = "¼öÈ£ÀÚ ±İÀçÀº";
                break;

            case 1:
                nickname = "±İÀçÀº";
                break;

            case 2:
                nickname = "¿ËÈ£ÀÚ ±¹±â¿õ";
                break;

            case 3:
                nickname = "±¹±â¿õ";
                break;

            case 4:
                nickname = "¿¹¼ú°¡ ÀÌ¿µ´ë";
                break;

            case 5:
                nickname = "ÀÌ¿µ´ë";
                break;

            case 6:
                nickname = "³í¸®ÁÖÀÇÀÚ ÀÌÀ¯½Å";
                break;

            case 7:
                nickname = "ÀÌÀ¯½Å";
                break;

            default: //1~7ÀÌ ¾Æ´Ò °æ¿ì ¿©±â·Î µé¾î¿È.
                nickname = "¾Æ¹«³ª";
                break;
        }


    }   

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        audioSource.PlayOneShot(click);//?¤ë””?¤ì†Œ???¬ìƒ


        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
            //5ì´?????ë²ˆì§¸ ì¹´ë“œ ë¯¸ì„ ?ì‹œ ?¤ì§‘???¨ìˆ˜
            GameManager.instance.CountFlip();
            Vector2 firstPos = GameManager.instance.firstCard.transform.position; //ì²«ë²ˆì§?ì¹´ë“œ ?„ì¹˜
            GameManager.instance.firstTracker.transform.position = firstPos; //ì²«ë²ˆì§?ì¹´ë“œ ?„ì¹˜ë¡??´í™???´ë™
            GameManager.instance.firstTracker.SetActive(false);
            GameManager.instance.secondTracker.SetActive(false);
        }
        else
        {
            GameManager.instance.secondCard = this;
            Vector2 secondPos = GameManager.instance.secondCard.transform.position; //?ë²ˆì§?ì¹´ë“œ ?„ì¹˜
            GameManager.instance.secondTracker.transform.position = secondPos; //ì²«ë²ˆì§?ì¹´ë“œ ?„ì¹˜ë¡??´í™???´ë™
            GameManager.instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke",1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
        GameManager.instance.Sname_Text.gameObject.SetActive(false);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke",1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);//ì¹´ë“œë¥??¤ì‹œ ?ìƒ?œë¡œ ?Œë¦¬ê³? ?Œì „ê°ì„ ê¸°ë³¸?¼ë¡œ ë³€ê²?


        //?¨ìˆ˜ë¡?ë°”ë¡œ ë¶ˆëŸ¬?¤ê¸°
        //GameManager.instance.close_nameText();

        //?ìŠ¤?¸ë? ë°”ë¡œ ê°€?¸ì˜¤??ë°©ë²•
        GameManager.instance.name_Text.gameObject.SetActive(false);
        Debug.Log("½ÇÆĞ »ç¶óÁö°Ô ÇÔ");

    }

    //Ä«µå µŞ¸é »ö»óº¯°æ 
    //

    public void ChangeColor(string Backcolor)
    {
        if (Backcolor == "green")
        {
            backColor.color = new Color( 29f/ 255f, 179f/ 255f, 172f/ 255f);
        }
        else if (Backcolor == "red")
        {
            backColor.color = new Color( 227f/ 255f, 51f/ 255f, 51f/ 255f);
        }
        else if (Backcolor == "black")
        {
            backColor.color = new Color( 1f/ 255f, 1f/ 255f, 1f/ 255f);
        }
        

    }
}
