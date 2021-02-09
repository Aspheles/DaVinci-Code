using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject figure;
    [SerializeField] private GameCamera camera;
    [SerializeField] private GameObject head;

    private Rigidbody rb;
    private Animator animation;

    private bool idle;
    private bool running;
    private bool sprinting;
    private bool inAir;


    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        animation = figure.GetComponent<Animator>();
    }
    
    public void Animate()
    {
        if (idle)
        {
            animation.SetBool("Running", false);
            animation.SetBool("Sprinting", false);
            animation.SetBool("Idle", true);
        }

        if (running)
        {
            animation.SetBool("Idle", false);
            animation.SetBool("Sprinting", false);
            animation.SetBool("Running", true);
        }

        if (sprinting)
        {
            animation.SetBool("Idle", false);
            animation.SetBool("Running", false);
            animation.SetBool("Sprinting", true);
        }

        if(inAir)
        {

        }
    }
    public void Move(float speed)
    {
        head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, camera.transform.GetChild(0).rotation, 1);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed += 0.3f;
            running = false;
            sprinting = true;
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * speed;
            Animate();
        }

        if (Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a"))
        {
            running = true;
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * speed;
            Animate();
        }

        if (Input.GetKey("w"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w), 2);
        }

        if (Input.GetKey("d"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w) * Quaternion.Euler(0, 90, 0), 2);
        }

        if (Input.GetKey("s"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w) * Quaternion.Euler(0, 180, 0), 2);
        }

        if (Input.GetKey("a"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w) * Quaternion.Euler(0, -90, 0), 2 );
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 10, 0));  
        }

        else if(!Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("a"))
        {
            running = false;
            sprinting = false;
            idle = true;
            Animate();
        }

    }

    void Update()
    {
        Move(3);
    }
}
