using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestionPackage;
    [SerializeField] List<QuestionSO> questionPaackageList= new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly=true;

    [Header("Button Sprites")]
    [SerializeField] Sprite defAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;


    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questionPaackageList.Count;
        progressBar.value = 0;
    }


    public void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestiion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;

            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestiion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);

        }
    }

    public void ControlAnswer(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
     
        
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestionPackage.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.correctAnswers++;
        }
        else
        {
            correctAnswerIndex = currentQuestionPackage.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestionPackage.GetAnswers(correctAnswerIndex);
            questionText.text = "Ups! Correct answer is:\n  " + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;

        }
    }

    void DisplayQuestion()
    {

        questionText.text = currentQuestionPackage.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestionPackage.GetAnswers(i);
        }

    }

    void GetNextQuestion()
    {
        if (questionPaackageList.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value += 1;
            scoreKeeper.questionSeen++;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0,questionPaackageList.Count);
      
            currentQuestionPackage = questionPaackageList[index];

            if (questionPaackageList.Contains(currentQuestionPackage))
            {
                questionPaackageList.Remove(currentQuestionPackage);
            }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
           Image buttonImage = answerButtons[i].GetComponent<Image>();
           buttonImage.sprite = defAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }

    }


}


