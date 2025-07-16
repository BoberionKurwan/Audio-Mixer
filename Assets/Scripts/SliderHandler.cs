using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHandler : MonoBehaviour
{
    private const float MinValue = 0.0001f;
    private const float InitialValue = 0.8f;
    private const int StandartMultiplier = 20;

    [SerializeField] private AudioMixerGroup _mixerGroup;
    
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.minValue = MinValue;
        _slider.value = InitialValue;
    }

    private void OnEnable()
    {
        _slider?.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _slider?.onValueChanged.RemoveListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * StandartMultiplier;
        _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, dB);
    }
}