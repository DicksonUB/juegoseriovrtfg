using System.Collections;

[System.Serializable]
public class Question
{
    public string inquiry;
    public string option0;
    public string option1;
    public string option2;
    public string option3;
    public string answer;

    public Question(string option1, string option2, string option3, string option4, string correctAnswer,string inquiry)
    {
        this.inquiry = inquiry;
        this.option0 = option1;
        this.option1 = option2;
        this.option2 = option3;
        this.option3 = option4;
        this.answer = correctAnswer;
    }
    public Question(string option1, string option2, string option3, string correctAnswer, string inquiry)
    {
        this.inquiry = inquiry; 
        this.option0 = option1;
        this.option1 = option2;
        this.option2 = option3;
        this.answer = correctAnswer;
    }
    public override string ToString()
    {
        return inquiry + "\n" + "1)" + option0 + "\n" + "2)" + option1 + "\n" + "3)" + option2 + "\n" + "4)" + option3 + "\n";
    }
}
