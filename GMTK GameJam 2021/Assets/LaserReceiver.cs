using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour
{
    public UnityEvent<bool, GameObject> OnStateChange = new UnityEvent<bool, GameObject>();
    bool isBeingHitByLaser;
    bool wasBeingHitByLaser;

    public void OnHitByLaser()
    {
        isBeingHitByLaser = true;
    }

    void Update()
    {
        if(isBeingHitByLaser != wasBeingHitByLaser)
        {
            OnStateChange.Invoke(isBeingHitByLaser, gameObject);
        }
        wasBeingHitByLaser = isBeingHitByLaser;
        isBeingHitByLaser = false;
    }
}
