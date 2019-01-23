using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    private Animator strikeController;

    private bool strike;
    // Use this for initialization
	void Start () {
        strikeController = GetComponent<Animator>();
        strike = false;
	}
	
    public void setstrike(bool strike)
    {
        this.strike = strike;
        strikeController.SetBool("strike", strike);
    }

    public bool getStrike()
    {
        return strike;
    }
    

}
