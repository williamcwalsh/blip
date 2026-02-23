using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private UISkillTree uiSkillTree;
    public Ship ship;
    
    void Start()
    {
        
        uiSkillTree.SetPlayerSkills(ship.GetPlayerSkills());
    }

}
