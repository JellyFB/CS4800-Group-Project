using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private float _currentTime;
    public bool isActive = false;

    private void Update()
    {
        // Progress through the time
        if (isActive)
        {
            _currentTime += Time.deltaTime;
        }
    }

    public float GetTime()
    {
        return _currentTime;
    }

    public void Reset()
    {
        _currentTime = 0;
    }

    public void SetTime(float loadedTime)
    {
        _currentTime = loadedTime;
    }
}
