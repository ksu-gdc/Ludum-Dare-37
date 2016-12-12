using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public float speed, rotSpeed;
    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector2 move;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Walking", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up")) other.gameObject.SetActive(false);
        else if (other.gameObject.CompareTag("EntryDoorTrigger")) SceneManager.LoadScene("Neighborhood");
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime;
        if (moveVertical > 0) transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (moveVertical < 0) transform.Translate(-Vector3.up * speed * Time.deltaTime);
        if (moveHorizontal > 0) transform.Rotate(Vector3.forward, -rotSpeed);
        if (moveHorizontal < 0) transform.Rotate(Vector3.forward, rotSpeed);
        if (moveVertical != 0) anim.SetBool("Walking", true);
        else anim.SetBool("Walking", false);
    }
}
