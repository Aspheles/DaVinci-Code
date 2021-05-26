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
    public static GameCamera instance;
    public bool canMove;

    void Start()
    {
        instance = this;
        canMove = true;
        UnityEngine.Cursor.visible = false;
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rotation = pivotPoint.transform.eulerAngles;
        minZoom = -0.5f;
        maxZoom = -3f;

        maxDistance = 5;

        position = transform.localPosition;

        distance = transform.localPosition.magnitude;
        dollyDir = transform.localPosition.normalized;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey("="))
            {
                Zoom(0.05f);
            }

            if (Input.GetKey("-"))
            {
                Zoom(-0.05f);
            }


            Move();
            Collision();
        }
        
    }

    void Move()
    {
        //Move
        transform.LookAt(target.transform);
        
        rotation.y += Input.GetAxis("Mouse X") * 10;
        rotation.x -= Input.GetAxis("Mouse Y") * 10;
        rotation.x = Mathf.Clamp(rotation.x, -40, 60);
            
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
                distance = Mathf.Clamp(hit.distance * (position.magnitude * 0.3f), 0.5f, position.magnitude);
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
