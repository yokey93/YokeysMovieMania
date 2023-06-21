using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToComplete = 30f;
    [SerializeField] float timeToAnswer = 10f;
    public bool isAnsweringQuestion;
    public float fillFraction;
    public bool loadNextQuestion;
    public float timerValue;

    void Update()
    {
        UpdateTimer();
    }
    
    // IF WE ANSWERED A QUESTION
    public void CancelTimer()
    {
        timerValue = 0;
    }

    // IF WE ARE ANSWERING OR NOT ANSWERING QUESTIONS
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;


        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToComplete;
            }
            else 
            {
                timerValue = timeToAnswer;
                isAnsweringQuestion = false;
            }
        }

        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToAnswer;
            }
            else
            {

                timerValue = timeToComplete;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
        }
    }

}
