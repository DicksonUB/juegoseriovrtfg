using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Questions
{
    public static List<Question> questions;
    public static List<Question> defaultQuestions;
    public static bool finished;

    public static List<Question> getQuestions()
    {

        if(questions == null || questions.Count == 0)
        {
            bool a = (questions == null);
            bool b = (questions.Count == 0);
            defaultQuestions = new List<Question>();
            Question q = new Question(a.ToString(),b.ToString() , "C", "C", "Example question:Press C");
            for (int i = 0; i < 28; i++)
            {
                defaultQuestions.Add(q);
            }
            return defaultQuestions;
        }
        return questions;
    }
    public static void addQuestion(Question q)
    {
        questions.Add(q);
    }
    
}
