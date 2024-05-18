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

    public GameObject[] ballObjects;

    IEnumerator DelayedProcess(int direction)
    {
        GameManager.instance.cameraShake.StartShake(0.1f, 0.5f);
        rg.velocity = new Vector2(0, 0);
        for (int i = 0; i < ballObjects.Length; i++)
            ballObjects[i].SetActive(false);

        float y_position = Random.Range(-1.8f, 3.3f);
        float x_position = 3.44f * direction;
        transform.position = new Vector2(x_position, y_position);

        
        // Wait for 1 seconds
        yield return new WaitForSeconds(1f);

        InitBall(direction);
    }

    void Start()
    {
        InitBall(1);
    }

    void Launch(int direction)
    {
        // Start the coroutine
        StartCoroutine(DelayedProcess(direction)); 
    }

    void InitBall(int direction)
    {
        for (int i = 0; i < ballObjects.Length; i++)
            ballObjects[i].SetActive(true);

        textL.gameObject.SetActive(true);
        textR.gameObject.SetActive(true);

        textLUp.gameObject.SetActive(false);
        textRUp.gameObject.SetActive(false);

        loseShader.SetActive(false);
        winShader.SetActive(false);

        float x_direction = -1 * direction;
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
            Launch(-1);
        }
        else if (collision.gameObject.tag == "Right")
        {
            leftScore++;
            textL.text = leftScore.ToString();
            textLUp.text = leftScore.ToString();
            textL.gameObject.SetActive(false);
            textLUp.gameObject.SetActive(true);
            loseShader.SetActive(true);
            Launch(1);
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