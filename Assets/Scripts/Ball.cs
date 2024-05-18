using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rg;
    public float speed;
    public ParticleSystem collisionParticleSystem;
    /*private int lPunkte, rPunkte;
    public TextMeshProUGUI textR, textL;*/
    void Start()
    {
        Launch();
    }

    void Launch()
    {
        transform.position = new Vector2(0, 1.5f);
        float x_direction = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        float y_direction = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        rg.velocity = new Vector2(x_direction * speed, y_direction * speed);
        EmitParticle(32);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Left")
        {
           /* rPunkte++;
            textR.text = rPunkte.ToString();*/
            Launch();
        }
        else if (collision.gameObject.tag == "Right")
        {
            /*lPunkte++;
            textL.text = lPunkte.ToString();*/
            Launch();
        }

        if (collision.gameObject.tag == "Wall")
        {
            EmitParticle(8);
            GameManager.instance.cameraShake.StartShake(0.035f, 0.035f);
        }

        if (collision.gameObject.tag == "Paddle")
        {
            EmitParticle(16);
            GameManager.instance.cameraShake.StartShake(0.025f, 0.025f);
        }
    }

    private void EmitParticle(int amount)
    {
        collisionParticleSystem.Emit(amount);
    }
}