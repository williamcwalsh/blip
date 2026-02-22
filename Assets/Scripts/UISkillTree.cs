using UnityEngine;
using UnityEngine.UI;

public class UISkillTree : MonoBehaviour
{
    [SerializeField] private Button gunUpgrade;
    [SerializeField] private Button engineUpgrade;

    private PlayerSkills playerSkills;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake(){
        gunUpgrade.onClick.AddListener(() => {
            playerSkills.UnlockSkill(PlayerSkills.SkillType.Shooting1);
            Debug.Log("test");
        });
        engineUpgrade.onClick.AddListener(() => {
            playerSkills.UnlockSkill(PlayerSkills.SkillType.Engine1);
        });
    }

    public void SetPlayerSkills(PlayerSkills playerSkills){
        this.playerSkills = playerSkills;
    }
}
