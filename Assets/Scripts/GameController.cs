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
    public List<GameObject> lockers;
    public List<GameObject> vents;

    public GameObject progressCarStatic;
    Transform [] progressCarPartsStatic;
    public GameObject progressDoorStatic;
    Transform [] progressDoorPartsStatic;
    public GameObject progressHelicopterStatic;
    Transform [] progressHelicopterPartsStatic;

    int numberOfPartsCar;
    int numberOfPartsDoor;
    int numberOfPartsHelicopter;

    public GameObject player;
    public GameObject exitButtonCar;
    public GameObject exitButtonDoor;
    public GameObject exitButtonHelicopter;
    public AudioSource music;
    public AudioSource audio;


    // Start is called before the first frame update
    void Start() 
    {
        
        
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
        DeepCopyList(questions);
        
        PlaceParts();
        numberOfPartsCar = 0;
        numberOfPartsDoor= 0;
        numberOfPartsHelicopter = 0;
        



    }
    private void DeepCopyList(List<Question> l)
    {
        foreach (Question q in Questions.getQuestions())
        {
            if (q.option2 == null)
            {
                l.Add(new Question(q.option0, q.option1, q.answer, q.inquiry));
            }
            else if(q.option3 == null)
            {
                l.Add(new Question(q.option0, q.option1, q.option2, q.answer, q.inquiry));
            }
            else
            {
                l.Add(new Question(q.option0, q.option1, q.option2, q.option3, q.answer, q.inquiry));
            }   
        }
    }
    public void FoundObject(string objectName,string objectType,Vector3 spawnPosition,GameObject go)
    {

        
        switch (objectType)
        {
            case "Car":
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
        StartCoroutine(WaitAndMove(1, go,go.transform.position, player.transform.position));
        StartCoroutine(PartAdquired());
        
    }
    
    
    IEnumerator WaitAndMove(float delayTime,GameObject go,Vector3 posA,Vector3 posB)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= 2)
        { // until one second passed
            go.transform.position = Vector3.Lerp(posA, posB, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
        Destroy(go);
    }
    IEnumerator PartAdquired()
    {
        
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=New%20part&tl=En-gb";
        WWW www = new WWW(url);

        yield return www;

        audio.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        music.Pause();
        audio.Play();
        music.Play();
    }
    IEnumerator PartLost()
    {

        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=Part%20lost&tl=En-gb";
        WWW www = new WWW(url);

        yield return www;

        audio.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        music.Pause();
        audio.Play();
        music.Play();
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
        SceneManager.LoadScene("EndScreen");
    }
    public void PlaceParts() 
    {
        int i; 
      
        foreach (List<GameObject> list in parts)
        {
            if (list.Count >= 1)
            {
                GameObject g = list[Random.Range(0, list.Count-1)];
                g.SetActive(true);
                PartController pc = g.GetComponent<PartController>();                
                pc.CreateCanvas();
                int pos = Random.Range(0, questions.Count-1);
                pc.SetQuestion(questions[pos]);
                questions.RemoveAt(pos);
            }
        }
        PlaceCounters();
    }
    
    public void PlaceCounters()
    {
        /*
        List<GameObject>lockersCopy = lockers;
        List<GameObject>ventsCopy = vents;
        */
        int pos;

        // We reset to all the questions again
        //CopyList(questionsCopy, questions);
        
        List<GameObject> ventsActive = new List<GameObject>();
        for (int i = 0; i < 6; i++)
        {
            
            if (questions.Count > 0) 
            {
                if(lockers.Count > 2)
                {
                    pos = Random.Range(0, lockers.Count - 1);
                    GameObject g = lockers[pos];
                    g.SetActive(true);
                    lockers.RemoveAt(pos);

                    CounterController cc = g.GetComponent<CounterController>();
                    cc.CreateCanvas();
                    pos = Random.Range(0, questions.Count - 1);
                    cc.SetQuestion(questions[pos]);
                    questions.RemoveAt(pos);
                }
            }
           
            if (questions.Count > 0)
            {             
                pos = Random.Range(0, questions.Count - 1);
                Question q = questions[pos];
                questions.RemoveAt(pos);
                
                for (int j = 0; j < 2; j++)
                {
                    if (vents.Count > 0)
                    {
                        pos = Random.Range(0, vents.Count - 1);

                        GameObject g = vents[pos];
                        g.SetActive(true);
                        vents.RemoveAt(pos);

                        CounterController cc = g.GetComponent<CounterController>();
                        cc.CreateCanvas();
                        cc.SetQuestion(q);
                        ventsActive.Add(g);
                    }
                }
                
            }
            
        }
       
        for (int i = 0; i < (ventsActive.Count/2); i++)
        {
            
            GameObject g1=ventsActive[2 * i];
            GameObject g2=ventsActive[2 * i+1];
            
            g1.GetComponent<Vent>().SetOppositeVent(g2.transform.position - new Vector3(1,0,0));
            g2.GetComponent<Vent>().SetOppositeVent(g1.transform.position - new Vector3(1,0,0));
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
        StartCoroutine(PartLost());
    }
    private string SanitazeName(string objectName)
    {
        var str = objectName;
        str = new string((from c in str
                          where char.IsLetter(c)
                          select c
               ).ToArray());
        
        return str;
    }

}   