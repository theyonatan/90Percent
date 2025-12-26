using UnityEngine;
using DG.Tweening;

public class OpenGate : MonoBehaviour
{
    [SerializeField] private float duration = 2f;  // Animation time in seconds
    [SerializeField] private Ease easeType = Ease.OutQuad;  // Smooth easing

    public void Open()
    {
        transform.DOLocalMoveY(5f, duration)
            .SetRelative(true)
            .SetEase(easeType);
    }
}
