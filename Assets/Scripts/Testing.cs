using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private UISkillTree uiSkillTree;
    
    void Start()
    {
        var shipObj = GameObject.FindGameObjectWithTag("Player");
        if (shipObj == null) return;

        var ship = shipObj.GetComponent<Ship>();
        if (ship == null) return;
        uiSkillTree.SetPlayerSkills(ship.GetPlayerSkills());
    }

}
