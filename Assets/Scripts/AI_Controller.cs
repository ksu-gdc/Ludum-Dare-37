using UnityEngine;
using System.Collections;

public class AI_Controller : MonoBehaviour {

    public Transform[] points;
    private int nextPoint = 0;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
	}

    void GoToNextPoint()
    {
        if (points.Length == 0) return;
        agent.destination = points[nextPoint].position;
        nextPoint = (nextPoint + 1) % points.Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (agent.remainingDistance < 0.5f) GoToNextPoint();
	}
}
