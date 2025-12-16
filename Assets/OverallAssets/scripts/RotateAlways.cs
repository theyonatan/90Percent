using UnityEngine;

public class RotateAlways : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 25f;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
