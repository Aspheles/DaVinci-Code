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

        scale.x = Input.GetAxis("Mouse ScrollWheel") * 2;
        scale.y = Input.GetAxis("Mouse ScrollWheel") * 2;
        scale.z = Input.GetAxis("Mouse ScrollWheel") * 2;

        rotation.y += Input.GetAxis("Mouse X") * 10;
        rotation.x -= Input.GetAxis("Mouse Y") * 10;

        transform.eulerAngles = rotation;
        transform.localScale += scale;
    }
    void Update()
    {

        Move();
        
    }
}
