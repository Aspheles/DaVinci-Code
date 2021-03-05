using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InanimateIdle : MonoBehaviour
{
    private Vector3 position;

    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float range;

    private bool status;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {

        float rotation = 0.1f * rotationSpeed;

        transform.eulerAngles += new Vector3(0, rotation, 0);

    }
}