using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    //private Quaternion prevRot;
    private float prevAngle;
    Vector3 forward;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        forward = transform.forward;
    }

    void Update()
    {
        Vector2 moveDirection = rb2d.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(move * speed);
    }
}
