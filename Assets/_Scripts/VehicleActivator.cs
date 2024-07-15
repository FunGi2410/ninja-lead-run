using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleActivator : MonoBehaviour
{
    public event Action OnPlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bike"))
        {
            OnPlayerDetected?.Invoke();
        }
    }
}
