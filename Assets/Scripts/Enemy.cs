using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Action<Enemy> deactivateAction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = transform.childCount != 0 ? GetComponentInChildren<SpriteRenderer>() : GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(transform.position.x < player.position.x - 60f && !spriteRenderer.isVisible)
        {
            deactivateAction(this);
        }
    }

    public void DeactivateEnemy(Action<Enemy> deactivateActionParameter)
    {
        deactivateAction = deactivateActionParameter;
    }
}
