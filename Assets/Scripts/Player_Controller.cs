using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        var moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var moveY = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveY);	
	}
}
