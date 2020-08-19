using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Questions
{
    public static List<Question> questions;
    public static List<Question> getQuestions()
    {
        return questions;
    }
    public static void addQuestion(Question q)
    {
        questions.Add(q);
    }
}
