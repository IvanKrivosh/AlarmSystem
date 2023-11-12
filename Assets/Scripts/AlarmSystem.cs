using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumePercentStep = 0.1f;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _delayChangeValue = 0.5f;    
    private float _cureentPercentValume;
    private IEnumerator _turnOnCoroutine;
    private IEnumerator _turnOffCoroutine;

    private void Awake()
    {
        _audioSource.volume = 0;
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Criminal criminal))
        {
            _turnOnCoroutine = TurnOnAlarm();
            StartCoroutine(_turnOnCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Criminal criminal))
        {
            _turnOffCoroutine = TurnOffAlarm();
            StartCoroutine(_turnOffCoroutine);
        }
    }

    private IEnumerator TurnOffAlarm()
    {
        if (_turnOnCoroutine != null)
            StopCoroutine(_turnOnCoroutine);

        while (_audioSource.volume != _minVolume)
        {            
            ChangeValume(false);
            yield return new WaitForSeconds(_delayChangeValue); ;
        }

        _audioSource.Stop();
    }

    private IEnumerator TurnOnAlarm()
    {
        if (_turnOffCoroutine != null)
            StopCoroutine(_turnOffCoroutine);

        _audioSource.Play();

        while (_audioSource.volume != _maxVolume)
        {            
            ChangeValume();
            yield return new WaitForSeconds(_delayChangeValue);
        }
    }

    private void ChangeValume(bool increment = true)
    {
        _cureentPercentValume += _volumePercentStep * (increment ? 1 : -1);
        _audioSource.volume = Mathf.MoveTowards(_minVolume, _maxVolume, _cureentPercentValume);
    }
}
