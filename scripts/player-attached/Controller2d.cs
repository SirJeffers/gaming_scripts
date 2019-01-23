using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( BoxCollider2D))]

public class Controller2d : MonoBehaviour {

    public LayerMask collisionMask;

    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public collisionInfo collisions;


    float horizontalRaySpacing;
    float verticalRaySpacing;

    private BoxCollider2D box;
    private RaycastOrigins raycastOrigins;

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, bottomeLeft, bottomRight;
    }

    public struct collisionInfo
    {
        public bool above, below, left, right;
        public void Reset()
        {
            above = below = left = right = false;
        }
    }

    // Use this for initialization
    void Start () {
        box = GetComponent<BoxCollider2D>();
        CalculateRaycastSpacing();

    }

    //// Update is called once per frame
    //void Update () {


    //}



    public void Move(Vector3 velocity)
    {
        collisions.Reset();
        UpdateRaycaseOrigins();

        if(velocity.x != 0)
            HorizontalCollisions(ref velocity);
        if(velocity.y != 0)
            VerticalCollisions(ref velocity);



        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX= Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomeLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.green);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = (directionX == -1);
                collisions.right = (directionX == 1);
            }

        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomeLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.green);

            if(hit)
            {
                velocity.y = (hit.distance- skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }

        }
    }



    void UpdateRaycaseOrigins()
    {
        Bounds bounds = box.bounds;
        bounds.Expand(skinWidth * -2);
        raycastOrigins.bottomeLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

    }

    void CalculateRaycastSpacing()
    {
        Bounds bounds = box.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, 20);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, 20);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
