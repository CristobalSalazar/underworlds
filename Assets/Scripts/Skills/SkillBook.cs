using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillBook : MonoBehaviour
{
    private static List<Action> skillQueue = new List<Action>();
    [SerializeField] private Skill[] _skills;
    public static Skill[] skills { get; private set; }

    void OnEnable()
    {
        skills = _skills;
    }

    void CastSkillsInQueue()
    {
        foreach (Action action in skillQueue)
        {
            action.Invoke();
        }
        skillQueue = new List<Action>();
    }

    public static Skill GetSkill(int number)
    {
        try {
            return skills[number];
        } catch {
            return null;
        }
    }

    void Start ()
    {
        for (int i =  0; i < skills.Length; i ++)
        {
            skills[i].transform = transform;
            skills[i].Init();
            GameEvents.On("Skill" + i.ToString(), skills[i].Cast);

            Button btn = SkillButton.GetButton(i);
            Image btnImage = btn.GetComponent<Image>();
            btnImage.sprite = skills[i].image;
            btnImage.color = Color.white;


        }
        GameEvents.On("PlayerTick", CastSkillsInQueue);
    }

    void AddToSkillQueue(int skill)
    {
        skillQueue.Add(skills[skill].Cast);
    }


    void OnDestroy()
    {
        GameEvents.Unsubscribe("PlayerTick", CastSkillsInQueue);
        for (int i =  0; i < skills.Length; i ++)
        {
            GameEvents.Unsubscribe("Skill" + i.ToString(), skills[i].Cast);
        }
    }
}
