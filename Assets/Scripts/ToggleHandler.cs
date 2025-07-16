using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleHandler : MonoBehaviour
{
    private const float MinSoundValue = -80f;

    [SerializeField] private AudioMixerGroup _mixerGroup;

    private Toggle _toggle;
    private float _savedVolume;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        _toggle.isOn = false;
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ToggleVolume);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ToggleVolume);
    }

    private void ToggleVolume(bool isMuted)
    {
        if (isMuted)
        {
            _mixerGroup.audioMixer.GetFloat(_mixerGroup.name, out _savedVolume);
            _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, MinSoundValue);
        }
        else
        {
            _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, _savedVolume);
        }

    }
}
