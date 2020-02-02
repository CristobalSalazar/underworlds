using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;

[RequireComponent(typeof(Button))]
public class SkillButton : MonoBehaviour
{
    private static List<SkillButton> skillButtons = new List<SkillButton>();
    [SerializeField] private int skillNumber;
    private Button button;
    private static List<Action> skillQueue = new List<Action>();

    void OnEnable()
    {
        skillButtons.Add(this);
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            GameEvents.Emit("Skill" + skillNumber.ToString());
        });
    }

    void OnDestroy()
    {
        skillButtons.Remove(this);
    }

    void Update()
    {
        Skill _skill = SkillBook.GetSkill(skillNumber);
        if (_skill == null) return;
        button.interactable = PlayerMana.CurrentMana >= _skill.actionCost * 100;
    }

    public static Button GetButton(int buttonNumber)
    {
        return skillButtons.First((SkillButton button) => {
            return button.skillNumber == buttonNumber;
        }).button;
    }
}
