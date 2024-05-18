using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform ballTransform;
    public float speed;
    public Rigidbody2D ballRg;
    public Rigidbody2D rg;

    // Update is called once per frame
    void Update()
    {
        if (ballTransform.position.x < 0)
        {
            if (Mathf.Abs(ballTransform.position.y - transform.position.y) < 0.18)
                rg.velocity = new Vector2(0f, 0f);
            if (ballTransform.position.y - transform.position.y > 0.1 && transform.position.y < 3.4f && ballRg.velocity.x < 0)
                rg.velocity = new Vector2(0f, speed);
            else if (transform.position.y - ballTransform.position.y > 0.1 && transform.position.y > -1.7f && ballRg.velocity.x < 0)
                rg.velocity = new Vector2(0f, -speed);

        }
            

    }
}
