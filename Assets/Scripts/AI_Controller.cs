using UnityEngine;
using System.Collections;

public class AI_Controller : MonoBehaviour
{
    public Transform[] waypoint;        // The amount of Waypoint you want
    public float patrolSpeed = 0.3f;       // The walking speed between Waypoints
    public bool loop = true;       // Do you want to keep repeating the Waypoints
    public float pauseDuration = 0;   // How long to pause at a Waypoint
    public float scaleMod = .01f;

    private float curTime;
    private int currentWaypoint = 0;
    private CharacterController character;
    private Animator anim;

    void Start()
    {
        character = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetBool("Walking", false);
    }

    void Update()
    {
        if (currentWaypoint < waypoint.Length)
        {
            patrol();
        }
        else
        {
            if (loop)
            {
                currentWaypoint = 0;
            }
        }
    }

    void patrol()
    {
        Vector3 target = waypoint[currentWaypoint].position;
        Vector3 moveDirection = target - transform.position;
        if (moveDirection.magnitude < scaleMod)
        {
            if (curTime == 0)
            {
                curTime = Time.time; // Pause over the Waypoint
                anim.SetBool("Walking", false);
            }
            if ((Time.time - curTime) >= pauseDuration)
            {
                currentWaypoint++;
                curTime = 0;
                anim.SetBool("Walking", true);
            }
        }
        else
        {
            Vector2 moveRot = character.velocity;
            if (moveRot != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveRot.y, moveRot.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            }
            character.Move(moveDirection.normalized * patrolSpeed * Time.deltaTime);
        }
    }
}
