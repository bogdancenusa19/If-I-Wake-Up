using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public GameObject character;  // Referința la caracterul AI
    public GameObject player;     // Referința la player
    public float moveSpeed = 2.0f;  // Viteza de deplasare a caracterului AI
    public float minDistance = 1.5f; // Distanța minimă între AI și player
    public float maxDistance = 3.0f; // Distanța maximă între AI și player
    private Vector3 targetPosition;  // Poziția țintă a caracterului
    
    void Start()
    {
        StartCoroutine(CheckPlayerPosition());
    }

    IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            // Calculăm distanța actuală între AI și player
            float distanceToPlayer = Vector3.Distance(character.transform.position, player.transform.position);

            // Calculăm o distanță random de păstrat între minDistance și maxDistance
            float desiredDistance = Random.Range(minDistance, maxDistance);

            // Dacă AI-ul este prea aproape de player sau prea departe, se va apropia/lărgi distanța
            if (distanceToPlayer > desiredDistance)
            {
                // Calculăm direcția către player
                Vector3 direction = (player.transform.position - character.transform.position).normalized;

                // Setăm ținta la distanța dorită de player
                targetPosition = player.transform.position - direction * desiredDistance;

                // Mișcăm caracterul în direcția țintei, cu o viteză mai lentă
                character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            // Așteptăm un frame înainte de a face următoarea verificare
            yield return null;
        }
    }

    public void SwapCharacters()
    {   
        GameObject temp = player; 
        player = character; 
        character = temp;
    }
}