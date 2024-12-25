using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Box 
{
    public int size;
    public List<Ball> ballList = new List<Ball>();
    public Box(int size)
    {
        this.size = size;
        
    }

    public bool CheckAllBalls()
    {
        if (ballList.Count > 0)
        {
            BallColor color;
            color = ballList[0].ballColor;

            foreach (Ball b in ballList)
            {
                if (b.ballColor != color) return false;

            }
            return true;


        }
        else
        {
            return true;
        }
    }


    public void PushBall(Ball b)
    {  if(ballList == null)
        {
            Debug.Log("Ballist null");
        }
        else
        {
            ballList.Insert(0, b);
        }
      
    }

    public Ball PopBall()
    { 
        if (ballList.Count > 0)
        {
            Ball b = ballList[0];
            ballList.RemoveAt(0);
            return b;
        }
        else
        {
            Debug.Log("Box is empty");
            return null;
        }
       
    }




}
