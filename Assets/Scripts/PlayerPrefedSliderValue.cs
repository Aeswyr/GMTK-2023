using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerPrefedSliderValue : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private float _defaultValue;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        float value = _defaultValue;
        if (PlayerPrefs.HasKey(_key))
        {
            value = PlayerPrefs.GetFloat(_key);
        }
        _slider.value = value;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat(_key, value);
    }
}
