using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{

    public Animator strike1, strike2, strike3;
    private int count;
    public void OnDepthReach(int depth)
    {
        if(depth == 200)
        strike1.Play("Strike");
        else if(depth == 300)
        strike3.Play("Strike");
    }

    public void OnFishCapture()
    {
        strike2.Play("Strike");
    }
    private void Update()
    {
        if (count == 3)
        {
            count = 4;
            Initiate.Fade("Level Complete Scene",Color.black, 4.0f);
        }
    }
}
