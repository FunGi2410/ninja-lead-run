using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleActivator : MonoBehaviour
{
    public event Action OnPlayerDetected;
    void Update()
    {
        if(Bike.Instance.transform.position.z >= transform.position.z)
        {
            OnPlayerDetected?.Invoke();
        }
    }
}
