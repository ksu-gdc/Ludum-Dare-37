using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    public float speed;
    public const float stopAnimation = 0.5f;
    private Rigidbody2D rb2d;
    private Animator animator;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveHorizontal, moveVertical);
        if ((move.x > stopAnimation || move.x < stopAnimation) || (move.y > stopAnimation || move.x < stopAnimation))
        {

        }
       // else animator.Blend("player_character_0",.5f);
        rb2d.AddForce(move * speed);
        Vector2 moveDirection = rb2d.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
