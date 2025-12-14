using UnityEngine;

public class DiamondVFXKillSelf : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(KillSelf), 10f);
    }

    public void KillSelf() => Destroy(gameObject);
}
