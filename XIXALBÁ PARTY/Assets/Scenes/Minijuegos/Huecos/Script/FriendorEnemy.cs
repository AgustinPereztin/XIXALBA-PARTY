using UnityEngine;

public class FriendOrEnemy : MonoBehaviour
{
    public bool isFriend;
    public HuecoManager manager;

    private void Awake()
    {
        // Intentar asignar automáticamente si no fue seteado desde HuecoManager
        if (manager == null)
        {
            manager = FindObjectOfType<HuecoManager>();
            if (manager == null)
                Debug.LogWarning("⚠️ HuecoManager no encontrado en escena.");
        }
    }

    private void OnMouseDown()
    {
        if (manager == null)
        {
            Debug.LogError("❌ No se puede interactuar: manager es NULL.");
            return;
        }

        if (isFriend)
        {
            Debug.Log("👋 Clickeaste un amigo.");
            manager.OnFriendClicked();
        }
        else
        {
            Debug.Log("💥 Clickeaste un enemigo.");
            manager.OnEnemyClicked();
        }

        Destroy(gameObject);
    }
}
