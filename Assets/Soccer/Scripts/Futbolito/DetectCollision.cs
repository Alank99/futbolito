using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] private Comportamiento _comportamiento;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("porteria1"))
        {
            _comportamiento.Reward(+3);

        }

        if (other.CompareTag("porteria2"))
        {
            _comportamiento.Castigo(-2);

        }
    }
}
