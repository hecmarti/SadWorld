using UnityEngine;
using UnityEngine.Events;

public class UnityEventLaunch : MonoBehaviour
{
    public UnityEvent genericEvent; 

    public void LaunchEvent()
    {
        genericEvent?.Invoke();
    }
}
