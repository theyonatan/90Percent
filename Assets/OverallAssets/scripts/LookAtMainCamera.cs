using System;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    private Transform _mainCameraTransform;

    private void Start()
    {
        if (Camera.main == null) return;
        
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (_mainCameraTransform)
            transform.LookAt(_mainCameraTransform);
    }
}
