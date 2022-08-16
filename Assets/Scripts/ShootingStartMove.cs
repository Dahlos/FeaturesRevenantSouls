using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ShootingStartMove : MonoBehaviour
{
    private Vector3 lastPositionClicked;
    private bool moving;
    public float movementSpeed = 10f;
    public float rotationSpeed;
    public Transform startPrueba;
    public Transform[] paths;
    private int currentPathIndex = 0;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPositionClicked = Camera.main!.ScreenToWorldPoint(Input.mousePosition);


            moving = true;
            // var toRotation = Quaternion.LookRotation(Vector3.forward, lastPositionClicked);
            // print("toRotation " + toRotation);
            // print("transform.rotation " + transform.rotation);
            // transform.rotation =
            //     Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            // Vector3 current = transform.forward;
            // Vector3 to = lastPositionClicked - transform.position;
            // transform.forward = Quaternion.RotateTowards(current, to, rotationSpeed * Time.deltaTime);
        }

        if (moving)
        {
            Vector3 myLocation = transform.position;
            Vector3 targetLocation = paths[currentPathIndex].position;
            if (Vector2.Distance(myLocation, targetLocation) <= 0.1f)
            {
                currentPathIndex++;
                if (currentPathIndex > paths.Length - 1) currentPathIndex = 0;
                targetLocation = paths[currentPathIndex].position;
            }

            // Vector3 targetLocation = lastPositionClicked;
            targetLocation.z = myLocation.z;

            Vector3 vectorToTarget = targetLocation - myLocation;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

            Quaternion targetRotation =
                Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime, Space.Self);
        }

        // if (moving && (Vector2) transform.position != lastPositionClicked)
        // {
        //     float step = movementSpeed * Time.deltaTime;
        //     transform.position = Vector2.MoveTowards(transform.position, lastPositionClicked, step);
        // }
        // else
        // {
        //     moving = false;
        // }


        // if (moving)
        // {
        // Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, lastPositionClicked);
        // transform.rotation =
        //     Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // }
    }
}