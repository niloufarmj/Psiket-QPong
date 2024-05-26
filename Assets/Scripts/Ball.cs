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

    public Vector2 currentVelocity;

    public bool pausedLocked = false;

    IEnumerator DelayedProcess(int direction)
    {
        GameManager.instance.cameraShake.StartShake(0.1f, 0.5f);
        StopBall();

        float y_position = Random.Range(-1.8f, 3.3f);
        float x_position = 6.1f * direction;
        transform.position = new Vector2(x_position, y_position);

        pausedLocked = true;
        
        // Wait for 1 seconds
        yield return new WaitForSeconds(1f);

        pausedLocked = false;

        InitBall(direction);
    }

    public void StopBall()
    {
        rg.velocity = new Vector2(0, 0);
        for (int i = 0; i < ballObjects.Length; i++)
            ballObjects[i].SetActive(false);
    }

    void Start()
    {
        speed = GameManager.instance.data.ballSpeed;
        InitBall(1);
    }

    public void Update()
    {
        

        if (GameManager.instance.reset)
        {
            ResetGame();
            GameManager.instance.reset = false;
        }
    }

    public void PauseBall()
    {
        currentVelocity = rg.velocity;
        StopBall();
    }
    public void ResumeBall()
    {
        rg.velocity = currentVelocity;
        for (int i = 0; i < ballObjects.Length; i++)
            ballObjects[i].SetActive(true);
    }

    public void ResetGame()
    {
        float y_position = Random.Range(-1.8f, 3.3f);
        float x_position = 3.44f * 1;
        transform.position = new Vector2(x_position, y_position);

        leftScore = 0;
        rightScore = 0;

        textR.text = rightScore.ToString();
        textL.text = leftScore.ToString();

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
            if (GameManager.instance.data.soundOn)
                GameManager.instance.outSound.Play();
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
            if (GameManager.instance.data.soundOn)
                GameManager.instance.outSound.Play();
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
            if (GameManager.instance.data.soundOn)
                GameManager.instance.hitSound.Play();
            EmitParticle(8);
            GameManager.instance.cameraShake.StartShake(0.035f, 0.035f);
        }

        if (collision.gameObject.tag == "Paddle")
        {
            if (GameManager.instance.data.soundOn)
                GameManager.instance.hitSound.Play();
            EmitParticle(16);
            GameManager.instance.cameraShake.StartShake(0.025f, 0.025f);
        }
    }

    private void EmitParticle(int amount)
    {
        collisionParticleSystem.Emit(amount);
    }
}