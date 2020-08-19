using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    List<Question> questions = null;
    List<Question> questionsGame = null;
    
    
    private List<List<GameObject>> parts;
    public List<GameObject> carKeys;
    public List<GameObject> carGas;
    public List<GameObject> carBatteries;
    public List<GameObject> carWirecutters;
    public List<GameObject> helicopterBags;
    public List<GameObject> helicopterBooks;
    public List<GameObject> helicopterGears;
    public List<GameObject> helicopterCrowbars;
    public List<GameObject> doorKeys;
    public List<GameObject> doorFakeIds;
    public List<GameObject> doorMasks;
    public List<GameObject> doorLuggages;
    public GameObject progressCar;
    Transform [] progressCarParts;
    public GameObject progressDoor;
    Transform [] progressDoorParts;
    public GameObject progressHelicopter;
    Transform [] progressHelicopterParts;
    public GameObject progressCarStatic;
    Transform [] progressCarPartsStatic;
    public GameObject progressDoorStatic;
    Transform [] progressDoorPartsStatic;
    public GameObject progressHelicopterStatic;
    Transform [] progressHelicopterPartsStatic;

    int numberOfPartsCar;
    int numberOfPartsDoor;
    int numberOfPartsHelicopter;

    public GameObject exitButtonCar;
    public GameObject exitButtonDoor;
    public GameObject exitButtonHelicopter;

    public AudioSource audio;
    // Start is called before the first frame update
    void Start() 
    {
        
        progressCarParts = progressCar.GetComponentsInChildren<Transform>(true);
        progressDoorParts = progressDoor.GetComponentsInChildren<Transform>(true);
        progressHelicopterParts = progressHelicopter.GetComponentsInChildren<Transform>(true);
        progressCarPartsStatic = progressCarStatic.GetComponentsInChildren<Transform>(true);
        progressDoorPartsStatic = progressDoorStatic.GetComponentsInChildren<Transform>(true);
        progressHelicopterPartsStatic = progressHelicopterStatic.GetComponentsInChildren<Transform>(true);
        

       
        parts = new List<List<GameObject>>();
        
        parts.Add(carKeys);
        parts.Add(carGas);
        parts.Add(carWirecutters);
        parts.Add(carBatteries);
        parts.Add(helicopterBags);
        parts.Add(helicopterBooks);
        parts.Add(helicopterGears);
        parts.Add(helicopterCrowbars);
        parts.Add(doorKeys);
        parts.Add(doorFakeIds);
        parts.Add(doorMasks);
        parts.Add(doorLuggages);
        
        questions = new List<Question>();
        PlaceParts();
        numberOfPartsCar = 0;
        numberOfPartsDoor= 0;
        numberOfPartsHelicopter = 0;




    }
    public void FoundObject(string objectName,string objectType,Vector3 spawnPosition,GameObject go)
    {
        
        
        switch (objectType)
        {
            case "Car":
                progressCar.gameObject.SetActive(true);
                progressCar.gameObject.transform.position = spawnPosition;
                foreach (Transform part in progressCarParts)
                {
                    if (part.gameObject.name.Equals("Shaded " + SanitazeName(objectName)))
                    {
                        part.gameObject.SetActive(false);
                    }                    
                    if (part.gameObject.name.Equals(SanitazeName(objectName)))
                    {
                        part.gameObject.SetActive(true);

                    }
                }
                foreach (Transform partStatic in progressCarPartsStatic)
                {
                    if (partStatic.gameObject.name.Equals("Shaded " + SanitazeName(objectName)))
                    {
                        partStatic.gameObject.SetActive(false);
                    }                    
                    if (partStatic.gameObject.name.Equals(SanitazeName(objectName)))
                    {
                        partStatic.gameObject.SetActive(true);

                    }
                }
                numberOfPartsCar++;
                CheckForWin();
                break;
            case "Door":
                /*
                GameObject debugCanvas = GameObject.FindGameObjectWithTag("DebugCanvas");
                debugCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "IN";
                */
                progressDoor.gameObject.SetActive(true);
                progressDoor.gameObject.transform.position = spawnPosition;
                foreach (Transform part in progressDoorParts)
                {
                    if (part.gameObject.name.Equals("Shaded " + SanitazeName(objectName)))
                    {
                        part.gameObject.SetActive(false);
                    }
                    if (part.gameObject.name.Equals(SanitazeName(objectName)))
                    {
                        part.gameObject.SetActive(true);

                    }
                }
                foreach (Transform partStatic in progressDoorPartsStatic)
                {
                    if (partStatic.gameObject.name.Equals("Shaded " + SanitazeName(objectName)))
                    {
                        partStatic.gameObject.SetActive(false);
                    }
                    if (partStatic.gameObject.name.Equals(SanitazeName(objectName)))
                    {
                        partStatic.gameObject.SetActive(true);

                    }
                }
                numberOfPartsDoor++;
                CheckForWin();
                break;
            case "Helicopter":
                progressHelicopter.gameObject.SetActive(true);
                progressHelicopter.gameObject.transform.position = spawnPosition;
                foreach (Transform part in progressHelicopterParts)
                {
                    if (part.gameObject.name.Equals("Shaded " + SanitazeName(objectName)))
                    {
                        part.gameObject.SetActive(false);
                    }
                    if (part.gameObject.name.Equals(SanitazeName(objectName)))
                    {
                        part.gameObject.SetActive(true);

                    }
                }
                foreach (Transform partStatic in progressHelicopterPartsStatic)
                {
                    if (partStatic.gameObject.name.Equals("Shaded " + SanitazeName(objectName)))
                    {
                        partStatic.gameObject.SetActive(false);
                    }
                    if (partStatic.gameObject.name.Equals(SanitazeName(objectName)))
                    {
                        partStatic.gameObject.SetActive(true);

                    }
                }
                numberOfPartsHelicopter++;
                CheckForWin();
                break;

        }
        StartCoroutine(PartAdquired());
        Destroy(go);
        
        

    }
    IEnumerator PartAdquired()
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=SampleText&tl=En-gb";
        WWW www = new WWW(url);

        yield return www;

        audio.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        audio.Play();
    }
    public void CheckForWin()
    {
        if(numberOfPartsCar == 4)
        {
            
            exitButtonCar.gameObject.SetActive(true);
            Button b = exitButtonCar.GetComponentInChildren<Button>();
            b.onClick.AddListener(Win);
        }
        if(numberOfPartsDoor == 4)
        {
            exitButtonDoor.gameObject.SetActive(true);
            Button b = exitButtonDoor.GetComponentInChildren<Button>();
            b.onClick.AddListener(Win);
        }
        if(numberOfPartsHelicopter == 4)
        {
            exitButtonHelicopter.gameObject.SetActive(true);
            Button b = exitButtonHelicopter.GetComponentInChildren<Button>();
            b.onClick.AddListener(Win);
        }
    }
    public void Win()
    {
        SceneManager.LoadScene(2);
    }
    public void PlaceParts() 
    {
        int i;
        questions = Questions.getQuestions();
        List<Question> questionsCopy = questions;
        foreach (List<GameObject> list in parts)
        {
            if (list.Count >= 1)
            {
                GameObject g = list[Random.Range(0, list.Count)];
                g.SetActive(true);

                QuestionController qc = g.GetComponent<QuestionController>();
                
                qc.CreateCanvas();
                int pos = Random.Range(0, questionsCopy.Count);
                qc.SetQuestion(questionsCopy[pos]);
                questionsGame.Add(questionsCopy[pos]);
                questionsCopy.RemoveAt(pos);
            }
        }
    }
    public void ResetQuestions() 
    {
        questions = null;
    }
    public void AddQuestion(Question q)
    {
        questions.Add(q);
    }

    public void DeletePart(GameObject go) 
    {
        Destroy(go);
    }
    private string SanitazeName(string objectName)
    {
        var str = objectName;
        str = new string((from c in str
                          where char.IsLetter(c)
                          select c
               ).ToArray());
        Debug.Log(str);
        return str;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}   