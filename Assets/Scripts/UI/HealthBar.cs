using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        EventBus.hpChanged += OnHpChanged;
    }

    private void OnDisable()
    {
        EventBus.hpChanged += OnHpChanged;
    }

    private void OnHpChanged(float val)
    {
        _slider.value = val;
    }
}
