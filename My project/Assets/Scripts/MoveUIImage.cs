using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveUIImage : MonoBehaviour
{
    public Image imageToMove;
    public float startDelay = 10.0f;
    public float movementDuration = 10.0f;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public Vector2 endPosition = new Vector2(0.0f, 100.0f);

    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = imageToMove.rectTransform.anchoredPosition;

        StartCoroutine(MoveImageAfterDelay());
    }

    IEnumerator MoveImageAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);

        float timer = 0.0f;
        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / movementDuration;

            Vector2 targetPosition = Vector2.Lerp(startPosition, endPosition, normalizedTime);
            imageToMove.rectTransform.anchoredPosition = targetPosition;

            yield return null;
        }
    }
}
