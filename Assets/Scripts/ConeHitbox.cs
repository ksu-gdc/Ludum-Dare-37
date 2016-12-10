using UnityEngine;
using System.Collections;

public class ConeHitbox: MonoBehaviour {
    public GameObject HitBoxPrefab;
    public float ShortWidth, LongWidth, Length;
    private GameObject HitBox;
    private CircleCollider2D bulb_collider;
    private PolygonCollider2D cone;

    void Start()
    {
        HitBox = Instantiate<GameObject>(HitBoxPrefab);
        HitBox.transform.SetParent(gameObject.transform, false);
        bulb_collider = HitBox.GetComponent<CircleCollider2D>();
        cone = HitBox.GetComponent<PolygonCollider2D>();
        if(bulb_collider == null) 
        {
            Destroy(HitBox);
            throw new System.Exception("Could not locate the circle collider");
        }
        if(cone == null) 
        {
            Destroy(HitBox);
            throw new System.Exception("Could not locate the polygon collider");
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
        bulb_collider.offset = Vector2.right * Length;
        bulb_collider.radius = LongWidth / 2;

        Vector2[] points = cone.GetPath(0);
        points[0] = Vector2.up * (ShortWidth / 2);
        points[1] = new Vector2(Length, LongWidth / 2);
        points[2] = new Vector2(Length, -1 * (LongWidth/2));
        points[3] = cone.points[0] * -1;
        cone.SetPath(0, points);
    }
}
