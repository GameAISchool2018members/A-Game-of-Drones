using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneAgent : Agent
{
    float throttle;
    float altitude;
    float delta_d;
    float dist_previous;
    public int score1;
    public int score2;

    public float[] actions;

    // TARGET

    private List<Vector3> targets;

    private int index = 0;

    Rigidbody drone_rb;

    void Start()
    {
        drone_rb = GetComponent<Rigidbody>();
        throttle = 15;
        delta_d = Vector3.Distance(Target.position, this.transform.position);
        dist_previous = delta_d;

        // TARGETS

        index = 0;

        Vector3 point;
        float radius = 20;

        targets = new List<Vector3>();

        for (int i = 0; i < 100; i++)
        {
            float angle = i * Mathf.PI / 5;

            point = new Vector3(Random.Range(-60, 60), Random.Range(20, 60), Random.Range(-60, 60));

            //point = new Vector3(radius * Mathf.Cos(angle), 20, radius * Mathf.Sin(angle));

            targets.Add(point);
        }

        GameObject.Find("Target").transform.position = targets[0];

        score1 = 0;
        score2 = 0;
    }

    public Transform Target;

    public override void AgentReset()
    {
        delta_d = Vector3.Distance(Target.position, this.transform.position);
        throttle = 15;
        dist_previous = delta_d;

        // TARGETS:

        if (delta_d < 5)
        {
            //Debug.Log(index);
            GameObject.Find("Target").transform.position = targets[index];
            index++;

            if (this.name == "Drone_AI")
            {
                score1++;
            }
            else
            {
                score2++;
            }
        }
        else
        {
            /*this.transform.position = new Vector3(0, 10, 0);
            this.transform.rotation = new Quaternion(0, 0, 0, 1);
            this.drone_rb.angularVelocity = Vector3.zero;
            this.drone_rb.velocity = Vector3.zero;*/
        }

        Done();
    }

    public void CollectObservations_Old()
    {

        // Calculate relative position
        Vector3 relativePosition = Target.position - this.transform.position;
        delta_d = relativePosition.magnitude;

        // Relative position
        AddVectorObs(relativePosition.x);
        AddVectorObs(relativePosition.y);
        AddVectorObs(relativePosition.z);

        // Relative velocity
        AddVectorObs(this.drone_rb.velocity.x);
        AddVectorObs(this.drone_rb.velocity.y);
        AddVectorObs(this.drone_rb.velocity.z);

        // Euler angles
        float x_angle = this.transform.rotation.eulerAngles.x * Mathf.Deg2Rad;
        x_angle = Mathf.Atan2(Mathf.Sin(x_angle), Mathf.Cos(x_angle));
        float y_angle = this.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        y_angle = Mathf.Atan2(Mathf.Sin(y_angle), Mathf.Cos(y_angle));
        float z_angle = this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        z_angle = Mathf.Atan2(Mathf.Sin(z_angle), Mathf.Cos(z_angle));
        AddVectorObs(x_angle);
        AddVectorObs(y_angle);
        AddVectorObs(z_angle);

        //Debug.Log(this.transform.rotation.eulerAngles.z);
        //Debug.Log(relativePosition.x);

        // Restart Agent
        if (delta_d < 4)
        {
            //Debug.Log(100.0f);
            AddReward(10.0f);
            this.AgentReset();
        }    
        else if (delta_d > 100)
        {
            AddReward(-10.0f);
            this.AgentReset();
        }
        else if (((this.transform.rotation.eulerAngles.x < 315) && (this.transform.rotation.eulerAngles.x > 45)) || ((this.transform.rotation.eulerAngles.z < 315) && (this.transform.rotation.eulerAngles.z > 45)))
        {
            AddReward(-10.0f);
            this.AgentReset();
        }
        else if (this.transform.position.y < 1.5)
        {
            AddReward(-10.0f);
            this.AgentReset();
        }
    }

    public override void CollectObservations()
    {

        // Calculate relative position
        Vector3 relativePosition = Target.position - this.transform.position;
        delta_d = relativePosition.magnitude;

        // Relative position
        AddVectorObs(relativePosition.x);
        AddVectorObs(relativePosition.y);
        AddVectorObs(relativePosition.z);

        // Relative velocity
        AddVectorObs(this.drone_rb.velocity.x);
        AddVectorObs(this.drone_rb.velocity.y);
        AddVectorObs(this.drone_rb.velocity.z);

        // Euler angles
        float x_angle = this.transform.rotation.eulerAngles.x * Mathf.Deg2Rad;
        x_angle = Mathf.Atan2(Mathf.Sin(x_angle), Mathf.Cos(x_angle));
        float y_angle = this.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        y_angle = Mathf.Atan2(Mathf.Sin(y_angle), Mathf.Cos(y_angle));
        float z_angle = this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        z_angle = Mathf.Atan2(Mathf.Sin(z_angle), Mathf.Cos(z_angle));
        AddVectorObs(x_angle);
        AddVectorObs(y_angle);
        AddVectorObs(z_angle);

        //Debug.Log(this.transform.rotation.eulerAngles.z);
        Debug.Log(relativePosition.x);

        // Restart Agent
        if (delta_d < 4)
        {
            //Debug.Log(100.0f);
            AddReward(10.0f);
            this.AgentReset();
        }
        else if (delta_d > 100)
        {
            AddReward(-10.0f);
            this.AgentReset();
        }
        else if (((this.transform.rotation.eulerAngles.x < 315) && (this.transform.rotation.eulerAngles.x > 45)) || ((this.transform.rotation.eulerAngles.z < 315) && (this.transform.rotation.eulerAngles.z > 45)))
        {
            AddReward(-10.0f);
            this.AgentReset();
        }
        else if (this.transform.position.y < 1.5)
        {
            AddReward(-10.0f);
            this.AgentReset();
        }
    }

    public void AgentAction_Old(float[] vectorAction, string textAction)
    {

        // Add distance reward
        float delta_delta_d = delta_d - dist_previous;
        dist_previous = delta_d;
        float reward = -1*delta_delta_d;

        //Debug.Log(reward);
        AddReward(reward);

        // THROTTLE
        float delta_throttle = vectorAction[0];

        delta_throttle = Mathf.Max(delta_throttle, -1);
        delta_throttle = Mathf.Min(delta_throttle, 1);

        //throttle += delta_throttle;
        throttle = 15 + delta_throttle;
        throttle = Mathf.Max(throttle, 0);
        throttle = Mathf.Min(throttle, 30);

        //drone_rb.AddRelativeForce(throttle * transform.up);
        drone_rb.transform.Translate(delta_throttle / 5 * transform.up);

        // ROLL
        float roll = vectorAction[1];
        roll = Mathf.Max(roll, -1);
        roll = Mathf.Min(roll, 1);

        // Restitution rall
        float delta_rotation_x = this.transform.rotation.eulerAngles.x;
        if (this.transform.rotation.eulerAngles.x > 180)
        {
            delta_rotation_x = this.transform.rotation.eulerAngles.x - 360;

        }
        float roll_rest = -0.0075f*delta_rotation_x;

        //drone_rb.AddRelativeTorque((-roll+roll_rest) * Vector3.right);
        drone_rb.transform.Translate(roll * Vector3.forward);

        // PITCH
        float pitch = vectorAction[2];
        pitch = Mathf.Max(pitch, -1);
        pitch = Mathf.Min(pitch, 1);

        // Restitution pitch
        float delta_rotation_z = this.transform.rotation.eulerAngles.z;
        if (this.transform.rotation.eulerAngles.z > 180)
        {
            delta_rotation_z = this.transform.rotation.eulerAngles.z - 360;
        }
        float pitch_rest = -0.0075f * delta_rotation_z;

        //drone_rb.AddRelativeTorque((-pitch+pitch_rest) * Vector3.forward);
        drone_rb.transform.Translate(pitch * Vector3.right);

        drone_rb.transform.Rotate(0, pitch, 0, 0);

        altitude = transform.position.y;

        actions = new float[3];
        actions[0] = throttle;
        actions[1] = roll;
        actions[2] = pitch;
    }


    public override void AgentAction(float[] vectorAction, string textAction)
    {

        // Add distance reward
        float delta_delta_d = delta_d - dist_previous;
        dist_previous = delta_d;
        float reward = -1 * delta_delta_d;

        //Debug.Log(reward);
        AddReward(reward);

        // THROTTLE
        float delta_throttle = vectorAction[0];

        delta_throttle = Mathf.Max(delta_throttle, -1);
        delta_throttle = Mathf.Min(delta_throttle, 1);

        //throttle += delta_throttle;
        throttle = 15 + delta_throttle;
        throttle = Mathf.Max(throttle, 0);
        throttle = Mathf.Min(throttle, 30);

        //drone_rb.velocity = Vector3.zero;
        //drone_rb.AddRelativeForce(throttle * transform.up);
        drone_rb.velocity += 1 * delta_throttle * transform.up;
        //drone_rb.transform.Translate(delta_throttle / 5 * transform.up);

        // ROLL
        float roll = vectorAction[1];
        roll = Mathf.Max(roll, -1);
        roll = Mathf.Min(roll, 1);

        // Restitution rall
        float delta_rotation_x = this.transform.rotation.eulerAngles.x;
        if (this.transform.rotation.eulerAngles.x > 180)
        {
            delta_rotation_x = this.transform.rotation.eulerAngles.x - 360;

        }
        float roll_rest = -0.0075f * delta_rotation_x;

        //drone_rb.AddRelativeTorque((-roll+roll_rest) * Vector3.right);
        //drone_rb.transform.Translate(roll * Vector3.forward);
        drone_rb.velocity += -1 * roll * Vector3.forward;

        // PITCH
        float pitch = vectorAction[2];
        pitch = Mathf.Max(pitch, -1);
        pitch = Mathf.Min(pitch, 1);

        // Restitution pitch
        float delta_rotation_z = this.transform.rotation.eulerAngles.z;
        if (this.transform.rotation.eulerAngles.z > 180)
        {
            delta_rotation_z = this.transform.rotation.eulerAngles.z - 360;
        }
        float pitch_rest = -0.0075f * delta_rotation_z;

        //drone_rb.AddRelativeTorque((-pitch+pitch_rest) * Vector3.forward);
        drone_rb.velocity += 1 * pitch * Vector3.right;
        //drone_rb.transform.Translate(pitch * Vector3.right);
    }
}
