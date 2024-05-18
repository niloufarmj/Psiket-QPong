using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Shake cameraShake;

    private void Start()
    {
        instance = this;
    }
}
