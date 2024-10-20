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
            _comportamiento.Reward(+2);
            Debug.LogFormat("Metí gol");

        }

        if (other.CompareTag("porteria2"))
        {
            _comportamiento.Castigo(-3);
            Debug.LogFormat("Me metieron gol");
        }
    }
}
