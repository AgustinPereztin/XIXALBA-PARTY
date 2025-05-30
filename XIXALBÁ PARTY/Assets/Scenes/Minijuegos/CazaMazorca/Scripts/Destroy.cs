using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private float timer = 3f;

    public void SetTimer(float time)
    {
        timer = time;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}