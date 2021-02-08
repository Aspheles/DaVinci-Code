using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;

    Vector3 rotation;
    Vector3 scale;

    void Start()
    {
        rotation = transform.eulerAngles;
        scale = transform.localScale;
    }
    void Move()
    {
        transform.GetChild(0).LookAt(target.transform);

        rotation.y += Input.GetAxis("Mouse X") * 10;
        rotation.x -= Input.GetAxis("Mouse Y") * 10;

        transform.eulerAngles = rotation;
        
    }

    void Zoom(int value)
    {

        scale.x = value;
        scale.y = value;
        scale.z = value;
            
        transform.localScale = Vector3.MoveTowards(transform.localScale, transform.localScale + scale, Time.deltaTime * 3);
  
    }
    void Update()
    {

        Move();
        if (Input.GetKey("="))
        {
            Zoom(-1);
        }

        if (Input.GetKey("-"))
        {
            Zoom(1);
        }
    }
}
