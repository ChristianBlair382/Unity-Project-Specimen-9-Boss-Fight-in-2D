using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float 
        minX,
        maxX,
        minY,
        maxY,
        currentX,
        currentY,
        cameraShakeThreshold = 2f;

    private Vector3 shakeOffset;
    private Coroutine shakeCoroutine;

    void LateUpdate()
    {
        if (target == null) return;

        currentX = Mathf.Clamp(target.position.x, minX, maxX);
        currentY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(currentX, currentY, -10f) + shakeOffset;
    }

    public void StartCameraShake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(CameraShake(duration, magnitude));
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            shakeOffset = Random.insideUnitCircle * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
        shakeCoroutine = null;
    }
}
