using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionObject : MonoBehaviour
{
    [SerializeField] private float limitDistance;
    [SerializeField] private float distanceToMove;
    private Transform playerTransform;
    private float horizontalDistance;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        horizontalDistance = playerTransform.position.x - transform.position.x;

        if (horizontalDistance > limitDistance)
        {
            Vector2 newPosition = transform.position;
            newPosition.x = transform.position.x + distanceToMove;
            transform.position = newPosition;
        }
    }
}
