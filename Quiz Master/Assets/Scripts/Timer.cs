using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestiion;
    public bool isAnsweringQuestion;
    public float fillFraction;

    float timerValue;

    public void Start()
    {
    }
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }


    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion)
        {
            if (timerValue <= 0)
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
            else
            {
                fillFraction = timerValue / timeToCompleteQuestion; // kalan zaman b�l� ilk verilen zaman yaparak timer�n� oran�n� ��kar�yourz
           
            }
        }
        else
        {
            if (timerValue <= 0)
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestiion = true;
            }
            else
            {
                fillFraction = timerValue / timeToCompleteQuestion; // kalan zaman b�l� ilk verilen zaman yaparak timer�n� oran�n� ��kar�yourz

            }
        }

        Debug.Log(isAnsweringQuestion+ ": "+ timerValue +" = "+fillFraction);
    }




}
