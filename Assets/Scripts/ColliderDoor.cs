using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDoor : MonoBehaviour
{
    public DoorInteractable doorController;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        doorController.EnemyInteracts();
    }
}
