using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DoorInteractable : MonoBehaviour,IInteractable
{
    [SerializeField] [Range(0f, 5f)] float speed;
    [SerializeField] Vector3 angle;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private bool oppened;
    private bool open;
    private bool close;
    

    private const float maxRange = 8f;
    public void start()
    {
        oppened = false;
        open = false;
        close = false;
    }
    public float MaxRange { get { return maxRange; } }
    public void OnStartHover()
    {
        tooltipText.gameObject.SetActive(true);
        tooltipText.SetText("Press E to interact");

    }
    
    void Update()
    {
        if (open) 
        {
            
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(angle), speed * Time.deltaTime);
            
        }
        else if(close)
        {
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, speed * Time.deltaTime);
            
        }
    }
    public void OnInteract()
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
   
    public void OnEndHover()
    {
        tooltipText.gameObject.SetActive(false);
    }

    
 
       
    
}
