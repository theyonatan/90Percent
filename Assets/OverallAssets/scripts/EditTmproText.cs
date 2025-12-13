using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EditTmproText : MonoBehaviour
{
    private TextMeshProUGUI _uiText;
    
    void Start()
    {
        _uiText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string text)
    {
        _uiText.text = text;
    }
}
