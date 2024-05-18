using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    public void StartShake(float offset, float duration)
    {
        StopShake();
        StartCoroutine(ShakeSequence(offset, duration));
    }

    public void StopShake()
    {
        StopAllCoroutines();
        transform.localPosition = initialPosition;
    }

    private void DoShake(float maxOffset)
    {
        float xOffset = Random.Range(-maxOffset, maxOffset);

        while (xOffset == 0)
        {
            xOffset = Random.Range(-maxOffset, maxOffset);
        }
        float yOffset = Random.Range(-maxOffset, maxOffset);
        while (yOffset == 0)
        {
            yOffset = Random.Range(-maxOffset, maxOffset);
        }
        transform.localPosition = initialPosition + new Vector3(xOffset, yOffset, 0f);
    }

    private IEnumerator ShakeSequence(float offset, float duration)
    {
        float durationPassed = 0f;

        while (durationPassed < duration)
        {
            DoShake(offset);
            durationPassed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialPosition;
    }
}
