using UnityEngine;
using UnityEngine.Events;

public class DetectorPenetration : MonoBehaviour
{
    [SerializeField] private UnityEvent _criminalEntered;
    [SerializeField] private UnityEvent _criminalLeaved;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Criminal criminal))
            _criminalEntered.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Criminal criminal))
            _criminalLeaved.Invoke();
    }
}
