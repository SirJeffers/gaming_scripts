using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {

    public Transform character;

    private Vector3 tempLocation;
    bool usecharacter;

    private Vector3 currentLocation;
    private Vector3 characterLocation;

	// Use this for initialization
	void Start () {
        //character = GameObject.FindGameObjectWithTag("Player").transform;
        characterLocation = new Vector3(character.position.x, character.position.y + 3, -10);
        currentLocation = transform.position;
        usecharacter = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //characterLocation = new Vector3(character.position.x, character.position.y + 2, -10);
        characterLocation = usecharacter ? character.position : tempLocation;
        currentLocation = transform.position;
        transform.position = Vector3.Lerp(currentLocation, characterLocation, Time.deltaTime*3);
	}

    public void setTarget(Vector3 holdTarget)
    {
        tempLocation = holdTarget;
        usecharacter = false;
    }

    public void resetTarget()
    {
        usecharacter = true;
    }

    public void setScale(float scale)
    {
        gameObject.GetComponent<Camera>().orthographicSize = scale;
    }
}
