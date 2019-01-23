using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTracker : MonoBehaviour {

    public Transform characterLocation;
    public cameraMovement control;
    public bossWalls walls;
    bool madewalls;

    public List<Vector3> lockLocations;

	// Use this for initialization
	void Start () {
        //lockLocations.Add(new Vector2(1, 2));
        madewalls = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(characterLocation.position.x < -5)
        {
            control.setTarget(lockLocations[2]);
        }
        else if (characterLocation.position.x > 8.0f && characterLocation.position.x < 32)
        {
            control.setTarget(lockLocations[0]);
        }
        else if (characterLocation.position.x > 68.7f && characterLocation.position.x < 106)
        {
            if (!madewalls)
            {
                walls.createWalls();
                madewalls = true;
            }
            control.setTarget(lockLocations[1]);
            control.setScale(11.5f);
        }
        else
        {
            control.resetTarget();
            control.setScale(10);
        }
    }
}
