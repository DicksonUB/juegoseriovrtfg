using Proyecto26;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FirebaseController : MonoBehaviour
{
    
    public TextMeshProUGUI errorMsg;
    public Button playButton;
    public void GetQuestions(string id) {
        Questions.questions = new List<Question>();
        RetrieveQuestionsFromDatabase(id, 1);

    }
    public void RetrieveQuestionsFromDatabase(string id, int i)
    {

        
        RestClient.Get<Question>(new RequestHelper { Uri = "https://vrtrivia-619c6.firebaseio.com/"+id+"/" + i + ".json" }).Then(response =>
        { 
            Question question = response;
            Questions.addQuestion(response);
            Debug.Log(question.option0);
            RetrieveQuestionsFromDatabase(id, i+1);
        }
        ,response =>
        {
            if (i == 1)
            {
                errorMsg.text =  "No questions found with that id";
            }
            else
            {
                errorMsg.text = "Questions downloaded correctly";
                playButton.interactable = true;
            }
        });
        }
}
    

