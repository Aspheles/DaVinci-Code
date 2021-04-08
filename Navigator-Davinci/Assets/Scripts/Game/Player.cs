using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject figure;
    [SerializeField] public GameCamera camera;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private GameObject head;

    private Rigidbody rb;
    private Animator animation;

    private GameObject ground;

    public bool idle;
    private bool running;
    private bool sprinting;
    private bool inAir;
    string[] animations;

    float speed = 3;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        animation = figure.GetComponent<Animator>();
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
                inAir = true;
                rb.AddForce(new Vector3(0, 200, 0));
            }

            else if (!Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey(KeyCode.Space))
            {
                running = false;
                idle = true;
            }

            Animate();
        
    }

   private void OnCollisionStay(Collision ground)
    {
        inAir = false;

        Animate();
    }

    private void OnCollisionExit(Collision ground)
    {
        Animate();
    }

    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {

        if (head.transform.eulerAngles.y < 80 && head.transform.eulerAngles.y > 280)
        {
            print("test");
            head.transform.rotation = pivotPoint.transform.rotation;
        }
    }
}
