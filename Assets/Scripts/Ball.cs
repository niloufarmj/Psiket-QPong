using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rg;
    public float speed;
    public ParticleSystem collisionParticleSystem;
    private int leftScore, rightScore;
    public TextMeshProUGUI textR, textL;
    public TextMeshProUGUI textRUp, textLUp;

    public GameObject loseShader, winShader;

    IEnumerator DelayedProcess()
    {
        GameManager.instance.cameraShake.StartShake(0.1f, 0.5f);
        transform.position = new Vector2(0.049f, 0.822f);
        rg.velocity = new Vector2(0, 0);
        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        InitBall();
    }

    void Start()
    {
        InitBall();
    }

    void Launch()
    {
        // Start the coroutine
        StartCoroutine(DelayedProcess()); 
    }

    void InitBall()
    {
        textL.gameObject.SetActive(true);
        textR.gameObject.SetActive(true);

        textLUp.gameObject.SetActive(false);
        textRUp.gameObject.SetActive(false);

        loseShader.SetActive(false);
        winShader.SetActive(false);

        float x_direction = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        float y_direction = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        rg.velocity = new Vector2(x_direction * speed, y_direction * speed);
        EmitParticle(32);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Left")
        {
            rightScore++;
            textR.text = rightScore.ToString();
            textRUp.text = rightScore.ToString();
            textR.gameObject.SetActive(false);
            textRUp.gameObject.SetActive(true);
            winShader.SetActive(true);
            Launch();
        }
        else if (collision.gameObject.tag == "Right")
        {
            leftScore++;
            textL.text = leftScore.ToString();
            textLUp.text = leftScore.ToString();
            textL.gameObject.SetActive(false);
            textLUp.gameObject.SetActive(true);
            loseShader.SetActive(true);
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