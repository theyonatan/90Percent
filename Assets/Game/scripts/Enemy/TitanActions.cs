using UnityEngine;

public class TitanActions : MonoBehaviour
{
    
    
}

public class TitanAttackStrategy : IActionStrategy
{
    private readonly string _animationNameFromMapper;
    
    public bool CanPerform => true;
    public bool Complete { get; private set; }

    private readonly CountdownTimer _timer;
    private readonly GoapAnimator _goapAnimator;
    private readonly GameObject _hitHitbox;
    
    public TitanAttackStrategy(GoapAnimator goapAnimator, GameObject hitHitbox)
    {
        _hitHitbox = hitHitbox;
        _goapAnimator = goapAnimator;
        _animationNameFromMapper = goapAnimator.AnimationMapper.Fight;
    }

    public void Start()
    {
        Complete = false;
        _hitHitbox.SetActive(true);
        
        _goapAnimator.TriggerAnimationUsingTimer(_animationNameFromMapper, "Attack",
            () =>
            {
                Complete = true;
                _hitHitbox.SetActive(false);
            });
    }
}

public class TitanFallDownStrategy : IActionStrategy
{
    public bool CanPerform => true;
    public bool Complete { get; private set; }

    private readonly CountdownTimer _timer;
    private readonly GoapAnimator _goapAnimator;
    
    private readonly TitanBossAgent _bossAgent;
    
    public TitanFallDownStrategy(GoapAnimator goapAnimator, TitanBossAgent agent)
    {
        _goapAnimator = goapAnimator;
        _bossAgent = agent;
    }

    public void Start()
    {
        _bossAgent.DisableGoap();
        Complete = false;
        _bossAgent.KnockedDown = false;
        
        _goapAnimator.TriggerAnimationUsingTimer("falldown","FallDown",
            () =>
            {
                _goapAnimator.TriggerAnimationUsingTimer("wakeup", "Getup", () =>
                {
                    Complete = true;
                    
                    // enable agent if player didn't kill him while lying down
                    if (!_bossAgent.Dead)
                        _bossAgent.EnableGoap();
                });
            });
    }
}
