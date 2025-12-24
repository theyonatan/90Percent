using UnityEngine;

public class TitanHitbox : MonoBehaviour
{
    [SerializeField] private float horizontalForce;
    [SerializeField] private float verticalForce;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (other.TryGetComponent(out cc_ExtensionKnockback knockback))
            knockback.ApplyKnockback(transform.parent.position, horizontalForce, verticalForce);
    }
}
