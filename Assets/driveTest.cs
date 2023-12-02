using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driveTest : MonoBehaviour
{
    public PrometeoCarController controller;
    public Rigidbody rb;
    void Start()
    {
        controller = GetComponent<PrometeoCarController>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        controller.TurnRight();
    }
}
