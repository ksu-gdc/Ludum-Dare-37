using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Detected : MonoBehaviour {

    public UnityEvent OnEnter, OnStay, OnExit;

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Trigger Collision entered");
        OnEnter.Invoke();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        OnStay.Invoke();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        OnExit.Invoke();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Collision entered");
        OnEnter.Invoke();
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        OnStay.Invoke();
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        OnExit.Invoke();
    }
}
