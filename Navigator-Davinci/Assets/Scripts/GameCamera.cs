using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject pivotPoint;

    Vector3 rotation;
    Vector3 scale;

    bool clamping;
    bool isColliding;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rotation = pivotPoint.transform.eulerAngles;
        scale = pivotPoint.transform.localScale;
}
    void Move()
    {

            if(rotation.x < 0)
            {

            }
            transform.LookAt(target.transform);
        
            rotation.y += Input.GetAxis("Mouse X") * 10;
            rotation.x -= Input.GetAxis("Mouse Y") * 10;
            rotation.x = Mathf.Clamp(rotation.x, -20, 85);
            
            pivotPoint.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            ClampCamera(rotation.x);
    }

    private void ClampCamera(float rotationx)
    {

        if(rotation.x < 20)
        {
            clamping = true;
            scale.x = 0.05f * rotationx;
            scale.y = 0.05f * rotationx;
            scale.z = 0.05f * rotationx;

            scale.x = Mathf.Clamp(scale.x, 0.5f, 5);
            scale.y = Mathf.Clamp(scale.y, 0.5f, 5);
            scale.z = Mathf.Clamp(scale.z, 0.5f, 5);

            pivotPoint.transform.localScale = scale;

        }
        else
        {
            clamping = false;
        }
    }

    private void Zoom(float value, float min, float max)
    {
        scale.x = value;
        scale.y = value;
        scale.z = value;
        
        if(value == 1)
        {
            if(pivotPoint.transform.localScale.x < max && pivotPoint.transform.localScale.y < max && pivotPoint.transform.localScale.z < max)
            {
                pivotPoint.transform.localScale = Vector3.MoveTowards(pivotPoint.transform.localScale, pivotPoint.transform.localScale + scale, Time.deltaTime * 3);
            }
        }

        if(value == -1)
        {
            if (pivotPoint.transform.localScale.x > min && pivotPoint.transform.localScale.y > min && pivotPoint.transform.localScale.z > min)
            {
                pivotPoint.transform.localScale = Vector3.MoveTowards(pivotPoint.transform.localScale, pivotPoint.transform.localScale + scale, Time.deltaTime * 3);
            }
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    void Update()
    {
        if(!clamping)
        {
            if(Input.GetKey("-"))
            {
                Zoom(1, 0.5f, 4);
            }

            if (Input.GetKey("="))
            {
                Zoom(-1, 4, 4);
            }

        }
        Move();
    }
}
