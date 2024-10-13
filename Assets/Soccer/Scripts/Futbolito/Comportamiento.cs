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
    //[SerializeField] private Transform _initPos;

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
        transform.localPosition = transform.localPosition;
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
        // Get the action
        float _moveX = actions.ContinuousActions[0];
        float _moveZ = actions.ContinuousActions[1];

        float _moveSpeed = 5f;
        transform.localPosition += new Vector3(_moveX, 0, _moveZ)*Time.deltaTime*_moveSpeed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {   
            //Debug.LogWarningFormat($"*** Goal reached | Total points: {totalReward} ***");
            // Add a reward
            SetReward(+1);
            Vector3 _newPosition = new Vector3(Random.Range(-10f, 10f), _targetTransform.localPosition.y, Random.Range(-10f, 10f));
            _targetTransform.localPosition = _newPosition;
        }

        if (other.CompareTag("wall"))
        {   
            //Debug.LogFormat($"*** Wall hit | Total points:  ***");
            // Add a reward
            SetReward(-1);
            EndEpisode();
        }
    }
}
