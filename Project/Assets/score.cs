using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    int score1;
    int score2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        score1 = GameObject.Find("Drone_AI").GetComponent<DroneAgent>().score1;
        score2 = GameObject.Find("Drone_Player").GetComponent<DroneAgentHuman>().score2;

        string s = "AI " + score1.ToString() + " vs " + "Human " + score2.ToString();

        GameObject.Find("Text").GetComponent<Text>().text = s;
	}
}
