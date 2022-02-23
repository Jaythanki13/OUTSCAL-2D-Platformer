using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Animator animator;
    //[SerializeField] ScoreController scoreController;
    //public GameObject gameOver;
    public float jump;

    private bool isCrouch = false;
    private bool isDead = false;

    [SerializeField]private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask platformLayerMask;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

   /* public void PickUpKey()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Pickup);
        scoreController.IncreaseScore(10);
    }*/

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");

        if (!isDead)
        {
            PlayerMovement(horizontal, vertical);
            Jump(horizontal, vertical);
            Crouch();
        }
    }

    private void Jump(float horizontal, float vertical)
    {
        animator.SetBool("Jumping", (vertical > 0 && IsGrounded() && !isCrouch));

        if (vertical > 0 && IsGrounded() && !isCrouch)
        {
            rb.velocity = Vector2.up * jump;
        }
    }

    private void PlayerMovement(float horizontal, float vertical)
    {
        if (!isCrouch)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            Vector3 scale = transform.localScale;
            scale.x = horizontal < 0 ? -1f * Mathf.Abs(scale.x) : horizontal > 0 ? Mathf.Abs(scale.x) : scale.x;
            transform.localScale = scale;

            Vector3 position = transform.position;
            position.x += horizontal * speed * Time.deltaTime;
            transform.position = position;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.3f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        return raycastHit.collider != null;
    }

    private void Crouch()
    {
        animator.SetBool("Crouch", (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)));
        isCrouch = (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl));
    }
}
