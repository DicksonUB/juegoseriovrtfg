using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;

public abstract class QuestionController : MonoBehaviour
{
    TextMeshProUGUI inquiryComponent;
    TextMeshProUGUI secondsLeft;
    Button submitButton;
    public string type;
    Toggle toggle1;
    Toggle toggle2;
    Toggle toggle3;
    Toggle toggle4;
    GameObject canvas;

    float durationInSeconds = 60f;
    Image fillImage;
    Question question;
    GameObject vr_camera;

    protected GameController gc;

    
    public string ActiveToggle()
    {
        if (toggle1 != null && toggle1.isOn)
        {
            return "A";
        }
        else if (toggle2 != null && toggle2.isOn)
        {
            return "B";
        }
        else if (toggle3 != null && toggle3.isOn)
        {
            return "C";
            
        }
        else if (toggle4 != null && toggle4.isOn)
        {
            return "D";
            
        }
        return "-1";
    }
    public void OnSubmit()
    {
        /*
        GameObject debugCanvas = GameObject.FindGameObjectWithTag("DebugCanvas");
        debugCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "YES";
        */
        StopCoroutine(Timer(durationInSeconds));
        if (ActiveToggle() == question.answer)
        {
            QuestionPassed();
        }
        else
        {
            QuestionNotPassed();
        }
    }

    public abstract void QuestionNotPassed();
    protected void DeactivateCanvas()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        
    }
    protected void ActivateCanvas()
    {
        canvas.GetComponent<Canvas>().enabled = true;
        
    }
    public abstract void QuestionPassed();
    
    public void CreateCanvas()
    {
        //var canvasInstance = Instantiate(canvas, transform.position, Quaternion.identity);
        //canvasInstance.transform.parent = gameObject.transform;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        
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
        
        
        Image[] images = canvas.GetComponentsInChildren<Image>();
        foreach(Image i in images)
        {
            if (i.name.Equals("Timer"))
            {
                fillImage = i;
            }
        }
        fillImage.fillAmount = 1f;
        

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
    public void SetQuestion(Question q)
    {
        question = q;
    }
    public void populateCanvas()
    {
        inquiryComponent.text = question.inquiry;
        toggle1.GetComponentInChildren<TextMeshProUGUI>().text = question.option0;
        toggle2.GetComponentInChildren<TextMeshProUGUI>().text = question.option1;
        toggle3.GetComponentInChildren<TextMeshProUGUI>().text = question.option2;
        if(question.option3 != null)
        {
            toggle4.GetComponentInChildren<TextMeshProUGUI>().text = question.option3;

        }
        else
        {
            toggle4.gameObject.SetActive(false);
        }

    }
    public void DisplayCanvas(Vector3 position)
    {
        canvas.GetComponent<CustomRenderQueue>().enabled = true;
        populateCanvas();
        StopCoroutine(Timer(durationInSeconds));
        StartCoroutine(Timer(durationInSeconds));
        
        vr_camera = GameObject.FindGameObjectWithTag("MainCamera");
        canvas.transform.position = position;

        //canvas.transform.LookAt(canvas.transform.position + vr_camera.transform.rotation * Vector3.forward, vr_camera.transform.rotation * Vector3.up);
        Vector3 difference = vr_camera.transform.position - canvas.transform.position;
        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        canvas.transform.rotation = Quaternion.Euler(0.0f, rotationY -180,0.0f);




        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);
        ActivateCanvas();

    }

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        //canvas = FindObjectOfType<Canvas>();
    }
}