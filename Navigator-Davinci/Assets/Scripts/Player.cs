using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject figure;
    [SerializeField] private GameCamera camera;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private GameObject head;

    private Rigidbody rb;
    private Animator animation;

    private GameObject[] ground;

    private bool idle;
    private bool running;
    private bool sprinting;
    private bool inAir;

    float speed = 3;


    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        animation = figure.GetComponent<Animator>();
        ground = GameObject.FindGameObjectsWithTag("Ground");
    }
    
    public void Animate()
    {
        if (idle)
        {
            animation.SetBool("inAir", false);
            animation.SetBool("Running", false);
            animation.SetBool("Sprinting", false);
            animation.SetBool("Idle", true);
        }

        if (running)
        {
            animation.SetBool("inAir", false);
            animation.SetBool("Idle", false);
            animation.SetBool("Sprinting", false);
            animation.SetBool("Running", true);
        }

        if (sprinting)
        {
            animation.SetBool("inAir", false);
            animation.SetBool("Idle", false);
            animation.SetBool("Running", true);
            animation.SetBool("Sprinting", true);
        }

        if(inAir)
        {
            animation.SetBool("Idle", false);
            animation.SetBool("Running", false);
            animation.SetBool("Sprinting", false);
            animation.SetBool("inAir", true);
        }
    }
    public void Move()
    {
        float boost = 0;

        if (Input.GetKey(KeyCode.LeftControl) && running)
        {
            boost = 3;
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }

        if (Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a"))
        {
            running = true;
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * (speed + boost);
        }

        if (Input.GetKey("w"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.rotation.y, 0, camera.transform.rotation.w), 5);
        }

        if (Input.GetKey("d"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.rotation.y, 0, camera.transform.rotation.w) * Quaternion.Euler(0, 90, 0), 5);
        }

        if (Input.GetKey("s"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.rotation.y, 0, camera.transform.rotation.w) * Quaternion.Euler(0, 180, 0), 5);
        }

        if (Input.GetKey("a"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.rotation.y, 0, camera.transform.rotation.w) * Quaternion.Euler(0, -90, 0), 5);
        }

        if (Input.GetKey(KeyCode.Space) && !inAir)
        {
            rb.AddForce(new Vector3(0, 50, 0));  
        }

        else if(!Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey(KeyCode.Space))
        {
            running = false;
            idle = true;
        }

        Animate();
    }

   private void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.GetComponent<Ground>())
        {
            
            inAir = false;
            
        }
        Animate();
    }

    private void OnCollisionExit(Collision ground)
    {
        if (ground.gameObject.GetComponent<Ground>())
        {

            inAir = true;

        }
        Animate();
    }

    void Update()
    {
        Move();
    }

    void LateUpdate()
    {
        head.transform.rotation = pivotPoint.transform.rotation;
    }
}
