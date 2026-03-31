using UnityEngine;

public class PlayerVisualAnimator : MonoBehaviour
{
    [Header("References")]
    public PlayerState state;

    [Header("Renderers")]
    public SpriteRenderer body;
    public SpriteRenderer outfit;
    public SpriteRenderer shortHair;
    public SpriteRenderer longHair;

    [Header("Body")]
    public Sprite idleDownBody;
    public Sprite idleUpBody;
    public Sprite idleSideBody;
    public Sprite[] walkDownBody;
    public Sprite[] walkUpBody;
    public Sprite[] walkSideBody;

    [Header("Short Hair")]
    public Sprite idleDownShort;
    public Sprite idleUpShort;
    public Sprite idleSideShort;
    public Sprite[] walkDownShort;
    public Sprite[] walkUpShort;
    public Sprite[] walkSideShort;

    [Header("Long Hair")]
    public Sprite idleDownLong;
    public Sprite idleUpLong;
    public Sprite idleSideLong;
    public Sprite[] walkDownLong;
    public Sprite[] walkUpLong;
    public Sprite[] walkSideLong;

    [Header("Outfit")]
    public Sprite idleDownOutfit;
    public Sprite idleUpOutfit;
    public Sprite idleSideOutfit;
    public Sprite[] walkDownOutfit;
    public Sprite[] walkUpOutfit;
    public Sprite[] walkSideOutfit;

    [Header("Animation")]
    public float frameRate = 0.12f;

    private float timer;
    private int frame;

    private enum Facing { Down, Up, Side }
    private Facing facing = Facing.Down;

    void Update()
    {
        if (state == null) return;

        Vector2 dir = state.lastMoveDir;

        // Determine direction
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            facing = Facing.Side;
        else
            facing = dir.y > 0 ? Facing.Up : Facing.Down;

        if (state.isMoving)
        {
            timer += Time.deltaTime;
            if (timer >= frameRate)
            {
                timer = 0;
                frame++;
                PlayWalk();
            }
        }
        else
        {
            frame = 0;
            PlayIdle();
        }

        HandleFlip(dir);
    }

    void PlayWalk()
    {
        switch (facing)
        {
            case Facing.Down:
                SetAll(Get(walkDownBody), Get(walkDownShort), Get(walkDownLong), Get(walkDownOutfit));
                break;

            case Facing.Up:
                SetAll(Get(walkUpBody), Get(walkUpShort), Get(walkUpLong), Get(walkUpOutfit));
                break;

            case Facing.Side:
                SetAll(Get(walkSideBody), Get(walkSideShort), Get(walkSideLong), Get(walkSideOutfit));
                break;
        }
    }

    void PlayIdle()
    {
        switch (facing)
        {
            case Facing.Down:
                SetAll(idleDownBody, idleDownShort, idleDownLong, idleDownOutfit);
                break;

            case Facing.Up:
                SetAll(idleUpBody, idleUpShort, idleUpLong, idleUpOutfit);
                break;

            case Facing.Side:
                SetAll(idleSideBody, idleSideShort, idleSideLong, idleSideOutfit);
                break;
        }
    }

    void SetAll(Sprite b, Sprite s, Sprite l, Sprite o)
    {
        if (body) body.sprite = b;

        if (shortHair.enabled && s != null)
            shortHair.sprite = s;

        if (longHair.enabled && l != null)
            longHair.sprite = l;

        if (outfit != null && outfit.enabled && o != null)
            outfit.sprite = o;
    }

    Sprite Get(Sprite[] arr)
    {
        if (arr == null || arr.Length == 0) return null;
        return arr[frame % arr.Length];
    }

    void HandleFlip(Vector2 dir)
    {
        bool flip = facing == Facing.Side && dir.x < 0;

        body.flipX = flip;
        outfit.flipX = flip;
        shortHair.flipX = flip;
        longHair.flipX = flip;
    }
}