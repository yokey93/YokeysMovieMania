using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuizQ", fileName = "NewQ")]

public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Test Question Text Here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;  // example "what yr did lennon die" in the Unity UI
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;  // example 2 is given in the Unity UI
    }

    public string GetAnswers (int index)
    {
        return answers[index];
    }

}
