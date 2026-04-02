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

    [Header("Outfit 1")]
    public Sprite idleDownOutfit1;
    public Sprite idleUpOutfit1;
    public Sprite idleSideOutfit1;
    public Sprite[] walkDownOutfit1;
    public Sprite[] walkUpOutfit1;
    public Sprite[] walkSideOutfit1;

    [Header("Outfit 2")]
    public Sprite idleDownOutfit2;
    public Sprite idleUpOutfit2;
    public Sprite idleSideOutfit2;
    public Sprite[] walkDownOutfit2;
    public Sprite[] walkUpOutfit2;
    public Sprite[] walkSideOutfit2;

    [Header("Outfit 3")]
    public Sprite idleDownOutfit3;
    public Sprite idleUpOutfit3;
    public Sprite idleSideOutfit3;
    public Sprite[] walkDownOutfit3;
    public Sprite[] walkUpOutfit3;
    public Sprite[] walkSideOutfit3;

    [Header("Current Outfit")]
    public int currentOutfitIndex = 0;
    // 0 = no outfit
    // 1 = outfit 1
    // 2 = outfit 2
    // 3 = outfit 3

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
                SetAll(Get(walkDownBody), Get(walkDownShort), Get(walkDownLong), GetCurrentWalkDownOutfit());
                break;

            case Facing.Up:
                SetAll(Get(walkUpBody), Get(walkUpShort), Get(walkUpLong), GetCurrentWalkUpOutfit());
                break;

            case Facing.Side:
                SetAll(Get(walkSideBody), Get(walkSideShort), Get(walkSideLong), GetCurrentWalkSideOutfit());
                break;
        }
    }

    void PlayIdle()
    {
        switch (facing)
        {
            case Facing.Down:
                SetAll(idleDownBody, idleDownShort, idleDownLong, GetCurrentIdleDownOutfit());
                break;

            case Facing.Up:
                SetAll(idleUpBody, idleUpShort, idleUpLong, GetCurrentIdleUpOutfit());
                break;

            case Facing.Side:
                SetAll(idleSideBody, idleSideShort, idleSideLong, GetCurrentIdleSideOutfit());
                break;
        }
    }

    void SetAll(Sprite b, Sprite s, Sprite l, Sprite o)
    {
        if (body != null && b != null)
            body.sprite = b;

        if (shortHair != null && shortHair.enabled && s != null)
            shortHair.sprite = s;

        if (longHair != null && longHair.enabled && l != null)
            longHair.sprite = l;

        if (outfit != null)
        {
            if (currentOutfitIndex == 0)
            {
                outfit.enabled = false;
            }
            else
            {
                outfit.enabled = true;

                if (o != null)
                    outfit.sprite = o;
            }
        }
    }

    Sprite Get(Sprite[] arr)
    {
        if (arr == null || arr.Length == 0) return null;
        return arr[frame % arr.Length];
    }

    Sprite GetCurrentIdleDownOutfit()
    {
        switch (currentOutfitIndex)
        {
            case 1: return idleDownOutfit1;
            case 2: return idleDownOutfit2;
            case 3: return idleDownOutfit3;
            default: return null;
        }
    }

    Sprite GetCurrentIdleUpOutfit()
    {
        switch (currentOutfitIndex)
        {
            case 1: return idleUpOutfit1;
            case 2: return idleUpOutfit2;
            case 3: return idleUpOutfit3;
            default: return null;
        }
    }

    Sprite GetCurrentIdleSideOutfit()
    {
        switch (currentOutfitIndex)
        {
            case 1: return idleSideOutfit1;
            case 2: return idleSideOutfit2;
            case 3: return idleSideOutfit3;
            default: return null;
        }
    }

    Sprite GetCurrentWalkDownOutfit()
    {
        switch (currentOutfitIndex)
        {
            case 1: return Get(walkDownOutfit1);
            case 2: return Get(walkDownOutfit2);
            case 3: return Get(walkDownOutfit3);
            default: return null;
        }
    }

    Sprite GetCurrentWalkUpOutfit()
    {
        switch (currentOutfitIndex)
        {
            case 1: return Get(walkUpOutfit1);
            case 2: return Get(walkUpOutfit2);
            case 3: return Get(walkUpOutfit3);
            default: return null;
        }
    }

    Sprite GetCurrentWalkSideOutfit()
    {
        switch (currentOutfitIndex)
        {
            case 1: return Get(walkSideOutfit1);
            case 2: return Get(walkSideOutfit2);
            case 3: return Get(walkSideOutfit3);
            default: return null;
        }
    }

    void HandleFlip(Vector2 dir)
    {
        bool flip = facing == Facing.Side && dir.x < 0;

        if (body != null) body.flipX = flip;
        if (outfit != null) outfit.flipX = flip;
        if (shortHair != null) shortHair.flipX = flip;
        if (longHair != null) longHair.flipX = flip;
    }
}