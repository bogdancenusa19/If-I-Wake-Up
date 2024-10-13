using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject lever;

    [SerializeField] private Vector2 leverActivatedPosition;
    [SerializeField] private float targetWallY;
    [SerializeField] private float wallMoveSpeed = 0.005f; //Check

    private bool canActivateWall = false;
    private bool wallIsMoving = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spirit"))
        {
            canActivateWall = true;
            MoveLever();
        }
    }

    private void Update()
    {
        if (canActivateWall && !wallIsMoving)
        {
            wallIsMoving = true;
            StartCoroutine(MoveWallDown());
        }
    }

    private IEnumerator MoveWallDown()
    {
        while (wall.transform.position.y > targetWallY)
        {
            wall.transform.position = new Vector2(
                wall.transform.position.x, 
                wall.transform.position.y - wallMoveSpeed);
            yield return null;
        }

        wallIsMoving = false;
    }

    private void MoveLever()
    {
        lever.transform.position = leverActivatedPosition;
    }
}
