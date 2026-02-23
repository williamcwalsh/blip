using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSkills
{
    
    public enum SkillType{
        None,
        Shooting1,
        Engine1,
        Engine2,
    }
    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills(){
        unlockedSkillTypeList = new List<SkillType>();
    }

    public void UnlockSkill(SkillType skillType){
        unlockedSkillTypeList.Add(skillType);
    }

    public bool IsSkillUnlocked(SkillType skillType){
        return unlockedSkillTypeList.Contains(skillType);
    }
}
