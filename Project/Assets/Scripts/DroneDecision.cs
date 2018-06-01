using System.Collections.Generic;
using UnityEngine;

public class DroneDecision : MonoBehaviour, Decision
{
    public float[] Decide(
        List<float> vectorObs,
        List<Texture2D> visualObs,
        float reward,
        bool done,
        List<float> memory)
    {
        if (gameObject.GetComponent<Brain>().brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            List<float> act = new List<float>();

            float[] actions = GameObject.Find("Drone").GetComponent<DroneAgent>().actions;

            act.Add(actions[0]);
            act.Add(actions[1]);
            act.Add(actions[2]);

            return act.ToArray();
        }

        return new float[1] { 1f };
    }

    public List<float> MakeMemory(
        List<float> vectorObs,
        List<Texture2D> visualObs,
        float reward,
        bool done,
        List<float> memory)
    {
        return new List<float>();
    }
}
