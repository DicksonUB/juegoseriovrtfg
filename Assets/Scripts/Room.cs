using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Doorway[] doorways;
    public Collider boxCollider;
    public Collider[] compoundCollider;
    public bool compound;
    public Bounds roomBounds;

    public Bounds getRoomBounds()
    {


        return boxCollider.bounds;


    }

}
