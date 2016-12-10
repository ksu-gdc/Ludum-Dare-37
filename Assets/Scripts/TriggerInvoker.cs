using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class TriggerInvoker: MonoBehaviour {
    public UnityEvent OnEnter, OnStay, OnExit;

    private int collision_count = 0;

    private void AddCollision()
    {
        collision_count++;
    }

    private void RemoveCollision()
    {
        collision_count = Mathf.Max(0, collision_count - 1);
    }

    public bool IsColliding()
    {
        return collision_count > 0;
    }

    public bool EnteredCollision()
    {
        return collision_count == 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        AddCollision();
        if (EnteredCollision())
            OnCompoundTriggerEnter(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (IsColliding())
            OnCompoundTriggerStay(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        RemoveCollision();
        if (!IsColliding())
            OnCompoundTriggerExit(other);
    }

    void OnCompoundTriggerEnter(Collider2D other)
    {
        Debug.Log("Compound Trigger Enter");
        OnEnter.Invoke();
    }

    void OnCompoundTriggerStay(Collider2D other)
    {
        OnStay.Invoke();
    }

    void OnCompoundTriggerExit(Collider2D other)
    {
        Debug.Log("Compound Trigger Exit");
        OnExit.Invoke();
    }
}
