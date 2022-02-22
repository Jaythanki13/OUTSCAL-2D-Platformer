using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]float speed = 20f;
    [SerializeField]Animator animator;


    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Collision:- " + collision.gameObject.name);
    // }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", horizontal);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 Scale = transform.localScale;
        if (horizontal < 0)
        {
            Scale.x = -1 * Mathf.Abs(Scale.x);
            Debug.Log("Flipped....");
        }

        else if (horizontal > 0)
        {
            Scale.x = Mathf.Abs(Scale.x);
        }

        else if (horizontal == 0)
        {
            
        }

        transform.localScale = Scale;

        
        float vertical = Input.GetAxisRaw("Vertical");
        animator.SetBool("Jumping", true);


        //PlayerMovement(horizontal);
    }


}
