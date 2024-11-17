using UnityEngine;
using UnityEngine.UI;

public class Patient : MonoBehaviour
{
    private int spawnIndex;
    private float angerTimer;
    private System.Action<int> onDespawnCallback;

    [SerializeField] private Image angerTimerBar;

    public void Initialize(int index, float timer, System.Action<int> callback)
    {
        spawnIndex = index;
        angerTimer = timer;
        onDespawnCallback = callback;

        UpdateAngerIndicator();
    }

    void Update()
    {
        angerTimer -= Time.deltaTime;
        UpdateAngerIndicator();

        if (angerTimer <= 0f)
        {
            Despawn();
        }
    }

    private void UpdateAngerIndicator()
    {
        if (angerTimerBar != null)
        {
            angerTimerBar.fillAmount = Mathf.Clamp01(angerTimer / angerTimerBar.fillAmount);
        }
    }

    private void Despawn()
    {
        onDespawnCallback?.Invoke(spawnIndex);
        Destroy(gameObject);
    }
}
