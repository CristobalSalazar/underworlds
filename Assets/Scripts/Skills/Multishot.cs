using UnityEngine;

[CreateAssetMenu(menuName="Skills/Multishot")]
public class Multishot : Skill
{
  private bool canCast;

    public override void Init()
    {
      GameEvents.On("PlayerTick", Spell);
    }

    public override void Cast()
    {
      canCast = true;
    }

    private void Spell()
    {
        if (canCast) {

            PlayerMana.DecreaseMana(actionCost * 100);

            GameObject multishot = Resources.Load<GameObject>("Prefabs/Skills/Multishot");
            GameObject instance = Instantiate(multishot, base.transform.position, base.transform.rotation, null);
            instance.transform.up = PlayerMovement.FacingDirection;
            canCast = false;
        }
    }
}

