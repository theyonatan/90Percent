using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]  // Allows running in Editor
public class ButtonSyncText : Button
{
    [Header("Child References (optional)")]
    public TextMeshProUGUI textChild;
    public Image imageChild;

    [Header("Disabled Appearance")]
    public Color disabledTextColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public Color disabledImageColor = new Color(1f, 1f, 1f, 0.2f);

    [SerializeField] [HideInInspector] private Color originalTextColor;
    [SerializeField] [HideInInspector] private Color originalImageColor;

    private void Reset()
    {
        CaptureOriginals();
    }

    private void OnValidate()
    {
        CaptureOriginals();
    }

    private void CaptureOriginals()
    {
        if (textChild == null) textChild = GetComponentInChildren<TextMeshProUGUI>();
        if (imageChild == null)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Image img) && img != targetGraphic)
                {
                    imageChild = img;
                    break;
                }
            }
        }

        if (textChild != null) originalTextColor = textChild.color;
        if (imageChild != null) originalImageColor = imageChild.color;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Auto-find if still null (for prefabs/instantiated)
        if (textChild == null) textChild = GetComponentInChildren<TextMeshProUGUI>();
        if (imageChild == null)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Image img) && img != targetGraphic)
                {
                    imageChild = img;
                    break;
                }
            }
        }

        // Fallback: capture if originals are black/zero (safety)
        if (originalTextColor == Color.clear || originalTextColor == Color.black)
        {
            if (textChild != null) originalTextColor = textChild.color;
        }
        if (originalImageColor == Color.clear || originalImageColor == Color.black)
        {
            if (imageChild != null) originalImageColor = imageChild.color;
        }

        UpdateVisuals(currentSelectionState);
    }

    private void UpdateVisuals(SelectionState state)
    {
        ColorBlock cb = colors;
        Color tint = Color.white;

        switch (state)
        {
            case SelectionState.Normal:
                tint = cb.normalColor;
                break;
            case SelectionState.Highlighted:
                tint = cb.highlightedColor;
                break;
            case SelectionState.Pressed:
                tint = cb.pressedColor;
                break;
            case SelectionState.Disabled:
                ApplyDisabledColors();
                return;
        }

        ApplyTintedColors(tint);
    }

    private void ApplyTintedColors(Color tint)
    {
        if (textChild != null)
            textChild.color = originalTextColor * tint;

        if (imageChild != null)
            imageChild.color = originalImageColor * tint;
    }

    private void ApplyDisabledColors()
    {
        if (textChild != null)
            textChild.color = disabledTextColor;

        if (imageChild != null)
            imageChild.color = disabledImageColor;
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        UpdateVisuals(state);
    }
}