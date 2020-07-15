using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Doorway[] doorways;
    public Collider boxCollider;
    public Collider[] compoundCollider;
    
    public Bounds[] getRoomBounds()
    {

        if (compoundCollider.Length > 0)
        {
            Bounds[] bounds = new Bounds[compoundCollider.Length];
            for(int i = 0; i < compoundCollider.Length; i++)
            {
                bounds[i] = compoundCollider[i].bounds;
            }
            return bounds;
            
        }
        Bounds[] oneBound = new Bounds[1];
        oneBound[0] = boxCollider.bounds;
        return oneBound;
    }
    
    public Collider[] GetRoomColliders()
    {

        if (compoundCollider.Length > 0)
        {
            
            return compoundCollider;

        }
        Collider[] oneCollider = new Collider[1];
        oneCollider[0] = boxCollider;
        return oneCollider;
    }
}
