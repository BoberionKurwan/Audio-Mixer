using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class ButtonHandler : MonoBehaviour
{
    private Button _button;
    private AudioSource _audioSource;

    private void Start()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();

        _button.onClick.AddListener(PlayAudio);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(PlayAudio);
    }

    private void PlayAudio()
    {
        _audioSource.Play();
    }
}