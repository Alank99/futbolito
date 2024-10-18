using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Comportamiento : Agent
{
    // Instance variables
    [SerializeField] private GameObject ball;
    // Target transform
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _targetTransform2;
    [SerializeField] private Vector3 _initialPosition;
    // Instance "New Position" variable
    private Vector3 _newPosition;
    // Count of "Wall hit" log
    //private static int _wallHitCount = 0;
    // Count of "Ball hit" log
    //private static int _ballHitCount = 0;
    // Instance "Is Ball Near" variable to detect the ball
    //private bool _isBallNear;

    // Max velocity on X axis (Agent)
    private float _maxSpeedX = 1f;
    // Max velocity on Z axis (Agent)
    private float _maxSpeedZ = 1f;
    // Velocity of Ball movement
    public float _ballSpeed = -0.1f;
    private bool _dir;

    private void Update()
    {
        // Move the ball automatically
        float direction = _dir ? 1 : -1;
        ball.transform.position += new Vector3(_ballSpeed * direction, 0, 0) * Time.deltaTime;

        // Set the max movement position of the ball to avoid the ball to go out of the field
        if (ball.transform.position.x >= -11.5f) _dir = false;
        if (ball.transform.position.x <= 11.5f) _dir = true;

    }

    public void Reward(int reward)
    {
        Debug.LogFormat("Metí gol");
        SetReward(reward);
        EndEpisode();
    }

    public void Castigo(int castigo)
    {
        Debug.LogFormat("Me metieron gol");
        SetReward(castigo);
        EndEpisode();
    }

    /// <summary>
    /// Called when the agent receives an action to take.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        // Instance the agent in a static position in the environment
        ball.transform.localPosition = new Vector3(-11.5f, 1.08000004f, 0.230000004f);
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
        //float moveSpeed = 1f;
        //transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
        // Dinamic movement based on velovity variables
        transform.position += new Vector3(_maxSpeedX, 0, _maxSpeedZ) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("wall"))
        {

            //Debug.LogErrorFormat($"*** Wall hits: {_wallHitCount}  ***");
            Castigo(-3);
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            //Debug.LogFormat($"*** Ball hits: {_ballHitCount}  ***");
            Reward(1);
        }
    }


    /// <summary>
    /// Method to move the ball to a random position in the environment.
    /// </summary>
    // public Vector3 MoveToRandomPosition()
    // {
    //     float _randPosX = Random.Range(-1f, 1f);
    //     float _randPosZ = Random.Range(-1f, 1f);
    //     var newpos = new Vector3(_randPosX, 0.5f, _randPosZ);
    //     return newpos;
    // }
}
