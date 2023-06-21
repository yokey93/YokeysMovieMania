using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // needed for changing Question && Button Text
using UnityEngine.UI; // needed to change our button IMAGE

public class Quiz : MonoBehaviour
{
    [Header ("Questions")]
    [SerializeField] TextMeshProUGUI questionText; //drag the question text UI here
    [SerializeField] List<QuestionSO> movieQuestions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header ("Answers")]
    [SerializeField] GameObject[] answerButtons; //drag the Answer Buttons HERE
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header ("Buttons")]
    [SerializeField] Sprite chosenButtonSprite;
    [SerializeField] Sprite defaultButtonSprite;

    [Header ("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header ("SCoRiNg")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score score;

    [Header ("Progress Bar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        score = FindObjectOfType<Score>();
        progressBar.maxValue = movieQuestions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction; // changes fill amount of image by the timer

        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }

        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void GetNextQuestion()
    {
        if (movieQuestions.Count > 0)
        {
            GetRandomQuestion(); // chooses a random Q first
            SetButtonState(true); // activates buttons
            SetDefaultButtonSprites(); // makes button Sprite default image
            DisplayQuestion(); // display random Q chosen

            progressBar.value++;
            score.IncrementQuestionSeen();
        }
    }

    // MAKE EACH BUTTON.IMAGE THE DEFAULT SPRITE
    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultButtonSprite;
        }
    }

    // CONTROLS THE BUTTONS.Button "INTERACTABLE" COMPONENT
    void SetButtonState(bool buttonState)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = buttonState;
        }
    }

    // CHANGE BUTTON AND QUESTION TEXT
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        // loops the answers for each button to display on a buttonText variable (TMPROUGUI)
        for (int i = 0; i < answerButtons.Length; i++)
        {   // BUTTON TEXT IS A CHILD COMPONENT OF BUTTON
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswers(i);
        }
    }

    // CHOOSE RANDOM QUESTION
    void GetRandomQuestion()
    {
        // Random Index variable will decide which Question is chosen
        int index = Random.Range(0, movieQuestions.Count);
        currentQuestion = movieQuestions[index];

        // Remove the Chosen Question From the List 
        if (movieQuestions.Contains(currentQuestion))
        {
            movieQuestions.Remove(currentQuestion);
        }
    }


    void DisplayAnswer(int index)
    {
        Image buttonImage;
        // CHANGE QUESTION TEXT AND BUTTON IMAGE ON RIGHT OR WRONG ANSWER
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct! Good work fam";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = chosenButtonSprite;
            score.IncremenetCorrectAnswers();
        } else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswers(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer is: \n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = chosenButtonSprite;
        }
    }

    // On CLICK() TO CHANGE BUTTON SPRITE - change index in UNITY BUTTON UI && DISABLES BUTTONS
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score" + score.CalculateScore() + "%";
    }
}
