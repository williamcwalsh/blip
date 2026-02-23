using UnityEngine;
using UnityEngine.UI;

public class UISkillTree : MonoBehaviour
{
    [SerializeField] private Button gunUpgrade;
    [SerializeField] private Button engineUpgrade;
    [SerializeField] private Button engineUpgrade2;

    public Ship ship;
    public int t1cost=5;
    public int t2cost=10;



    private PlayerSkills playerSkills;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake(){
        gunUpgrade.onClick.AddListener(() => {
            if(ship.money>=t1cost){
                playerSkills.UnlockSkill(PlayerSkills.SkillType.Shooting1);
                ship.money-=t1cost;
            }
        });
        engineUpgrade.onClick.AddListener(() => {
            if(ship.money>=t1cost){
                playerSkills.UnlockSkill(PlayerSkills.SkillType.Engine1);
                ship.money-=t1cost;
            }
        });
        engineUpgrade2.onClick.AddListener(() => {
            if(ship.money>=t2cost &&
            playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Engine1)
            ){
                playerSkills.UnlockSkill(PlayerSkills.SkillType.Engine2);
                ship.money-=t2cost;
            }
        });
    }

    public void SetPlayerSkills(PlayerSkills playerSkills){
        this.playerSkills = playerSkills;
    }
}
