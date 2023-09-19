using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrabMate : OnGrab,IInteractable
{
    ToolbeltV2 _toolBeltV2;
    private void Start()
    {
        var player = GameObject.Find("Player");
        _toolBeltV2 = player.GetComponent<ToolbeltV2>();
    }
    public new void Interact(RaycastHit hit)
    {
        _toolBeltV2.ItemCollector(hit);
        Debu();
    }

    private void Debu()
    {
        Debug.Log("funciona el GRAB");
    }
}
