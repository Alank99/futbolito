using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;
using Random = UnityEngine.Random;

public class Comportamiento : Agent
{
    // Instance variables
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _targetTransform2;
    [SerializeField] private Vector3 _initialPosition;

    // Instance "New Position" variable
    private Vector3 _newPosition;

    public void Reward()
    {
        SetReward(2);
    }

    public void Castigo()
    {
        SetReward(-1);
    }

    public void Finish()
    {
        EndEpisode();
    }

    /// <summary>
    /// Called when the agent receives an action to take.
    /// </summary>
    public override void OnEpisodeBegin()
    {   
        // Move the target to a new spot
        transform.localPosition = _initialPosition;
        //transform.localPosition = new Vector3(0,1,8.5f);
    }

    /// <summary>
    /// Called when the agent requests a decision.
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {   
        // Add observations to the sensor
        sensor.AddObservation(transform.localPosition);
        // Add observations to the sensor
        sensor.AddObservation(_targetTransform.localPosition);
        sensor.AddObservation(_targetTransform2.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
            float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 1f;
        transform.position += new Vector3(moveX, 0, moveZ)*Time.deltaTime*moveSpeed;
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("wall"))
        {   
            Debug.LogFormat($"*** Wall hit  ***");
            SetReward(-1);
            // End the episode
            Debug.LogFormat($"*** End episode ***");
            EndEpisode();
            Debug.LogFormat($"*** Episode ended ***");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Debug.LogFormat($"*** Ball hit  ***");
            SetReward(1);
        }
    }
}
