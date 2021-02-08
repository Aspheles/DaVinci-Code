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

    private bool inAir;


    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        animation = figure.GetComponent<Animator>();
    }
    
    public void Animate(string name)
    {

    }
    public void Move(float speed)
    {
        // head.transform.eulerAngles = Vector3.RotateTowards(0, 1, 0);
        head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, camera.transform.GetChild(0).rotation, 3.5f);
        //Mathf.Clamp(head.transform.rotation, head.transform.rotation * Quaternion.Euler(0,- 90, 0), 90);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed += 5;
        }

        if (Input.GetKey("w"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w), 1);
            transform.position += new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * Time.deltaTime * speed;
        }

        if (Input.GetKey("d"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w) * Quaternion.Euler(0, 90, 0), 1);
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * speed;
        }

        if (Input.GetKey("s"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w) * Quaternion.Euler(0, 180, 0), 1);
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * speed;
        }

        if (Input.GetKey("a"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.GetChild(0).rotation.y, 0, camera.transform.GetChild(0).rotation.w) * Quaternion.Euler(0, -90, 0), 1);
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
               rb.AddForce(new Vector3(0, 10, 0));  
        }

        else
        {
            //animation.SetBool("Idle", true);
        }
    }

    void Update()
    {
        Move(5);
       
        //Animate();
    }

    private void LateUpdate()
    {
        
    }
}
