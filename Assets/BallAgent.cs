using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class BallAgent : Agent
{
    private Rigidbody rigidBody;

    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        if (transform.localPosition.y < 0)
        {
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0, 0.5f,0);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rigidBody.velocity.x);
        sensor.AddObservation(rigidBody.velocity.z);
        sensor.AddObservation(Target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionX = actions.ContinuousActions[0];
        var actionZ = actions.ContinuousActions[1];

        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionX;
        controlSignal.z = actionZ;

        rigidBody.AddForce(controlSignal * 10);

        float distanceToTarget = Vector3.Distance(transform.localPosition, Target.localPosition);

        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
            ;
        }

        if (transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }
}
