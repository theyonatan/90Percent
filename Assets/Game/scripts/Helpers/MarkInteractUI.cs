using UnityEngine;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;

public class MarkInteractUI : MonoBehaviour
{
    [SerializedDictionary] public SerializedDictionary<string, Sprite> Icons;
    private Image _image;
    
    void Start()
    {
        _image = GetComponent<Image>();
        DisableImage();
    }

    public void EnableImage(string icon)
    {
        _image.sprite = Icons[icon];
        _image.gameObject.SetActive(true);
    }

    public void DisableImage()
    {
        _image.gameObject.SetActive(false);
    }
}
