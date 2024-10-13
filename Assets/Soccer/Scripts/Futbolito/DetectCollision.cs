using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] private Comportamiento _comportamiento;
    [SerializeField] private Comportamiento _comportamiento2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Porteria1"))
        {
            _comportamiento.Reward();
            _comportamiento2.Castigo();
            _comportamiento.Finish();
            _comportamiento2.Finish();
        }

        if (other.CompareTag("Porteria2"))
        {
            _comportamiento2.Reward();
            _comportamiento.Castigo();
            _comportamiento.Finish();
            _comportamiento2.Finish();
        }
    }
}
