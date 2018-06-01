using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone_controller_human : MonoBehaviour {

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

        float delta_throttle = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            delta_throttle = 1;
            //if (throttle < 100)
                //throttle++;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            delta_throttle = -1;
            //if (throttle > 1)
                //throttle--;
        }
                
        delta_throttle = Mathf.Max(delta_throttle, -1);
        delta_throttle = Mathf.Min(delta_throttle, 1);

        //throttle += delta_throttle;
        throttle = 15 + delta_throttle;
        throttle = Mathf.Max(throttle, 0);
        throttle = Mathf.Min(throttle, 30);

        // PITCH AND YAW

        if (Input.GetKey(KeyCode.W))
        {
            drone_rb.AddTorque(-Vector3.right*5);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            drone_rb.AddTorque(Vector3.right*5);
        }

        float roll = 0;

        if (Input.GetKey(KeyCode.A))
        {
            //drone_rb.AddTorque(-Vector3.forward*15);
            roll--;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //drone_rb.AddTorque(Vector3.forward*15);
            roll++;
        }

        Debug.Log(throttle);
        //drone_rb.AddForce(transform.up*(throttle/3));

        altitude = transform.position.y;
        velocity = drone_rb.velocity.magnitude;

        drone_rb.transform.Translate(roll * Vector3.forward);*/

        // PROPELLER ROTATION
        rotor1.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
        rotor2.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
        rotor3.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
        rotor4.transform.Rotate(transform.up * Time.deltaTime * 3000, Space.World);
    }


   
}
