using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityStandardAssets.Characters.ThirdPerson;
using TMPro;

public class CounterController : QuestionController
{
    private GameObject crosshair;
    private TextMeshProUGUI secondsLeft;
    private Vector3 position;
    private XRRig player;
    ThirdPersonCharacter enemy;
    public override void QuestionNotPassed()
    {
        DeactivateCanvas();
        
    }
    public override void QuestionPassed()
    {
        DeactivateCanvas();
        if (type.Equals("Vent"))
        {
            XRRig player = GameObject.FindObjectOfType<XRRig>();
            Vent vent = gameObject.GetComponent<Vent>();
            player.transform.position = vent.oppositeVentPosition;
        }
        else
        {
            player = GameObject.FindObjectOfType<XRRig>();
            enemy = GameObject.FindObjectOfType<ThirdPersonCharacter>();
            GameObject locker = GameObject.FindGameObjectWithTag("Locker");
            position = player.transform.position;
            player.transform.position = locker.transform.position;
            player.transform.localScale = new Vector3(0.4f, 0.6f, 0.4f);
            enemy.GetComponent<EnemyController>().SetChasing(false);
            enemy.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            secondsLeft = crosshair.GetComponent<TextMeshProUGUI>();
            StartCoroutine(Timer(10));
        }
       
    }
    public IEnumerator Timer(float duration)
    {
        float startTime = Time.time;
        float time = duration;
        int timeInt;

        // Time.time returns time in seconds from start of game
        while (Time.time - startTime < duration)
        {
            time -= Time.deltaTime;
            timeInt = (int)time;
            secondsLeft.text = "-" + timeInt.ToString() + " s.";
            yield return null;
        }
        ExitLocker();
    }
    private void ExitLocker() 
    {
        player.transform.position = position;
        secondsLeft.text = "";
        player.transform.localScale = new Vector3(1f, 1f, 1f);
        enemy.GetComponent<EnemyController>().SetChasing(true);
        enemy.transform.localScale = new Vector3(1f, 0.9f, 1f);
    }

}
