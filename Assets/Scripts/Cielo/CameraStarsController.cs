using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStarsController : MonoBehaviour
{
    private Camera camera;
    private bool isMoving;
    private float distanceX;
    private float targetPositionX;
    private DirectionMoveSkyStar _directionMoveSkyStar;
    private Vector3 directionVector3;
    public float speed;
    public float distance;
    public GeneradorCoordenadas generadorCoordenadas;
    public StarsBackgroundController starsBackgroundController;

    private void Start()
    {
        camera = GetComponent<Camera>();
        starsBackgroundController = GetComponent<StarsBackgroundController>();
        distanceX = 18.4f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && isMoving == false)
        {
            print("Right");
            isMoving = true;
            targetPositionX = camera.transform.position.x + distanceX;
            _directionMoveSkyStar = DirectionMoveSkyStar.Right;
            directionVector3 = Vector3.right;
            distance = Math.Abs(camera.transform.position.x - targetPositionX);
            starsBackgroundController.ChangePositionSky(_directionMoveSkyStar);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && isMoving == false)
        {
            print("Left");
            isMoving = true;
            targetPositionX = camera.transform.position.x - distanceX;
            _directionMoveSkyStar = DirectionMoveSkyStar.Left;
            directionVector3 = Vector3.left;
            distance = Math.Abs(camera.transform.position.x - targetPositionX);
            starsBackgroundController.ChangePositionSky(_directionMoveSkyStar);
        }

        if (isMoving && IsBeforeLimit())
        {
            if (distance > 1f)
                distance = (float)Math.Ceiling(
                    Math.Round(Math.Abs(camera.transform.position.x - targetPositionX) * 0.5f, 2));
            camera.transform.Translate(directionVector3 * speed * distance * Time.deltaTime);
        }
        else if (isMoving && !IsBeforeLimit())
        {
            isMoving = false;
            targetPositionX = 0;
            distance = 0;
            directionVector3 = Vector3.zero;
            generadorCoordenadas.Generate();
        }
    }

    private bool IsBeforeLimit()
    {
        var position = camera.transform.position;
        return _directionMoveSkyStar switch
        {
            DirectionMoveSkyStar.Right => position.x < targetPositionX,
            DirectionMoveSkyStar.Left => position.x > targetPositionX,
            _ => false
        };
    }
}

public enum DirectionMoveSkyStar
{
    Left,
    Right
}