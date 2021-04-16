using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public static Player instance;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject figure;
    [SerializeField] public GameCamera camera;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private GameObject head;

    private Rigidbody rb;
    private Animator animation;

    private GameObject ground;

    public bool idle;
    private bool running;
    private bool sprinting;
    public bool canwalk;

    float speed = 3;

    private void Start()
    {
        instance = this;
        rb = player.GetComponent<Rigidbody>();
        animation = figure.GetComponent<Animator>();
        ground = GameObject.FindGameObjectWithTag("Ground");
        canwalk = true;
       

    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Room")
        {
            if (RunManager.instance.puzzles.Count > 0 && Room.instance.terminalsLoaded)
            {
                RunManager.instance.loadingScreen.SetActive(false);
                canwalk = true;
                //Session.instance.message = "Questions have been loaded";
            }
            else
            {
                RunManager.instance.loadingScreen.SetActive(true);
                canwalk = false;
                //Session.instance.message = "Waiting for questions to load...";
            }
        }
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
    }
    public void Move()
    {
        if (canwalk)
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

            else if (!Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("a"))
            {
                running = false;
                idle = true;
            }

            Animate();
        }
            
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {
        if (pivotPoint.transform.localRotation.eulerAngles.y > 275 || pivotPoint.transform.localRotation.eulerAngles.y < 85)
        {
            head.transform.localRotation = pivotPoint.transform.localRotation * Quaternion.Euler(-25, 0, 0);
        }

        if (head.transform.localRotation.eulerAngles.y < 275 && head.transform.localRotation.eulerAngles.y > 85)
        {
            head.transform.localRotation = new Quaternion(0, head.transform.localRotation.eulerAngles.y, 0, head.transform.localRotation.w) * Quaternion.Euler(-25, 0, 0);
        }
    }
}
