using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject figure;
    [SerializeField] private GameCamera camera;
    private Animator animation;

    private void Start()
    {
        animation = figure.GetComponent<Animator>();
    }
    
    public void Animate(bool status)
    {
        if(status)
        {
            animation.SetBool("Idle", false);
            animation.SetBool("Running", true);
        }
        else
        {
            animation.SetBool("Running", false);
            animation.SetBool("Idle", true);
        }
        
    }
    public void Move(float speed)
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            speed += 5;
        }

        if (Input.GetKey("w"))
        {
            Animate(true);
            transform.position += new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * Time.deltaTime * speed;
            transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, new Vector3(0, camera.transform.GetChild(0).eulerAngles.y, 0), 1, 2.5f);
        }

        if (Input.GetKey("d"))
        {
            Animate(true);
            transform.position += new Vector3(camera.transform.right.x, 0, camera.transform.right.z) * Time.deltaTime * speed;
            transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, new Vector3(0, camera.transform.GetChild(0).eulerAngles.y + 90, 0), 1, 2.5f);
        }

        if (Input.GetKey("s"))
        {
            Animate(true);
            transform.position -= new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * Time.deltaTime * speed;
            transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, new Vector3(0, camera.transform.GetChild(0).eulerAngles.y + 180 , 0), 1, 2.5f);
        }

        if (Input.GetKey("a"))
        {
            Animate(true);
            transform.position -= new Vector3(camera.transform.right.x, 0, camera.transform.right.z) * Time.deltaTime * speed;
            transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, new Vector3(0, camera.transform.GetChild(0).eulerAngles.y + -90, 0), 1, 2.5f);
        }

        else
        {
            Animate(false);
        }
    }

    void Update()
    {
        Move(7);
    }
}
