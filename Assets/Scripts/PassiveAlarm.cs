using UnityEngine;
using System.Collections;

public class PassiveAlarm : MonoBehaviour {
    public SpriteRenderer Renderer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeColor(Color newColor)
    {
        Renderer.color = newColor;
    }


    public void ChangeRed()
    {
        ChangeColor(Color.red);
    }

    public void ChangeWhite()
    {
        ChangeColor(Color.white);
    }

    public void ChangeBlack()
    {
        ChangeColor(Color.black);
    }
}
