using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    private const float DelayChangeValue = 0.5f;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumePercentStep = 0.1f;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;       
    private float _cureentPercentValume;
    private IEnumerator _turnOnCoroutine;
    private IEnumerator _turnOffCoroutine;    
    private WaitForSeconds _audioChangeDelay = new WaitForSeconds(DelayChangeValue);

    private void Awake()
    {
        _audioSource.volume = 0;
    }

    public void TurnOn()
    {
        _turnOnCoroutine = TurnOnSound();
        StartCoroutine(_turnOnCoroutine);
    }

    public void TurnOff()
    {
        _turnOffCoroutine = TurnOffSound();
        StartCoroutine(_turnOffCoroutine);
    }

    private IEnumerator TurnOnSound()
    {
        if (_turnOffCoroutine != null)
            StopCoroutine(_turnOffCoroutine);

        _audioSource.Play();

        while (_audioSource.volume != _maxVolume)
        {
            ChangeValume();
            yield return _audioChangeDelay;
        }
    }

    private IEnumerator TurnOffSound()
    {
        if (_turnOnCoroutine != null)
            StopCoroutine(_turnOnCoroutine);

        while (_audioSource.volume != _minVolume)
        {            
            ChangeValume(false);
            yield return _audioChangeDelay;
        }

        _audioSource.Stop();
    }    

    private void ChangeValume(bool increment = true)
    {
        _cureentPercentValume += _volumePercentStep * (increment ? 1 : -1);
        _audioSource.volume = Mathf.MoveTowards(_minVolume, _maxVolume, _cureentPercentValume);
    }
}
