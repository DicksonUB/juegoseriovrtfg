using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderDoor : MonoBehaviour
{
    public HandDoor doorController;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        doorController.EnemyInteracts();
    }
}
