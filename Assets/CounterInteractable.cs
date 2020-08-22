using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR.Interaction.Toolkit;

public class CounterInteractable : XRBaseInteractable
{
    private XRBaseInteractor hoverInteractor = null;
    CounterController cc;



    public void Awake()
    {
        base.Awake();
        onHoverEnter.AddListener(ObjectHovered);
        onHoverExit.AddListener(ObjectDeHovered);
        onSelectEnter.AddListener(ObjectSelected);
        onSelectExit.AddListener(ObjectDeselected);
        cc = GetComponent<CounterController>();
    }
    private void ObjectSelected(XRBaseInteractor interactor)
    {
        cc.DisplayCanvas(new Vector3(transform.position.x, 2.5f, transform.position.z));
    }
    private void ObjectDeselected(XRBaseInteractor interactor)
    {
        //Do nothing
    }
    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(ObjectHovered);
        onHoverExit.RemoveListener(ObjectDeHovered);
        onSelectEnter.RemoveListener(ObjectSelected);
        onSelectExit.RemoveListener(ObjectDeselected);
    }
    private void ObjectHovered(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
    }
    private void ObjectDeHovered(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {


    }


    private bool inRange()
    {
        // Maybe?
        return true;
    }
}
