using System.Globalization;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EditTmproText : MonoBehaviour
{
    private TextMeshProUGUI _uiText;
    
    void Start()
    {
        _uiText = GetComponent<TextMeshProUGUI>();
        _uiText.text = "0";

        StatsSingleton.Instance.GetStat(StatType.Diamonds).OnStatChanged += UpdateText;
    }

    private void UpdateText(float newValue)
    {
        _uiText.text = newValue.ToString(CultureInfo.CurrentCulture);
    }
}
