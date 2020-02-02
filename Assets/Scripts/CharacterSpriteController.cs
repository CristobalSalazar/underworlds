using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    public SpriteRenderer head;
    public Sprite frontHead;
    public Sprite backHead;
    public SpriteRenderer body;
    public Sprite frontBody;
    public Sprite backBody;
    public Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        GameEvents.On("PlayerStartStep", OnPlayerStartStep);
        GameEvents.On("PlayerFace", SetSprites);
        FrontFace();
    }

    void OnDestroy()
    {
        GameEvents.Unsubscribe("PlayerStartStep", OnPlayerStartStep);
        GameEvents.Unsubscribe("PlayerFace", SetSprites);
    }

    private void OnPlayerStartStep()
    {
        animator.Play("Jump", 0, 0);
    }


    private void FrontFace()
    {
        head.sprite = frontHead;
        head.sortingOrder = 1;
        body.sprite = frontBody;
    }

    private void BackFace()
    {
        head.sprite = backHead;
        head.sortingOrder = -1;
        body.sprite = backBody;
    }

    public void SetSprites()
    {
        Vector2 direction = PlayerMovement.FacingDirection;
        if (direction == Vector2.up) {
            BackFace();
        } else {
            FrontFace();
        }
    }
}
