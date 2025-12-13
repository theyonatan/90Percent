using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkInteractUI : MonoBehaviour
{
    [SerializeField] public Dictionary<string, Sprite> _icons;
    private Image _image;
    
    void Start()
    {
        _image = GetComponent<Image>();
        DisableImage();
    }

    public void EnableImage(string icon)
    {
        _image.sprite = _icons[icon];
        _image.gameObject.SetActive(true);
    }

    public void DisableImage()
    {
        _image.gameObject.SetActive(false);
    }
}
