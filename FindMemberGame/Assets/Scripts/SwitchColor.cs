using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : MonoBehaviour
{
    List<string> GreenPos = new List<string>(); //그린카드 위치 리스트
    List<string> RedPos = new List<string>(); //레드카드 위치 리스트
    List<string> BlackPos = new List<string>(); //블랙카드 위치 리스트

    Vector2 fPos; //첫번째 카드 위치
    Vector2 sPos; //두번째 카드 위치

    string fL; //첫번째 카드 위치 문자열로 받기
    string sL; //두번째 카드 위치 문자열로 받기

    public void switchColor()
    {
        fPos = GameManager.instance.firstCard.transform.position; //첫번째 카드 위치
        fL = "'" + fPos + "'"; //위치를 문자열로 변경
        sPos = GameManager.instance.secondCard.transform.position; //첫번째 카드 위치
        sL = "'" + sPos + "'"; //위치를 문자열로 변경

        //두 카드 전부 흰색,
        if (!GreenPos.Contains(fL) && !GreenPos.Contains(sL) && !RedPos.Contains(fL) && !RedPos.Contains(sL) && !BlackPos.Contains(fL) && !BlackPos.Contains(sL))
        {
            GreenPos.Add(fL); //첫번째 카드위치 그린리스트에 추가
            GameManager.instance.firstCard.ChangeColor("green"); //첫 카드 그린색상
            GreenPos.Add(sL); //두번째 카드위치 그린리스트에 추가
            GameManager.instance.secondCard.ChangeColor("green");
        }
        else if (GreenPos.Contains(fL) && GreenPos.Contains(sL)) //둘다 그린,
        {
            RedPos.Add(fL); //첫째 레드리스트에 추가
            GreenPos.Remove(fL);//첫째 그린리스트에서 삭제
            GameManager.instance.firstCard.ChangeColor("red");
            RedPos.Add(sL); //둘째 레드리스트에 추가
            GameManager.instance.secondCard.ChangeColor("red");
            GreenPos.Remove(sL);
        }
        else if (RedPos.Contains(fL) && RedPos.Contains(sL)) //둘다 레드,
        {
            BlackPos.Add(fL); //첫째 블랙리스트에 추가
            RedPos.Remove(fL);//첫째 레드리스트에서 삭제
            GameManager.instance.firstCard.ChangeColor("black"); 
            BlackPos.Add(sL); //둘째 블랙리스트에 추가
            GameManager.instance.secondCard.ChangeColor("black");
            RedPos.Remove(sL);;
        }
        //첫째는 그린, 둘째는 그린이 아닐 때
        else if (GreenPos.Contains(fL) && !GreenPos.Contains(sL)) 
        {
            RedPos.Add(fL); //첫째는 레드리스트에 추가
            GreenPos.Remove(fL);
            GameManager.instance.firstCard.ChangeColor("red"); 
            if (!GreenPos.Contains(sL) && !RedPos.Contains(sL) && !BlackPos.Contains(sL)) //둘째가 흰색이면,
            {
                GreenPos.Add(sL);//그린리스트에 추가
                GameManager.instance.secondCard.ChangeColor("green");
            }
            else if (RedPos.Contains(sL)) //둘째가 레드이면,
            {
                BlackPos.Add(sL);//블랙리스트에 추가
                RedPos.Remove(sL);//레드리스트에서 제거
                GameManager.instance.secondCard.ChangeColor("black");
            }
        }

            //첫째는 그린이 아니고, 둘째는 그린일 때
        else if (!GreenPos.Contains(fL) && GreenPos.Contains(sL)) 
        {
            RedPos.Add(sL); //둘째는 레드리스트에 추가
            GreenPos.Remove(sL);
            GameManager.instance.secondCard.ChangeColor("red"); 
            if (!GreenPos.Contains(fL) && !RedPos.Contains(fL) && !BlackPos.Contains(fL)) //첫째가 흰색이면,
            {
                GreenPos.Add(fL);//그린리스트에 추가
                GameManager.instance.firstCard.ChangeColor("green");
            }
            else if (RedPos.Contains(fL)) //첫째가 레드이면,
            {
                BlackPos.Add(fL);//블랙리스트에 추가
                RedPos.Remove(fL);//레드리스트에서 제거
                GameManager.instance.firstCard.ChangeColor("black");
            }
        }

            //첫째 레드, 둘째는 레드 아닐 때
        else if (RedPos.Contains(fL) && !RedPos.Contains(sL)) 
        {
            BlackPos.Add(fL); //첫째는 블랙리스트에 추가
            RedPos.Remove(fL);
            GameManager.instance.firstCard.ChangeColor("black"); 
            if (!GreenPos.Contains(sL) && !RedPos.Contains(sL) && !BlackPos.Contains(sL)) //둘째가 흰색이면,
            {
                GreenPos.Add(sL);//그린리스트에 추가
                GameManager.instance.secondCard.ChangeColor("green");
            }
            else if (GreenPos.Contains(sL)) //둘째가 그린이면,
            {
                RedPos.Add(sL);//레드리스트에 추가
                GreenPos.Remove(sL);//그린리스트에서 제거
                GameManager.instance.secondCard.ChangeColor("red");
            }
        }

            //첫째는 레드가 아니고, 둘째는 레드일 때
        else if (!RedPos.Contains(fL) && RedPos.Contains(sL)) 
        {
            BlackPos.Add(sL); //둘째는 블랙리스트에 추가
            RedPos.Remove(sL); //레드리스트에서 제거
            GameManager.instance.secondCard.ChangeColor("black"); //블랙
            if (!GreenPos.Contains(fL) && !RedPos.Contains(fL) && !BlackPos.Contains(fL)) //첫째가 흰색이면,
            {
                GreenPos.Add(fL);//그린리스트에 추가
                GameManager.instance.firstCard.ChangeColor("green");
            }
            else if (GreenPos.Contains(fL)) //첫째가 그린이면,
            {
                RedPos.Add(fL);//레드리스트에 추가
                GreenPos.Remove(fL);//그린리스트에서 제거
                GameManager.instance.firstCard.ChangeColor("red");//레드
            }
        }
            
        //첫째는 블랙. 둘째는 다른 색일 때
        else if (BlackPos.Contains(fL) && !BlackPos.Contains(sL))
        {
            if(!GreenPos.Contains(sL) && !RedPos.Contains(sL) && !BlackPos.Contains(sL)) //둘째가 흰색
            {
                GreenPos.Add(sL);//그린리스트에 추가
                GameManager.instance.secondCard.ChangeColor("green");
            }
            else if (GreenPos.Contains(sL)) //둘째가 그린,
            {
                RedPos.Add(sL);//레드리스트에 추가
                GreenPos.Remove(sL);//그린리스트에서 제거
                GameManager.instance.secondCard.ChangeColor("red");//레드
            }
            else if (RedPos.Contains(sL)) //둘째가 레드,
            {
                BlackPos.Add(sL);//블랙리스트에 추가
                RedPos.Remove(sL);//레드리스트에서 제거
                GameManager.instance.secondCard.ChangeColor("black");//블랙
            }
        }

        //첫째는 다른 색, 둘째가 블랙일 때
        else if (!BlackPos.Contains(fL) && BlackPos.Contains(sL))
        {
            if(!GreenPos.Contains(fL) && !RedPos.Contains(fL) && !BlackPos.Contains(fL)) //첫째가 흰색
            {
                GreenPos.Add(fL);//그린리스트에 추가
                GameManager.instance.firstCard.ChangeColor("green");
            }
            else if (GreenPos.Contains(fL)) //첫째가 그린,
            {
                RedPos.Add(fL);//레드리스트에 추가
                GreenPos.Remove(fL);//그린리스트에서 제거
                GameManager.instance.firstCard.ChangeColor("red");//레드
            }
            else if (RedPos.Contains(fL)) //첫째가 레드,
            {
                BlackPos.Add(fL);//블랙리스트에 추가
                RedPos.Remove(fL);//레드리스트에서 제거
                GameManager.instance.firstCard.ChangeColor("black");//블랙
            }
        }
        //둘 다 블랙이면, 게임종료
        else if (BlackPos.Contains(fL) && BlackPos.Contains(sL))
        {
            GameManager.instance.BlackOver();
        }
    }
    public void resetList() //위치리스트 초기화 함수
    {
        GreenPos.Clear(); 
        RedPos.Clear();
        BlackPos.Clear();
    }       
}
