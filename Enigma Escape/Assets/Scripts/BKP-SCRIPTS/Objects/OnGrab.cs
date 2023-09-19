using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrab : MonoBehaviour, IInteractable
{
    ToolbeltV2 _toolBeltV2;
    private void Start()
    {
        var player = GameObject.Find("Player");
        _toolBeltV2 = player.GetComponent<ToolbeltV2>();
    }
    public void Interact(RaycastHit hit)
    {
        _toolBeltV2.ItemCollector(hit);
    }
}
