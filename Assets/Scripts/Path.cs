using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private List<Transform> _checkpoints = new List<Transform>();
    private int _currentIndex = 0;
    
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            _checkpoints.Add(transform.GetChild(i));
    }

    public Transform GetNextPoint()
    {
        if (_currentIndex == _checkpoints.Count)
            _currentIndex = 0;

        return _checkpoints[_currentIndex++];
    }   
}
