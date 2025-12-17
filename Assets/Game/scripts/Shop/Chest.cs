using System;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour, RanchInteractable
{
    [SerializeField] private TextMeshPro diamondUICounter;
    [SerializeField] private GameObject interactCanvas;
    
    private int _savedDiamonds;

    private void Start()
    {
        UpdateChestUI();
        StopMarking();
    }

    public void Interact()
    {
        int diamonds = (int)StatsSingleton.Instance.GetStat(StatType.Diamonds).Value;
        
        // deposit diamonds if player got any
        if (diamonds != 0)
        {
            StatsSingleton.Instance.SetStat(StatType.Diamonds, 0f);
            _savedDiamonds += diamonds;
        }
        else // give player if not
        {
            StatsSingleton.Instance.SetStat(StatType.Diamonds, _savedDiamonds);
            _savedDiamonds = 0;
        }
        
        UpdateChestUI();
    }

    public void MarkAsInteractable()
    {
        interactCanvas.SetActive(true);
    }

    public void StopMarking()
    {
        interactCanvas.SetActive(false);
    }

    public void UpdateChestUI()
    {
        diamondUICounter.text = $"{_savedDiamonds}";
    }
}
