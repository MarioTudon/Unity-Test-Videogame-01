using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private float offset;

    private void Start()
    {
        offset = transform.position.x - playerTransform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + offset, transform.position.y, transform.position.z);
    }
}
