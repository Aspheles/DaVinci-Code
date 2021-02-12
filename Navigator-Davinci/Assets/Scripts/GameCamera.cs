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
    float distance;
    Vector3 dollyDir;

    bool zooming;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rotation = pivotPoint.transform.eulerAngles;
        minZoom = -20f;
        maxZoom = -70f;

        position = transform.localPosition;

        distance = transform.localPosition.magnitude;
        dollyDir = transform.localPosition.normalized;
    }

    void Update()
    {
  /*      if (Input.GetKey("="))
        {
            Zoom(1);
        }

        if (Input.GetKey("-"))
        {
            Zoom(-1);
        }*/


        Move();

    }

    private void LateUpdate()
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
            rotation.x = Mathf.Clamp(rotation.x, -40, 85);
            
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

    private void Collision()
    {
        RaycastHit hit;
        Vector3 desPos = pivotPoint.transform.TransformPoint(dollyDir * distance);
        Debug.DrawLine(pivotPoint.transform.position, desPos, Color.green);
        if (Physics.Linecast(pivotPoint.transform.position, desPos, out hit))
        {
            if (hit.transform.tag == "Terrain")
            {
                distance = Mathf.Clamp(hit.distance * 0.7f, 0.2f, 5);
                print("sss");
            }

        }

        else
        {
            print("lll");
            distance = position.magnitude;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * 15);
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
