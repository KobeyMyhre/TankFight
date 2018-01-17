using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {



    TankController controller;
    Rigidbody2D rb;
    public float speed;
    public float bodyRotSpeed;
    public float rotSpeed;
    public Transform body;
    public Transform barrel;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<TankController>();
        rb = GetComponent<Rigidbody2D>();
	}
    public float currentAngle;
    public float desiredAngle;
    
    // Update is called once per frame
    void Update ()
    {
        Vector2 move = new Vector2(controller.moveX, controller.moveY);
        rb.velocity = move * speed;

        Vector2 forward = body.transform.position + body.transform.forward;
        Vector2 lookVel = (Vector2)body.transform.position + rb.velocity.normalized;
        Debug.DrawLine(body.transform.position, body.transform.position + new Vector3(rb.velocity.x, rb.velocity.y, 0));

        Vector2 bodyRot = new Vector2(body.transform.position.x - controller.moveX, body.transform.position.y - controller.moveY);
        body.transform.up = Vector3.Slerp(body.transform.up, ((Vector2)body.transform.position - bodyRot).normalized, bodyRotSpeed * Time.deltaTime);




        Vector2 desireRot = new Vector2(barrel.transform.position.x - controller.rotX, barrel.transform.position.y - controller.rotY);
        barrel.transform.up = Vector3.Slerp(barrel.transform.up, ((Vector2)barrel.transform.position - desireRot).normalized, rotSpeed * Time.deltaTime);

        
    }
}
