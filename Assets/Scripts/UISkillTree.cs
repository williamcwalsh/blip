using UnityEngine;
using UnityEngine.UI;

public class UISkillTree : MonoBehaviour
{
    [SerializeField] private Button skillBtn;
    private PlayerSkills playerSkills;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake(){
        skillBtn.onClick.AddListener(() => {
            playerSkills.UnlockSkill(PlayerSkills.SkillType.Shooting1);
        });
    }

    public void SetPlayerSkills(PlayerSkills playerSkills){
        this.playerSkills = playerSkills;
    }
}
