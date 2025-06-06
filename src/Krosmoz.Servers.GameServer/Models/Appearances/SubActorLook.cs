// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using System.Text;
using Krosmoz.Core.Cache;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Servers.GameServer.Models.Appearances;

/// <summary>
/// Represents a sub-actor look.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class SubActorLook
{
    private readonly ActorLook? _look;

    private SubEntityBindingPointCategories _bindingCategory;
    private sbyte _bindingIndex;

    /// <summary>
    /// Gets or sets the binding index of the sub-entity.
    /// Changing this value invalidates the sub-entity validator.
    /// </summary>
    public sbyte BindingIndex
    {
        get => _bindingIndex;
        set
        {
            _bindingIndex = value;
            SubEntityValidator.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the binding category of the sub-entity.
    /// Changing this value invalidates the sub-entity validator.
    /// </summary>
    public SubEntityBindingPointCategories BindingCategory
    {
        get => _bindingCategory;
        set
        {
            _bindingCategory = value;
            SubEntityValidator.Invalidate();
        }
    }

    /// <summary>
    /// Gets the look of the sub-entity.
    /// Setting this value updates the associated look validator.
    /// </summary>
    public ActorLook? Look
    {
        get => _look;
        private init
        {
            if (value is null)
            {
                _look = null;
                return;
            }

            if (_look is not null)
                _look.EntityLookValidator.ObjectInvalidated -= OnLookInvalidated;

            _look = value;
            _look.EntityLookValidator.ObjectInvalidated += OnLookInvalidated;
        }
    }

    /// <summary>
    /// Gets the validator for the sub-entity, which ensures the sub-entity is valid.
    /// </summary>
    public ObjectValidator<SubEntity> SubEntityValidator { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SubActorLook"/> class.
    /// </summary>
    /// <param name="index">The binding index of the sub-entity.</param>
    /// <param name="category">The binding category of the sub-entity.</param>
    /// <param name="look">The look of the sub-entity.</param>
    public SubActorLook(sbyte index, SubEntityBindingPointCategories category, ActorLook look)
    {
        _bindingIndex = index;
        _bindingCategory = category;
        Look = look;
        SubEntityValidator = new ObjectValidator<SubEntity>(BuildSubEntity);
    }

    /// <summary>
    /// Builds a new sub-entity based on the current state of the sub-actor look.
    /// </summary>
    /// <returns>A new <see cref="SubEntity"/> instance.</returns>
    private SubEntity BuildSubEntity()
    {
        return new SubEntity { BindingPointCategory = (sbyte)BindingCategory, BindingPointIndex = BindingIndex, SubEntityLook = Look!.GetEntityLook() };
    }

    /// <summary>
    /// Gets the validated sub-entity.
    /// </summary>
    /// <returns>The validated <see cref="SubEntity"/> instance.</returns>
    public SubEntity GetSubEntity()
    {
        return SubEntityValidator;
    }

    /// <summary>
    /// Handles the invalidation of the look validator by invalidating the sub-entity validator.
    /// </summary>
    /// <param name="obj">The invalidated look validator.</param>
    private void OnLookInvalidated(ObjectValidator<EntityLook> obj)
    {
        SubEntityValidator.Invalidate();
    }

    /// <summary>
    /// Returns a string representation of the sub-actor look.
    /// </summary>
    /// <returns>A string representing the sub-actor look.</returns>
    public override string ToString()
    {
        return new StringBuilder()
            .Append((sbyte)BindingCategory)
            .Append('@')
            .Append(BindingIndex)
            .Append('=')
            .Append(Look).ToString();
    }
}
