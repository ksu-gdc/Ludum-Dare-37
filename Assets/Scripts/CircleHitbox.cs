using UnityEngine;
using System.Collections;

public class CircleHitbox: MonoBehaviour {
    public GameObject HitBoxPrefab;
    public float Radius;
    public Vector2 Offset;
    private GameObject HitBox;
    private CircleCollider2D circle;

    void Start()
    {
        HitBox = Instantiate<GameObject>(HitBoxPrefab);
        HitBox.transform.SetParent(gameObject.transform, false);
        circle = HitBox.GetComponent<CircleCollider2D>();
        if(circle == null) 
        {
            Destroy(HitBox);
            throw new System.Exception("Could not locate the circle collider");
        }
        UpdateSize();
    }

    void OnValidate()
    {
        if(HitBox != null)
        {
            UpdateSize();
        }
    }

    void UpdateSize()
    {
        circle.radius = Radius;
        circle.offset = Offset;
    }
}
