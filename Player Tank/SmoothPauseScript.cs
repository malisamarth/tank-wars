using System.Collections;
using UnityEngine;

public class SmoothPauseScript : MonoBehaviour {
    public static SmoothPauseScript Instance { get; private set; }

    [Header("Pause Settings")]
    [SerializeField] private float pauseDuration = 0.8f;

    private Coroutine pauseRoutine;

    private void Awake() {
        if (Instance != null) {
            //Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SmoothPause() {
        if (pauseRoutine != null)
            StopCoroutine(pauseRoutine);

        pauseRoutine = StartCoroutine(SmoothPauseRoutine());
    }

    public void Resume() {
        Time.timeScale = 1f;
    }

    private IEnumerator SmoothPauseRoutine() {
        float elapsed = 0f;
        float startScale = Time.timeScale;

        while (elapsed < pauseDuration) {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(startScale, 0f, elapsed / pauseDuration);
            yield return null;
        }

        Time.timeScale = 0f;
    }


}
