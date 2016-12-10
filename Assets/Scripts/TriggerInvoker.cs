using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TriggerInvoker: MonoBehaviour {

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
}
