using UnityEngine;

public class Diamond : MonoBehaviour, Interactable
{
    public DiamondType DiamondType;
    [SerializeField] private int maxHealth;
    private int _currentHealth;

    [SerializeField] private GameObject diamondVfx;
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
        Debug.Log($"Interacting with Diamond");
        
        DamageDiamond();
    }

    private void DamageDiamond()
    {
        int damage = PlayerPickaxe.Instance.GetMiningStrength(DiamondType);
        if (damage <= 0)
            return;
        
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(0, _currentHealth);
        
        UpdateHealthUI();
        
        if (_currentHealth <= 0)
            Invoke(nameof(BreakDiamond), 0.2f);
    }

    private void UpdateHealthUI()
    {
        float normalized = (float)_currentHealth / maxHealth;
        _healthBarBar.SetHealth(normalized);
    }

    private void BreakDiamond()
    {
        if (diamondVfx != null)
        {
            Instantiate(diamondVfx, transform.position, transform.rotation);
            Invoke(nameof(DestroyVfx), 1f);
        }
        PlayerPickaxe.CollectDiamond(DiamondType);
        Destroy(gameObject);
        
        // titan death
        if (transform.parent.TryGetComponent(out KillTitan killTitan))
            killTitan.KillTheTitan();
    }

    private void DestroyVfx()
    {
        Destroy(diamondVfx);
    }
}