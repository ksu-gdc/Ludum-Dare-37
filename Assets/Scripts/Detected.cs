using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Detected : MonoBehaviour {

    public UnityEvent OnEnter, OnStay, OnExit;

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
