using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Jump") > 0)
            SceneManager.LoadScene(1);
	}
}
