using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]  bunu yyazmak aasset,dosya kýsmýnda  sað týkladýðýmýzda sanki unity'nin bir parçasýný oluþturur gibi gösterir
[CreateAssetMenu(menuName ="Quiz Question", fileName ="New Question")]
public class QuestionSO : ScriptableObject
{
    
    [TextArea(2,6)] //Inspectordaki textboxun buyukluggunu ayarlar
    [SerializeField]string question = "Enter your question here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField][Range(0,3)] int correctAnswerIndex;

    public string GetQuestion()
    {
        return this.question;
    }

    public string GetAnswers(int index)
    {
        return this.answers[index];
    }
    public int GetCorrectAnswerIndex()
    {
        return this.correctAnswerIndex;
    }


}
