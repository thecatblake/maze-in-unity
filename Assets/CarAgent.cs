using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class CarAgent : Agent
{
    public PrometeoCarController controller;
    void Start()
    {
        controller = GetComponent<PrometeoCarController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        discreteActions[0] = h == 0 ? 0 : h > 0 ? 1 : -1;
        discreteActions[1] = v == 0 ? 0 : v > 0 ? 1 : -1;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;

        if (discreteActions[0] == 1)
        {
            controller.TurnRight();
        } else if (discreteActions[0] == -1)
        {
            controller.TurnLeft();
        }

        if (discreteActions[1] == 1)
        {
            controller.GoForward();
        } else if (discreteActions[1] == -1)
        {
            controller.Brakes();
        }
    }
}
