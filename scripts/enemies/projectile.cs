using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {


    public Transform target;

    private Vector2 shotTarget;
    private float speed;
    private Rigidbody2D ballbody;

    private float existanceTime;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        shotTarget = target.position - transform.position;
        ballbody = GetComponent<Rigidbody2D>();
        speed = (Vector2.Distance(this.transform.position, shotTarget))/3;
	}
	
	// Update is called once per frame
	void Update()
    {
        ballbody.velocity = (shotTarget);
        existanceTime += Time.deltaTime;

        if (existanceTime > 5.0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(tag == "enemyprojectile")
        {
            if (collision.gameObject.CompareTag("sword"))
            {
                tag = "projectile";
                shotTarget *= -3;
            }
            else if (!collision.gameObject.CompareTag("sword") && !collision.gameObject.CompareTag("enemy") && !collision.gameObject.CompareTag("projectile"))
                Destroy(gameObject);

        }
        else
        {
            if(!collision.gameObject.CompareTag("enemyprojectile") && !collision.gameObject.CompareTag("sword") && !collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
