using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Comportamiento : Agent
{
    // Instance variables
    //[SerializeField] private GameObject ball;
    // Target transform
    [SerializeField] private Transform _targetTransform; // Ball transform
    [SerializeField] private Transform _targetTransform2; // Porteria transform
    // [SerializeField] private Vector3 initialPosition = new Vector3(0,0.95f,-0.33f);

    public void Reward(int reward)
    {

        SetReward(reward);
        EndEpisode();
    }

    public void Castigo(int castigo)
    {
        SetReward(castigo);
        EndEpisode();
    }

    /// <summary>
    /// Called when the agent receives an action to take.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        // La posición inicial del agente se mantiene igual
        // transform.localPosition = initialPosition;

        // Llama a MoveToRandomPosition() para mover la pelota a partir de su posición inicial
    //     _targetTransform.localPosition = MoveToRandomPosition();
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

        float _moveSpeed = 1f;
        transform.localPosition += new Vector3(_moveX, 0, _moveZ) * Time.deltaTime * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {

        // if (other.CompareTag("wall"))
        // {
        //     Castigo(-3);
        // }
    }


    public void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("ball"))
        // {
        //     Reward(+2);
        // }
        // else Castigo(-3);
    }


    /// <summary>
    /// Method to move the ball to a random position in the environment.
    /// </summary>
    // public Vector3 MoveToRandomPosition()
    // {
    //     // Posición inicial de la pelota
    //     Vector3 initialPos = new Vector3(-2f, 1.1f, 0f);
    //     // Desplazamiento aleatorio a partir de la posición inicial
    //     float num = 0f;
    //     float _randPosX = Random.Range(-7, num);
    //     float _randPosZ = Random.Range(-num, num);
    //     // Suma el desplazamiento a la posición inicial
    //     return initialPos + new Vector3(_randPosX, 0, _randPosZ);
    // }
}
