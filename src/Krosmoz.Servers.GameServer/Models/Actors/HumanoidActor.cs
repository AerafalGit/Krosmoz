// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Types.Game.Character.Restriction;
using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Servers.GameServer.Models.Actors;

/// <summary>
/// Represents a humanoid actor in the game world.
/// A humanoid actor is an extension of the <see cref="NamedActor"/> class
/// with additional properties for sex, restrictions, and human options.
/// </summary>
public abstract class HumanoidActor : NamedActor
{
    /// <summary>
    /// Gets the unique identifier for the account associated with the humanoid actor.
    /// </summary>
    public abstract int AccountId { get; }

    /// <summary>
    /// Gets or sets the sex of the humanoid actor.
    /// </summary>
    public abstract bool Sex { get; set; }

    /// <summary>
    /// Gets or sets the restrictions applied to the humanoid actor.
    /// </summary>
    public abstract Restrictions Restrictions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot aggress others.
    /// </summary>
    public bool CantAggress
    {
        get => Restrictions.HasFlag(Restrictions.CantAggress);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantAggress;
            else
                Restrictions &= ~Restrictions.CantAggress;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot be aggressed by others.
    /// </summary>
    public bool CantBeAggressed
    {
        get => Restrictions.HasFlag(Restrictions.CantBeAggressed);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantBeAggressed;
            else
                Restrictions &= ~Restrictions.CantBeAggressed;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot trade with others.
    /// </summary>
    public bool CantTrade
    {
        get => Restrictions.HasFlag(Restrictions.CantTrade);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantTrade;
            else
                Restrictions &= ~Restrictions.CantTrade;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot use objects.
    /// </summary>
    public bool CantUseObject
    {
        get => Restrictions.HasFlag(Restrictions.CantUseObject);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantUseObject;
            else
                Restrictions &= ~Restrictions.CantUseObject;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot use tax collectors.
    /// </summary>
    public bool CantUseTaxCollector
    {
        get => Restrictions.HasFlag(Restrictions.CantUseTaxCollector);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantUseTaxCollector;
            else
                Restrictions &= ~Restrictions.CantUseTaxCollector;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot use interactives.
    /// </summary>
    public bool CantUseInteractive
    {
        get => Restrictions.HasFlag(Restrictions.CantUseInteractive);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantUseInteractive;
            else
                Restrictions &= ~Restrictions.CantUseInteractive;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot chat.
    /// </summary>
    public bool CantChat
    {
        get => Restrictions.HasFlag(Restrictions.CantChat);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantChat;
            else
                Restrictions &= ~Restrictions.CantChat;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot be challenged.
    /// </summary>
    public bool CantBeChallenged
    {
        get => Restrictions.HasFlag(Restrictions.CantBeChallenged);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantBeChallenged;
            else
                Restrictions &= ~Restrictions.CantBeChallenged;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot be attacked by mutants.
    /// </summary>
    public bool CantBeAttackedByMutant
    {
        get => Restrictions.HasFlag(Restrictions.CantBeAttackedByMutant);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantBeAttackedByMutant;
            else
                Restrictions &= ~Restrictions.CantBeAttackedByMutant;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot attack others.
    /// </summary>
    public bool CantAttack
    {
        get => Restrictions.HasFlag(Restrictions.CantAttack);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantAttack;
            else
                Restrictions &= ~Restrictions.CantAttack;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot attack monsters.
    /// </summary>
    public bool CantAttackMonster
    {
        get => Restrictions.HasFlag(Restrictions.CantAttackMonster);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantAttackMonster;
            else
                Restrictions &= ~Restrictions.CantAttackMonster;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot be a merchant.
    /// </summary>
    public bool CantBeMerchant
    {
        get => Restrictions.HasFlag(Restrictions.CantBeMerchant);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantBeMerchant;
            else
                Restrictions &= ~Restrictions.CantBeMerchant;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot challenge others.
    /// </summary>
    public bool CantChallenge
    {
        get => Restrictions.HasFlag(Restrictions.CantChallenge);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantChallenge;
            else
                Restrictions &= ~Restrictions.CantChallenge;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot change zones.
    /// </summary>
    public bool CantChangeZone
    {
        get => Restrictions.HasFlag(Restrictions.CantChangeZone);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantChangeZone;
            else
                Restrictions &= ~Restrictions.CantChangeZone;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot exchange items.
    /// </summary>
    public bool CantExchange
    {
        get => Restrictions.HasFlag(Restrictions.CantExchange);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantExchange;
            else
                Restrictions &= ~Restrictions.CantExchange;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot minimize the game.
    /// </summary>
    public bool CantMinimize
    {
        get => Restrictions.HasFlag(Restrictions.CantMinimize);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantMinimize;
            else
                Restrictions &= ~Restrictions.CantMinimize;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot move.
    /// </summary>
    public bool CantMove
    {
        get => Restrictions.HasFlag(Restrictions.CantMove);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantMove;
            else
                Restrictions &= ~Restrictions.CantMove;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot run.
    /// </summary>
    public bool CantRun
    {
        get => Restrictions.HasFlag(Restrictions.CantRun);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantRun;
            else
                Restrictions &= ~Restrictions.CantRun;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot speak to NPCs.
    /// </summary>
    public bool CantSpeakToNpc
    {
        get => Restrictions.HasFlag(Restrictions.CantSpeakToNpc);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantSpeakToNpc;
            else
                Restrictions &= ~Restrictions.CantSpeakToNpc;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor cannot walk in 8 directions.
    /// </summary>
    public bool CantWalk8Directions
    {
        get => Restrictions.HasFlag(Restrictions.CantWalk8Directions);
        set
        {
            if (value)
                Restrictions |= Restrictions.CantWalk8Directions;
            else
                Restrictions &= ~Restrictions.CantWalk8Directions;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the actor is forced to walk slowly.
    /// </summary>
    public bool ForceSlowWalk
    {
        get => Restrictions.HasFlag(Restrictions.ForceSlowWalk);
        set
        {
            if (value)
                Restrictions |= Restrictions.ForceSlowWalk;
            else
                Restrictions &= ~Restrictions.ForceSlowWalk;
        }
    }

    /// <summary>
    /// Determines whether the humanoid actor can move.
    /// </summary>
    /// <returns>
    /// True if the humanoid actor can move; otherwise, false.
    /// The result is based on the actor's restrictions and the base implementation.
    /// </returns>
    public override bool CanMove()
    {
        return !CantMove && base.CanMove();
    }

    /// <summary>
    /// Retrieves the game roleplay actor information for the humanoid actor.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="GameRolePlayHumanoidInformations"/> containing
    /// details about the humanoid actor, such as account ID, contextual ID, look,
    /// disposition, name, and humanoid-specific information.
    /// </returns>
    public override GameRolePlayActorInformations GetGameRolePlayActorInformations()
    {
        return new GameRolePlayHumanoidInformations
        {
            AccountId = AccountId,
            ContextualId = Id,
            Look = Look.GetEntityLook(),
            Disposition = GetEntityDispositionInformations(),
            Name = Name,
            HumanoidInfo = GetHumanInformations()
        };
    }

    /// <summary>
    /// Retrieves the human information for the humanoid actor.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="HumanInformations"/> containing details about
    /// the humanoid actor, such as sex, options, and restrictions.
    /// </returns>
    public virtual HumanInformations GetHumanInformations()
    {
        return new HumanInformations
        {
            Sex = Sex,
            Options = GetHumanOptions(),
            Restrictions = GetActorRestrictionsInformations(),
        };
    }

    /// <summary>
    /// Retrieves the actor restrictions information for the humanoid actor.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ActorRestrictionsInformations"/> containing
    /// the restrictions applied to the humanoid actor, such as whether they
    /// can trade, move, chat, or perform other actions.
    /// </returns>
    public virtual ActorRestrictionsInformations GetActorRestrictionsInformations()
    {
        return new ActorRestrictionsInformations
        {
            CantAggress = CantAggress,
            CantBeAggressed = CantBeAggressed,
            CantTrade = CantTrade,
            CantUseObject = CantUseObject,
            CantUseTaxCollector = CantUseTaxCollector,
            CantUseInteractive = CantUseInteractive,
            CantChat = CantChat,
            CantBeChallenged = CantBeChallenged,
            CantBeAttackedByMutant = CantBeAttackedByMutant,
            CantAttack = CantAttack,
            CantAttackMonster = CantAttackMonster,
            CantBeMerchant = CantBeMerchant,
            CantChallenge = CantChallenge,
            CantChangeZone = CantChangeZone,
            CantExchange = CantExchange,
            CantMinimize = CantMinimize,
            CantMove = CantMove,
            CantRun = CantRun,
            CantWalk8Directions = CantWalk8Directions,
            ForceSlowWalk = ForceSlowWalk,
            CantSpeakToNPC = CantSpeakToNpc
        };
    }

    /// <summary>
    /// Retrieves the human options associated with the humanoid actor.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="HumanOption"/> representing the human options.
    /// </returns>
    public virtual IEnumerable<HumanOption> GetHumanOptions()
    {
        return [];
    }
}
