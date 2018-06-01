using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone_controller : MonoBehaviour {

    public float throttle = 0.0f;
    public float altitude;
    public float velocity;


    GameObject rotor1, rotor2, rotor3, rotor4;

    Rigidbody drone_rb;

	// Use this for initialization
	void Start () {
        drone_rb = GetComponent<Rigidbody>();

        rotor1 = GameObject.Find("ROTOR1");
        rotor2 = GameObject.Find("ROTOR2");
        rotor3 = GameObject.Find("ROTOR3");
        rotor4 = GameObject.Find("ROTOR4");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*// THROTTLE
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (throttle < 100)
                throttle++;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (throttle > 1)
                throttle--;
        }

        // PITCH AND YAW
        if (Input.GetKey(KeyCode.W))
        {
            drone_rb.AddTorque(-Vector3.right*5);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            drone_rb.AddTorque(Vector3.right*5);
        }

        if (Input.GetKey(KeyCode.A))
        {
            drone_rb.AddTorque(-Vector3.forward*15);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            drone_rb.AddTorque(Vector3.forward*15);
        }

        Debug.Log(throttle);
        drone_rb.AddForce(transform.up*(throttle/3));

        altitude = transform.position.y;
        velocity = drone_rb.velocity.magnitude;*/


        // PROPELLER ROTATION
        rotor1.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
        rotor2.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
        rotor3.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
        rotor4.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
    }


   
}
