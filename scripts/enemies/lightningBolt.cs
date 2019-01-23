using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBolt : MonoBehaviour {

    private Transform position;
    public drop ball;


    public List<Vector2> targets;
    private int number;

	// Use this for initialization
	void Start () {
        number = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = targets[number];
        number++;
        if (number >= 4)
            number = 0;
        Instantiate<drop>(ball, transform.position, transform.rotation);
	}
}
