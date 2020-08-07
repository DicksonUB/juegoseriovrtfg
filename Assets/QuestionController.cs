using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;

public class QuestionController : MonoBehaviour
{
    TextMeshProUGUI inquiryComponent;
    TextMeshProUGUI secondsLeft;
    Button submitButton;
    public string type;
    Toggle toggle1;
    Toggle toggle2;
    Toggle toggle3;
    Toggle toggle4;
    public Canvas canvas;

    float durationInSeconds = 600f;
    Image fillImage;
    private Question question;

    GameController gc;
    public string ActiveToggle()
    {
        if (toggle1 != null && toggle1.isOn)
        {
            return "1";
        }
        else if (toggle2 != null && toggle2.isOn)
        {
            return "2";
        }
        else if (toggle3 != null && toggle3.isOn)
        {
            return "3";
            
        }
        else if (toggle4 != null && toggle4.isOn)
        {
            return "4";
            
        }
        return "-1";
    }
    public void OnSubmit()
    {
        if(ActiveToggle() == question.correctAnswer)
        {
            QuestionPassed();
        }
        else
        {
            QuestionNotPassed();
        }
    }
    
    private void QuestionNotPassed()
    {
        gc.DeletePart(gameObject);
        
        Debug.Log("Question not passed");
    }
    
    private void QuestionPassed()
    {
        
        gc.FoundObject(gameObject.name,type,gameObject.transform.position,gameObject);
        
        Debug.Log("Question passed");
    }
    public void CreateCanvas()
    {
        //var canvasInstance = Instantiate(canvas, transform.position, Quaternion.identity);
        //canvasInstance.transform.parent = gameObject.transform;

        Toggle[] toggles = canvas.GetComponentsInChildren<Toggle>();
        foreach(Toggle t in toggles)
        {
            string name = t.name;
            switch (name)
            {
                case "1":
                    toggle1 = t;
                    break;
                case "2":
                    toggle2 = t;
                    break;
                case "3":
                    toggle3 = t;
                    break;
                case "4":
                    toggle4 = t;
                    break;
            }
        }

        TextMeshProUGUI[] texts = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        foreach(TextMeshProUGUI t in texts)
        {
            if (t.name.Equals("Inquiry"))
            {
                inquiryComponent = t;
            }
            if (t.name.Equals("Seconds"))
            {
                secondsLeft = t;
            }

        }

        submitButton = canvas.GetComponentInChildren<Button>();
        Debug.Log(gameObject.name);
        Debug.Log(canvas.name);
        submitButton.onClick.AddListener(OnSubmit);
        Image[] images = canvas.GetComponentsInChildren<Image>();
        foreach(Image i in images)
        {
            if (i.name.Equals("Timer"))
            {
                fillImage = i;
            }
        }
        fillImage.fillAmount = 1f;
        StartCoroutine(Timer(durationInSeconds));

    }
    public IEnumerator Timer(float duration)
    { 
        float startTime = Time.time;
        float time = duration;
        int timeInt;
        
        // Time.time returns time in seconds from start of game
        while(Time.time - startTime < duration)
        {
            time -= Time.deltaTime;
            fillImage.fillAmount = time / duration;
            timeInt = (int)time;
            secondsLeft.text = timeInt.ToString() + " s.";
            yield return null;
        }
        QuestionNotPassed();

    }
    public void populateCanvas(Question q)
    {
        question = q;
        inquiryComponent.text = q.inquiry;
        toggle1.GetComponentInChildren<TextMeshProUGUI>().text = q.option1;
        toggle2.GetComponentInChildren<TextMeshProUGUI>().text = q.option2;
        toggle3.GetComponentInChildren<TextMeshProUGUI>().text = q.option3;
        if(q.option4 != null)
        {
            toggle4.GetComponentInChildren<TextMeshProUGUI>().text = q.option4;
        }

    }
    public void DisplayCanvas()
    {
        canvas.gameObject.SetActive(true);
        
    }
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }
}