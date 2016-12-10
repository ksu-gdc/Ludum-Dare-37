using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward;
        Vector3 headingAngle = Quaternion.LookRotation(forward).eulerAngles;
        Vector2 move = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(move * speed);
        transform.Rotate; 
    }
}
