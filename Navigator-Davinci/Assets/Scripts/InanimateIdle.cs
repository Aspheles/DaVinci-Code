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

        StartCoroutine(Move());

    }

    private IEnumerator Move()
    {
        if (status == false)
        {
            print(status);
            while (transform.position != position)
            {
                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * moveSpeed);
                yield return null;
            }
            status = true;
            StartCoroutine(Move());
        }

        if (status == true)
        {
            while (transform.position != position + new Vector3(0, range, 0))
            {
                transform.position = Vector3.Lerp(transform.position, position + new Vector3(0, range, 0), Time.deltaTime * moveSpeed);
                yield return null;
            }
            status = false;
            StartCoroutine(Move());
        }

    }
    private void Update()
    {

        float rotation = 0.1f * rotationSpeed;

        transform.eulerAngles += new Vector3(0, rotation, 0);

    }
}