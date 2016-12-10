using UnityEngine;
using System.Collections;

public class ConeDetector : MonoBehaviour {
    public GameObject HitBoxPrefab;
    public float ShortWidth, LongWidth, Length;
    private GameObject HitBox;
    private CircleCollider2D bulb_collider;
    private PolygonCollider2D cone;

    void Awake()
    {
        HitBox = Instantiate<GameObject>(HitBoxPrefab);
        bulb_collider = HitBox.GetComponent<CircleCollider2D>();
        cone = HitBox.GetComponent<PolygonCollider2D>();
        if(bulb_collider == null) 
        {
            throw new System.Exception("Could not locate the circle collider");
        }
        if(cone == null) 
        {
            throw new System.Exception("Could not locate the polygon collider");
        }
        Destroy(HitBox);
    }

    void OnValidate()
    {
        bulb_collider.offset += Vector2.right * Length;
        bulb_collider.radius = LongWidth / 2;
        cone.points[0] = Vector2.up * (ShortWidth / 2);
        cone.points[1] = new Vector2(Length, LongWidth / 2);
        cone.points[2] = new Vector2(Length, -1 * cone.points[1].y);
        cone.points[3] = cone.points[1] * -1;
    }
}
