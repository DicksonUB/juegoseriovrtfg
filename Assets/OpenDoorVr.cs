using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class OpenDoorVr : XRGrabInteractable
{
    private float speed;
    private Vector3 angle;
    private bool oppened;
    private bool open;
    private bool close;
    private bool done;
    private double epsilon = 0.5f;
    private XRBaseInteractable interactable = null;
    // Start is called before the first frame update
    void Start()
    {
        oppened = false;
        open = false;
        close = false;
        interactable = GetComponent<XRBaseInteractable>();
        interactable.onActivate.AddListener(Interacted);
        interactable.onDeactivate.AddListener(Interacted);
        angle = new Vector3(0, 90, 0);
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (open)
        {
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, gameObject.transform.parent.rotation * Quaternion.Euler(angle), speed * Time.deltaTime);
        }
        else if (close)
        {
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, gameObject.transform.parent.rotation, speed * Time.deltaTime);
        }
    }
    private void Interacted(XRBaseInteractor interactor)
    {

        if (!oppened)
        {
            open = true;
            close = false;

            
        }
        else
        {
            open = false;
            close = true;
            
        }
        oppened = !oppened;
    }
    

}