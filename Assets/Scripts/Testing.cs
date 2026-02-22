using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Ship ship;
    [SerializeField] private UISkillTree uiSkillTree;
    
    void Start()
    {
        uiSkillTree.SetPlayerSkills(ship.GetPlayerSkills());
    }

}
