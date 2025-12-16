using System;
using UnityEngine;

public class BackLookAtMainCamera : MonoBehaviour
{
    private Transform _mainCameraTransform;

    private void Start()
    {
        DetectMainCamera();
    }

    private void OnEnable()
    {
        DetectMainCamera();
        
        RotateBackwardsMainCamera();
    }

    private void DetectMainCamera()
    {
        if (Camera.main == null) return;
        
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        RotateBackwardsMainCamera();
    }

    private void RotateBackwardsMainCamera()
    {
        if (!_mainCameraTransform) return;

        Vector3 dir = transform.position - _mainCameraTransform.position;
        dir.y = 0f;
        
        if (dir.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }
}
