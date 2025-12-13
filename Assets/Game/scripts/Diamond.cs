using System;
using UnityEngine;

public class Diamond : MonoBehaviour, Interactable
{
    private DiamondManager _diamondManager;
    private MarkInteractUI _markInteractUI;

    private void Awake()
    {
        _diamondManager = DiamondManager.Instance;
        _markInteractUI = FindFirstObjectByType<MarkInteractUI>();
    }

    public void MarkAsInteractable()
    {
        _markInteractUI.EnableImage("dig");
    }

    public void Interact()
    {
        Debug.Log($"Interacting with Diamond");
        
        _diamondManager.CollectDiamond();
    }

    public void StopMarking()
    {
        _markInteractUI.DisableImage();
    }
}
