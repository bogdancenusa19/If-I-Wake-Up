using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIControl : MonoBehaviour
{
    [SerializeField] private GameObject spirit;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 2.0f;  
    [SerializeField] private float minDistance = 1.5f; 
    [SerializeField] private float maxDistance = 3.0f; 
    private Vector3 targetPosition; 

    private bool isUnderControl = false;
    
    void Start()
    {
        StartCoroutine(CheckPlayerPosition());
    }

    private void Update()
    {
        if (!isUnderControl)
        {
            KeepFacingUpdated();
        }
    }

    private void KeepFacingUpdated()
    {
        if (spirit.transform.position.x > player.transform.position.x)
        {
            animator.transform.localScale = new Vector3(1.0f, 1.0f, -1);
        }
        else
        {
            animator.transform.localScale = new Vector3(1.0f, 1.0f, 1);
        }
    }

    IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(spirit.transform.position, player.transform.position);
            
            float desiredDistance = Random.Range(minDistance, maxDistance);
            
            if (distanceToPlayer > desiredDistance)
            {
                Vector3 direction = (player.transform.position - spirit.transform.position).normalized;
                
                targetPosition = player.transform.position - direction * desiredDistance;

                if (!isUnderControl)
                {
                    spirit.transform.position = Vector3.MoveTowards(spirit.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
            }
            yield return null;
        }
    }

    public void SetIsUnderControl(bool state)
    {
        isUnderControl = state;
    }
    
}