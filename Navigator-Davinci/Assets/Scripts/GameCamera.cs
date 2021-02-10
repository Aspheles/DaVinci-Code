using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject pivotPoint;

    Vector3 rotation;
    Vector3 scale;
    bool isColliding = false;

    void Start()
    {
        rotation = pivotPoint.transform.eulerAngles;
        scale = pivotPoint.transform.localScale;
    }
    void Move()
    {

        transform.LookAt(target.transform);
        rotation.y += Input.GetAxis("Mouse X") * 10;
        rotation.x -= Input.GetAxis("Mouse Y") * 10;

        if (Input.GetAxis("Mouse Y") > 0)
        {
            pivotPoint.transform.localScale = Vector3.MoveTowards(pivotPoint.transform.localScale, pivotPoint.transform.localScale + new Vector3(-1, -1, -1), Time.deltaTime * 4);
        }

        if (Input.GetAxis("Mouse Y") < 0)
        {
            pivotPoint.transform.localScale = Vector3.MoveTowards(pivotPoint.transform.localScale, pivotPoint.transform.localScale + new Vector3(1, 1, 1), Time.deltaTime * 4);
        }

        pivotPoint.transform.eulerAngles = rotation;
    }

    void Zoom(int value, float range, float rate)
    {

        scale.x = value;
        scale.y = value;
        scale.z = value;
        
        if(value == 1)
        {
            if(pivotPoint.transform.localScale.x < range && pivotPoint.transform.localScale.y < range && pivotPoint.transform.localScale.z < range)
            {
                pivotPoint.transform.localScale = Vector3.MoveTowards(pivotPoint.transform.localScale, pivotPoint.transform.localScale + scale, Time.deltaTime * rate);
            }
        }

        if (value == -1)
        {
            if (pivotPoint.transform.localScale.x > range && pivotPoint.transform.localScale.y > range && pivotPoint.transform.localScale.z > range)
            {
                pivotPoint.transform.localScale = Vector3.MoveTowards(pivotPoint.transform.localScale, pivotPoint.transform.localScale + scale, Time.deltaTime * rate);
            }
        }


    }

    void Update()
    {

        Move();

        if (Input.GetKey("="))
        {
            Zoom(-1, 1, 3);
        }

        if (Input.GetKey("-"))
        {
            Zoom(1, 5, 3);
        }
    }
}
