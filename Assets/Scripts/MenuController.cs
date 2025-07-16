using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _fireball;
    [SerializeField] private Button _whoosh;
    [SerializeField] private Button _water;
    [SerializeField] private Button _mute;
    [SerializeField] private Slider _volume;
    [SerializeField] private Slider _button;
    [SerializeField] private Slider _background;

    public AudioMixerGroup Mixer;
    private bool toggle = true;

    private void OnEnable()
    {
        _fireball.onClick.AddListener(() => PlayAudioFromChildren(_fireball));
        _whoosh.onClick.AddListener(() => PlayAudioFromChildren(_whoosh));
        _water.onClick.AddListener(() => PlayAudioFromChildren(_water));
        _mute.onClick.AddListener(ToggleSounds);

        _volume.onValueChanged.AddListener(ChangeVolume);
        _button.onValueChanged.AddListener(ChangeButtonVolume);
        _background.onValueChanged.AddListener(ChangeBackgroundVolume);
    }

    private void Start()
    {
        _volume.minValue = 0.0001f; // Почти 0, но не 0
        _button.minValue = 0.0001f;
        _background.minValue = 0.0001f;

        _volume.value = 0.5f;
        _button.value = 0.5f;
        _background.value = 0.5f;

        ChangeVolume(_volume.value);
        ChangeButtonVolume(_button.value);
        ChangeBackgroundVolume(_background.value);
    }

    private void OnDisable()
    {
        _fireball.onClick.RemoveAllListeners();
        _whoosh.onClick.RemoveAllListeners();
        _water.onClick.RemoveAllListeners();

        _volume.onValueChanged.RemoveAllListeners();
        _button.onValueChanged.RemoveAllListeners();
        _background.onValueChanged.RemoveAllListeners();
    }

    private void PlayAudioFromChildren(Button _button)
    {
        _button.GetComponentInChildren<AudioSource>().Play();
    }

    public void ToggleSounds()
    {
        toggle = !toggle;
        Mixer.audioMixer.SetFloat("Master", toggle ? 0f : -80f);
    }

    public void ChangeVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20;
        Mixer.audioMixer.SetFloat("Master", dB);
    }

    public void ChangeButtonVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20;
        Mixer.audioMixer.SetFloat("Effects", dB);
    }

    public void ChangeBackgroundVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20;
        Mixer.audioMixer.SetFloat("Background", dB);
    }
}