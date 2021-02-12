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
    float zAxis;

    float minZoom;
    float maxZoom;

    bool clamping;
    bool isColliding;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rotation = pivotPoint.transform.eulerAngles;
        minZoom = -20f;
        maxZoom = -70f;

        zAxis = transform.localPosition.z;
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

    }

    private void ClampCamera(float rotationx)
    {

        /*if (rotation.x < 20)
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
        }*/
    }

    private void Zoom(float value)
    {
        if(transform.localPosition.z > maxZoom && transform.localPosition.z < minZoom)
        {
            zAxis += value;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zAxis);
        }

        if(transform.localPosition.z < maxZoom || transform.localPosition.z > minZoom)
        {
            zAxis -= value;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zAxis);
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
        if(Input.GetKey("-"))
        {
            Zoom(1);
        }

        if (Input.GetKey("="))
        {
            Zoom(-1);
        }

        Move();
    }
}
