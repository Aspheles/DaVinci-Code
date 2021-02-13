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
    Vector3 position;

    float minZoom;
    float maxZoom;
    public float distance;
    Vector3 dollyDir;
    float maxDistance;

    bool zooming;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rotation = pivotPoint.transform.eulerAngles;
        minZoom = -15f;
        maxZoom = -80f;

        maxDistance = 50;

        position = transform.localPosition;

        distance = transform.localPosition.magnitude;
        dollyDir = transform.localPosition.normalized;
    }

    void Update()
    {
        if (Input.GetKey("="))
        {
            Zoom(1);
        }

        if (Input.GetKey("-"))
        {
            Zoom(-1);
        }


        Move();

    }

    private void FixedUpdate()
    {
        Collision();
    }
    void Move()
    {

            if(rotation.x < 0)
            {

            }
            transform.LookAt(target.transform);
        
            rotation.y += Input.GetAxis("Mouse X") * 10;
            rotation.x -= Input.GetAxis("Mouse Y") * 10;
            rotation.x = Mathf.Clamp(rotation.x, -45, 85);
            
            pivotPoint.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

    }

    private void Collision()
    {
        RaycastHit hit;
        Vector3 standardPosition = pivotPoint.transform.TransformPoint(dollyDir * position.magnitude);

        if (Physics.Linecast(pivotPoint.transform.position, standardPosition, out hit))
        {
            if (hit.transform.tag == "Terrain")
            {
                distance = Mathf.Clamp(hit.distance * (position.magnitude * 0.4f), 0.5f, position.magnitude);
            }
        }

        else
        {
            distance = position.magnitude;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * 20);
    }

    private void Zoom(float value)
    {
        zooming = true;
        if(transform.localPosition.z > maxZoom && transform.localPosition.z < minZoom)
        {
            position.z += value;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, position.z);
        }

        if(transform.localPosition.z < maxZoom || transform.localPosition.z > minZoom)
        {
            position.z -= value;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, position.z);
        }
    }
}
