using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossWalls : MonoBehaviour {

    public walls walls;

	public void createWalls()
    {
        Instantiate(walls, new Vector3(67.13f, 6.98f, 0), Quaternion.Euler(0, 0, 0));
    }

    public void destroyWalls()
    {
        walls.Destroy(walls.gameObject);
    }


}
