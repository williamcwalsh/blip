using UnityEngine;

public class CameraRigPersist : MonoBehaviour
{
    static CameraRigPersist Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}