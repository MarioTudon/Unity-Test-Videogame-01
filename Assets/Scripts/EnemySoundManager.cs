using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    private Enemy enemy;
    private bool done = false;
    [SerializeField] private AudioSource enemyAudio;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.spriteRenderer.isVisible && !done)
        {
            done = true;
            enemyAudio.Play();
        }
        else if (!enemy.spriteRenderer.isVisible)
        {
            enemyAudio.Stop();
        }
    }

    private void OnDisable()
    {
        done = false;
    }
}
