using System.Collections;
using UnityEngine;

public class LogoMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;  // 움직일 거리
    public float moveSpeed = 1.0f;     // 움직이는 속도
    public float bounceHeight = 0.1f;  // 튕기는 높이

    private Vector3 originalPosition;  // 초기 위치

    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveLogo());
    }

    IEnumerator MoveLogo()
    {
        while (true)
        {
            float t = 0f;

            // 아래로 이동
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;

                float yOffset = Mathf.Sin(t * Mathf.PI) * bounceHeight;
                transform.position = originalPosition + new Vector3(0f, yOffset, 0f);

                yield return null;
            }

            t = 0f;

            // 위로 이동
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;

                float yOffset = Mathf.Sin(t * Mathf.PI) * bounceHeight;
                transform.position = originalPosition - new Vector3(0f, yOffset, 0f);

                yield return null;
            }
        }
    }
}
