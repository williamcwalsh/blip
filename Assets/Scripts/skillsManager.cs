using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance { get; private set; }
    public PlayerSkills Skills { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Skills = new PlayerSkills();
    }
}