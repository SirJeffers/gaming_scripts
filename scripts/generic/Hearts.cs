using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour {

    public Sprite [] heartsArray;
    public Image currentHeart;

	// Use this for initialization
	void Start () {
        currentHeart.sprite = heartsArray[0];
	}
	
    public void damage(int i)
    {
        currentHeart.sprite = heartsArray[i];
    }
	
}
