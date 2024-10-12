using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject lever;

    [SerializeField] private Vector2 leverActivatedPosition;
    [SerializeField] private float wallY;

    private bool canActivateWall = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spirit"))
        {
            canActivateWall = true;
        }
    }

    private void Update()
    {
        if (canActivateWall)
        {
            ActivateWall();
        }
    }

    private void ActivateWall()
    {
        if (wall.transform.position.y >= wallY)
        {
            wall.transform.position = new Vector2(0, wall.transform.position.y - 0.025f);
        }
    }
}
