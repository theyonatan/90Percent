using System;
using UnityEngine;

public class ExtensionDiamondInteract : ExtensionInteractier
{
    private void Awake()
    {
        InteractableType = typeof(Diamond);
    }

    private void Start()
    {
        base.Start();
        UnsubscribeFromDefaultInteract();
        _inputDirector.OnFirePressed += OnPressedInteract;
    }

    private void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        _inputDirector.OnFirePressed -= OnPressedInteract;
    }
}
