using UnityEngine;
using UnityEngine.AI;

public class KillTitan : MonoBehaviour
{
    private static readonly int Dead = Animator.StringToHash("Dead");
    private bool _dead;
    [SerializeField] private bool storyTitan;
    
    public void KillTheTitan()
    {
        if (_dead)
            return;
        _dead = true;
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        
        IGoapAgent agentGoap = GetComponent<IGoapAgent>();
        agentGoap.StopGoap();
        
        Animator animator = GetComponent<Animator>();
        
        GetComponent<TitanBossAgent>().Dead = true;
        
        if (storyTitan)
        {
            animator.SetBool(Dead, true);
        }
        else
        {
            animator.enabled = false;
            
            foreach (var rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
}
