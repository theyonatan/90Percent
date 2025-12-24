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
