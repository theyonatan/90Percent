using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiamondHealthBar : MonoBehaviour
{
    private Slider _healthBar;
    
    private Camera _camera;

    private void Start()
    {
        _healthBar = GetComponent<Slider>();
        
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("No player found");
            _camera = Camera.main;
            return;
        }
        
        _camera = player.GetCamera();
        DisableHealthBar();
    }

    private void LateUpdate()
    {
        if (!_camera)
            return;
        
        transform.parent.forward = transform.parent.position - _camera.transform.position;
    }

    public void SetHealth(float health)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateHealth(health));
    }

    private IEnumerator AnimateHealth(float targetHealth)
    {
        float start = _healthBar.value;
        float time = 0f;
        float duration = 0.2f;

        while (time < 1f)
        {
            time += Time.deltaTime / duration;
            _healthBar.value = Mathf.Lerp(start, targetHealth, time);
            yield return null;
        }
        
        _healthBar.value = targetHealth;
    }

    public void EnableHealthBar()
    {
        _healthBar.gameObject.SetActive(true);
    }

    public void DisableHealthBar()
    {
        if (!_healthBar)
            return;
        
        _healthBar.gameObject.SetActive(false);
    }
}
