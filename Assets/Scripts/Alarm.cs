using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumePercentStep = 0.1f;
    [SerializeField] private float _delayChangeValue = 0.5f;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _targetVolume;
    private WaitForSeconds _audioChangeDelay;
    private Coroutine _coroutineSound;

    private void Awake()
    {
        _audioSource.volume = 0;
        _audioChangeDelay = new WaitForSeconds(_delayChangeValue);
    }

    public void TurnOn()
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();

        SetTargetVolume(_maxVolume);
    }

    public void TurnOff()
    {
        SetTargetVolume(_minVolume);
    }

    private void SetTargetVolume(float targetVolume)
    {
        _targetVolume = targetVolume;

        if (_coroutineSound == null)
            _coroutineSound = StartCoroutine(ChangeSoundVolume());
    }

    private IEnumerator ChangeSoundVolume()
    {
        while (_audioSource.volume != _targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumePercentStep);
            yield return _audioChangeDelay;
        }

        if (_targetVolume == _minVolume)
            _audioSource.Stop();

        _coroutineSound = null;
    }
}
