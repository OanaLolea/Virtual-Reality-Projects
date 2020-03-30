using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    private int Minutes = 0;
    private int Seconds = 0;

    public TextMeshPro timer;
    private float m_leftTime;
    

    private void Start()
    {
        //timer = GetComponent<TextMeshPro>();
        this.Seconds = 30;
        m_leftTime = GetInitialTime();
    }

    private void Update()
    {
        if (m_leftTime > 0f)
        {
            //  Update countdown clock
            m_leftTime -= Time.deltaTime;
            Minutes = GetLeftMinutes();
            Seconds = GetLeftSeconds();

            //  Show current clock
            if (m_leftTime > 0f)
            {
                timer.text = "Time : " + Minutes + ":" + Seconds.ToString("00");
            }
            else
            {
                //  The countdown clock has finished
                timer.text = "";
            }
        }
    }

    private float GetInitialTime()
    {
        return Minutes * 60f + Seconds;
    }

    private int GetLeftMinutes()
    {
        return Mathf.FloorToInt(m_leftTime / 60f);
    }

    private int GetLeftSeconds()
    {
        return Mathf.FloorToInt(m_leftTime % 60f);
    }

    public bool isOver()
    {
        return (Minutes == 0 && Seconds==0);
    }
}
