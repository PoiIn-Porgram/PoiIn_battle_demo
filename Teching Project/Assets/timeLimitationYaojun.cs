using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class timeLimitationYaojun: MonoBehaviour
{
    public int timeRest = 30;
    public int timePassed = 0;
    public bool start = false;
    public void Update()
    { 
        if (start == true)
        {
            timePass(timePassed);
            start = false;
        }
    }
    public void timePass(int timePassed)
    {
        StartCoroutine(cutDownTime(timePassed));
    }
    new IEnumerator cutDownTime(int timePassed)
    {
        timePassed--;
        timeRest--;
        if (timePassed > 0)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(cutDownTime(timePassed));
        }
        yield return null;
    }
}
