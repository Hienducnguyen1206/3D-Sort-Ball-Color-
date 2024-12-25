using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball 
{
    public BallColor ballColor;

    public Ball(BallColor color)
    {
        ballColor = color;
    }


}