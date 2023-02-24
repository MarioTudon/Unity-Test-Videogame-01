using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private bool doubleJump;

    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private float runSpeed = 10f;
    [HideInInspector] public bool runing = false;
    [SerializeField] private float timeToSlide = 3f;
    [SerializeField] private AudioSource slideAudio;
    private float slidingTime = 0f;
    private bool done = false;
    [SerializeField] private Rigidbody2D rB2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkerRadius = 0.05f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject footprintSnow;

    [SerializeField] private GameObject initialExplosion;
    [SerializeField] private GameObject[] enemiesSpawners;

    [SerializeField] private GameObject losePanel;
    [HideInInspector] public bool youLose = false;
    [SerializeField] private AudioSource hurtAudio;
    [SerializeField] private MaskTransition maskTransition;
    [SerializeField] private HighScore highScore;
    [SerializeField] private HighScore highScoreSnow;
    private float loseCounter = 0;

    [SerializeField] private UIAnimation uIAnimation;

    private void Awake()
    {
        loseCounter = 0;
        losePanel.SetActive(false);
        initialExplosion.SetActive(false);
        foreach (GameObject enemySpawner in enemiesSpawners)
        {
            enemySpawner.SetActive(false);
        }
    }

    private void Update()
    {
        Slide();

        if (youLose)
        {
            loseCounter += Time.deltaTime;
            Lose();
            if (Input.GetMouseButtonDown(0) && loseCounter > 0.5f)
            {
                maskTransition.ResetGame();
            }
        }
    }

    private void FixedUpdate()
    {
        Run();
        Jump();
    }

    private void Jump()
    {
        if (!runing || youLose) return;

        /*if (IsGrounded() && !Input.GetKey(KeyCode.Space))
        {
            doubleJump = false;
        }*/

        if ((IsGrounded()/* || doubleJump*/) && Input.GetMouseButtonDown(0))
        {
            rB2D.velocity = new Vector2(rB2D.velocity.x, jumpingPower);
            jumpAudio.Play();
            /*doubleJump = !doubleJump;*/
        }

        if (Input.GetMouseButtonUp(0) && rB2D.velocity.y > 0f)
        {
            rB2D.velocity = new Vector2(rB2D.velocity.x, rB2D.velocity.y * 0.5f);
        }

        playerAnimator.SetBool("IsGrounded", IsGrounded());
        footprintSnow.SetActive(IsGrounded());
    }

    private void Run()
    {
        if (youLose) return;

        if (runing)
        {
            rB2D.velocity = new Vector2(runSpeed, rB2D.velocity.y);
        }
        if (Input.GetMouseButton(0) && !IsMouseOverUIWithIgnores() && !runing)
        {
            uIAnimation.UIStartGame();
            runing = true;
            initialExplosion.SetActive(true);
            foreach (GameObject enemySpawner in enemiesSpawners)
            {
                enemySpawner.SetActive(true);
            }
        }
    }

    private void Slide()
    {
        if (!runing || youLose) return;

        if (IsGrounded() && Input.GetMouseButton(1) && slidingTime < timeToSlide)
        {
            slidingTime += Time.deltaTime;
            playerAnimator.SetBool("Sliding", true);
            if (!done)
            {
                done = true;
                slideAudio.Play();
            }
        }
        else
        {
            playerAnimator.SetBool("Sliding", false);
        }

        if (Input.GetMouseButtonUp(1))
        {
            slidingTime = 0f;
            done = false;
            slideAudio.Stop();
        }
    }

    private void Lose()
    {
        rB2D.velocity -= rB2D.velocity * Time.deltaTime * 0.1f;
        highScore.UpdateHighScore();
        highScoreSnow.UpdateHighScore();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkerRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkerRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" || youLose) return;

        playerAnimator.SetTrigger("Lose");
        losePanel.SetActive(true);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.layer != 7)
            {
                Destroy(enemy);
            }
        }

            footprintSnow.SetActive(false);

            foreach (GameObject enemySpawner in enemiesSpawners)
            {
                enemySpawner.SetActive(false);
            }
        
        youLose = true;
        done = true;
        slideAudio.Stop();
        jumpAudio.Stop();
        hurtAudio.Play();
    }

    private bool IsMouseOverUIWithIgnores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsLists = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsLists);
        for (int i = 0; i < raycastResultsLists.Count; i++)
        {
            if(raycastResultsLists[i].gameObject.tag == "Mouse Click Through")
            {
                raycastResultsLists.RemoveAt(i);
                i--;
            }
        }

        return raycastResultsLists.Count > 0;
    }
}


