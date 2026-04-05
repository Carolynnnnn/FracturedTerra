# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

FracturedTerra is a 2D top-down adventure game built in **Unity 6000.3.7f1** using the Universal Render Pipeline (URP). It features 6 playable levels, each owned by a different team member, connected via a central hub world with gem-gated portals.

## Building & Running

This is a Unity project — there are no CLI build commands. All development requires:
- **Unity 6000.3.7f1** (exact version required)
- Open via Unity Hub, then open `Fractured Terra/` as the project folder
- The **New Input System** is enabled; do not use the legacy `Input` class

**Build scene order** (configured in Build Settings):
1. `Assets/Scenes/Main Menu.unity`
2. `Assets/Scenes/Carylions Hubworld Backup.unity`
3. `Assets/Scenes/Levels/Sophia_Level1.unity` through `Carolyn_Level6.unity`

**Test scenes** for isolated system testing: `SophiaTest.unity`, `Rachel Attack.unity`, `SimranTest.unity`, `Customize.unity`, `Rachel Bars.unity`

## Architecture

### Core Player Stack

The player is composed of several independent scripts on the Player prefab:

- **`PlayerController.cs`** — WASD input via New Input System → sets velocity on Rigidbody2D; checks `PlayerState.canMove` before moving
- **`PlayerState.cs`** — Shared state struct (`canMove`, `isMoving`, `lastMoveDir`); read by animator and other systems
- **`PlayerVisualAnimator.cs`** — Multi-layered sprite animation (body + hair + outfit); frame-based at configurable rate; 8-directional with sprite flipping
- **`PlayerHealth.cs`** — Max 100 HP, 2 HP/sec regen after 4-sec delay; respawns at `PlayerSpawner`; updates health bar UI fill amount
- **`PlayerAttackRP.cs`** — 12-slot ability system with 6 ability types (Melee, Projectile, Area, Freeze, Charm, ShieldAura); attack key: O

### Ability System ("RP" suffix = Raph-Pierre's systems)

`AbilityDataRP.cs` is a serializable data class (name, range, damage, cooldown, effectPrefab, type, unlocked).

- **`AbilityUnlockManagerRP.cs`** — Singleton; call `UnlockAbility(index)` to gate unlocks
- **`AbilitySaveSystemRP.cs`** — Persists unlock state via `PlayerPrefs` with keys `"AbilityUnlocked_" + index` and `"SelectedAbility"`; **F9 clears all save data** (debug key)
- **`AbilityUIManagerRP.cs`** — 12 buttons; locked = alpha 0.35, unlocked = alpha 1.0

### Inventory & Items

`InventoryManager.cs` holds a **static list** (persists across scenes) of up to 12 `InventoryItem` slots. Item use is a switch-statement by `itemName` (e.g., "Health Potion" heals 50 HP). Drop: hold L for 3 seconds.

`WeaponItem.cs` extends `InventoryItem` with `attackPower`, `attackSpeed`, `acquiredInLevel`. Six weapons defined in `WeaponDatabase.cs` (Wood→Stone→Fire/Dark/Light Swords, Trident).

### Interactable Pattern

Implement `IInteractable` (`Interact()` + `CanInteract()`) for world objects. P key triggers interaction via `InteractionControl.cs`. Current implementors: `Chest.cs`, `LvlPortal.cs` (requires gem count to pass).

### Progression

`PlayerProgress.cs` tracks XP, coins, armor (scales at 20 and 50 XP thresholds), and activity counts. Call `RegisterActivity(ActivityType, xpReward, coinReward)`.

`CoinManager.cs` and `GemManager.cs` use static counters + `TextMeshPro` UI updated in `Update()`.

### Enemy Systems

- **`EnemyHealthRP.cs`** — `TakeDamage(int)` → `Die()` destroys GameObject
- **`EnemyStatusRP.cs`** — `Freeze(duration)` disables scripts; `CharmRemove()` destroys enemy

### Persistence

Only `PlayerPrefs` is used (ability unlocks). Inventory and coin/gem counts are static fields — they persist within a play session but reset on application restart.

## Key Conventions

- **"RP" suffix** on scripts = Raph-Pierre's combat/ability system; a legacy `AttackHitbox.cs` also exists but is superseded by the RP system
- **`[SerializeField]`** is used extensively for editor-configurable fields; prefer this over public fields
- Each team member owns their level folder and level scene: `Alisha - Level 2/`, `JapneetAssets/`, etc.
- `FindObjectOfType<>()` is used in some places — avoid adding more of these; prefer direct references

## Known Debug Keys to Be Aware Of

These are in production code and should not be triggered accidentally:
- **H** — `PlayerHealth.cs`: deals 10 damage to player
- **K** — `PlayerProgress.cs`: registers a test activity
- **F9** — `AbilitySaveSystemRP.cs`: deletes all `PlayerPrefs` data
