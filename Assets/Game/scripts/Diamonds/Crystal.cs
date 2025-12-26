using UnityEngine;

public class Crystal : MonoBehaviour, Interactable
{
    private static readonly int Knockdown = Animator.StringToHash("Knockdown");
    [SerializeField] private int maxHealth;
    private int _currentHealth;

    [SerializeField] private GameObject crystalVfx;
    private MarkInteractUI _markInteractUI;
    private DiamondHealthBar _healthBarBar;
    
    private void Awake()
    {
        _markInteractUI = FindFirstObjectByType<MarkInteractUI>();
        _healthBarBar = GetComponentInChildren<DiamondHealthBar>();
        
        _currentHealth = maxHealth;
    }

    public void MarkAsInteractable()
    {
        _markInteractUI.EnableImage("dig");
        _healthBarBar.EnableHealthBar();
    }
    
    public void StopMarking()
    {
        _markInteractUI.DisableImage();
        _healthBarBar.DisableHealthBar();
    }
    
    public void Interact()
    {
        Debug.Log($"Interacting with Crystal");
        
        DamageCrystal();
    }

    private void DamageCrystal()
    {
        const int damage = 1;
        
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(0, _currentHealth);
        
        UpdateHealthUI();
        
        if (_currentHealth <= 0)
            Invoke(nameof(BreakCrystal), 0.2f);
    }

    private void UpdateHealthUI()
    {
        float normalized = (float)_currentHealth / maxHealth;
        _healthBarBar.SetHealth(normalized);
    }

    private void BreakCrystal()
    {
        // make all titans fall
        FallAllTitans();
        
        // crystal vfx
        if (crystalVfx != null)
        {
            Instantiate(crystalVfx, transform.position, transform.rotation);
            Invoke(nameof(DestroyVfx), 1f);
        }
        Destroy(gameObject);
    }

    private void FallAllTitans()
    {
        foreach (var titan in FindObjectsByType<TitanBossAgent>(FindObjectsSortMode.InstanceID))
        {
            titan.KnockedDown = true;
            titan.ResetActionAndGoal();
        }
    }

    private void DestroyVfx()
    {
        Destroy(crystalVfx);
    }
}
