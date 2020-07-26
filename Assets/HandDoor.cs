using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR.Interaction.Toolkit;

public class HandDoor : XRBaseInteractable
{
    private XRBaseInteractor hoverInteractor = null;
    private Vector3 doorPosition;
    private Vector3 handPosition;
    private float lengthDoor;
    private bool oppened;
    private bool open;
    private bool close;
    Quaternion targetRotation;
    float speed = 5F;
    bool holding;


    public void Awake()
    {
        base.Awake();
        onHoverEnter.AddListener(DoorHovered);
        onHoverExit.AddListener(DoorDehovered);
        onSelectEnter.AddListener(DoorSelected);
        onSelectExit.AddListener(DoorDeSelected);
        oppened = false;
        open = false;
        close = false;
    }
    private void DoorSelected(XRBaseInteractor interactor)
    {
        holding = true;
        if (!oppened)
        {
            open = true;
            close = false;
            targetRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else
        {
            open = false;
            close = true;
            targetRotation = gameObject.transform.parent.rotation;
        }
        oppened = !oppened;
    }
    private void DoorDeSelected(XRBaseInteractor interactor)
    {
        holding = false;
    }
    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(DoorHovered);
        onHoverExit.RemoveListener(DoorDehovered);
    }
    private void DoorHovered(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        doorPosition = gameObject.transform.position;
        lengthDoor = gameObject.GetComponent<BoxCollider>().bounds.size.x;


    }
    private void DoorDehovered(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {

        
            /*
            handPosition = hoverInteractor.transform.position;
            //canvas.GetComponentInChildren<TextMeshProUGUI>().text = handPosition.ToString();
            float opposite = Vector3.Distance(doorPosition, handPosition);
            float division = opposite / hypotenuse;
            canvas.GetComponentInChildren<TextMeshProUGUI>().text = division.ToString();
            float angle = (Mathf.Asin(division) * 180) / Mathf.PI;
            
            Vector3 localRotation = gameObject.transform.eulerAngles;
            Vector3 newRotation = new Vector3(localRotation.x, angle, localRotation.z);
            
            transform.rotation = Quaternion.Euler(newRotation);
                        */
            /*
handPosition = hoverInteractor.transform.position;


    Vector3 directionToHand = handPosition - doorPosition;
    Quaternion targetRotation = Quaternion.LookRotation(directionToHand);
    canvas.GetComponentInChildren<TextMeshProUGUI>().text = targetRotation.ToString();
    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,targetRotation.y, transform.rotation.z));
    */

            if (open)
            {
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, gameObject.transform.parent.rotation * targetRotation, speed * Time.deltaTime);
            }
            else if (close)
            {
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, gameObject.transform.parent.rotation, speed * Time.deltaTime);
            }
        }

    
    private bool inRange()
    {
        return lengthDoor > Vector3.Distance(doorPosition, handPosition);
    }
    public void EnemyInteracts() 
    {
        if (!oppened)
        {
            open = true;
            close = false;
            targetRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            oppened = !oppened;
        }
       
    }


}
    
        
    

/*
 using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR.Interaction.Toolkit;

public class HandDoor : XRBaseInteractable
{
    private UnityEvent OnPush = null;
    private XRBaseInteractor hoverInteractor = null;
    private Vector3 doorPosition;
    private Vector3 handPosition;
    public void Awake()
    {
        base.Awake();
        onHoverEnter.AddListener(StartPush);
        onHoverExit.AddListener(EndPush);
    }
    private void Start()
    {
        
    }
    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(StartPush);
        onHoverExit.RemoveListener(EndPush);
    }
    private void StartPush(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        doorPosition = gameObject.transform.position;
    }

    private void EndPush(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
    }  
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            handPosition = hoverInteractor.transform.position;
            Vector3 localRotation = gameObject.transform.eulerAngles;
            Vector3 newRotation = new Vector3(localRotation.x, handPosition.y, localRotation.z); 
            transform.rotation = Quaternion.Euler(newRotation);
            
        }
    }
    private void checkPush()
    {
        //bool inPostion = inPosition();
        OnPush.Invoke();
    }
  
}
*/
