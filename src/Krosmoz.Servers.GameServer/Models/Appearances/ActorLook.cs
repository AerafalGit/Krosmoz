// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using Krosmoz.Core.Cache;
using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Servers.GameServer.Models.Appearances;

/// <summary>
/// Represents the appearance of an actor.
/// </summary>
public sealed class ActorLook
{
    private Dictionary<int, Color> _colors;
    private List<short> _defaultScales;
    private List<short> _scales;
    private List<ushort> _skins;
    private List<SubActorLook> _subLooks;
    private ushort _bonesId;
    private ushort _guildSkin;

    /// <summary>
    /// Gets or sets the ID of the actor's bones.
    /// Changing this value invalidates the entity look validator.
    /// </summary>
    public ushort BonesId
    {
        get => _bonesId;
        set
        {
            _bonesId = value;
            EntityLookValidator.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the guild skin of the actor.
    /// Changing this value invalidates the entity look validator.
    /// </summary>
    public ushort GuildSkin
    {
        get => _guildSkin;
        set
        {
            _guildSkin = value;
            EntityLookValidator.Invalidate();
        }
    }

    /// <summary>
    /// Gets the collection of skins applied to the actor, including the guild skin if present.
    /// </summary>
    private ReadOnlyCollection<ushort> Skins
    {
        get
        {
            var skins = _skins.ToList();

            if (GuildSkin is not 0)
                skins.Add(GuildSkin);

            return skins.AsReadOnly();
        }
    }

    /// <summary>
    /// Gets the collection of scales applied to the actor.
    /// </summary>
    public IReadOnlyCollection<short> Scales =>
        _scales.AsReadOnly();

    /// <summary>
    /// Gets the collection of sub-looks (sub-entities) associated with the actor.
    /// </summary>
    public IReadOnlyCollection<SubActorLook> SubLooks =>
        _subLooks.AsReadOnly();

    /// <summary>
    /// Gets the look of the actor's pet, if any.
    /// </summary>
    public ActorLook? PetLook =>
        _subLooks.FirstOrDefault(static x => x.BindingCategory is SubEntityBindingPointCategories.HookPointCategoryPet)?.Look;

    /// <summary>
    /// Gets the collection of colors applied to the actor, indexed by their respective keys.
    /// </summary>
    public IReadOnlyDictionary<int, Color> Colors =>
        new ConcurrentDictionary<int, Color>(_colors);

    /// <summary>
    /// Gets the validator for the entity look, which ensures the entity is valid.
    /// </summary>
    public ObjectValidator<EntityLook> EntityLookValidator { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActorLook"/> class with default values.
    /// </summary>
    private ActorLook()
    {
        _colors = [];
        _skins = [];
        _scales = [];
        _defaultScales = [];
        _subLooks = [];
        EntityLookValidator = new ObjectValidator<EntityLook>(BuildEntityLook);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActorLook"/> class with a specified bones ID.
    /// </summary>
    /// <param name="bones">The ID of the actor's bones.</param>
    public ActorLook(ushort bones) : this()
    {
        _bonesId = bones;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActorLook"/> class with specified properties.
    /// </summary>
    /// <param name="bones">The ID of the actor's bones.</param>
    /// <param name="skins">The collection of skins applied to the actor.</param>
    /// <param name="indexedColors">The collection of colors applied to the actor, indexed by their keys.</param>
    /// <param name="scales">The collection of scales applied to the actor.</param>
    /// <param name="subLooks">The collection of sub-looks (sub-entities) associated with the actor.</param>
    private ActorLook(
        ushort bones,
        IEnumerable<ushort> skins,
        Dictionary<int, Color> indexedColors,
        IEnumerable<short> scales,
        IEnumerable<SubActorLook> subLooks)
        : this()
    {
        _bonesId = bones;
        _skins = skins.ToList();
        _colors = indexedColors;

        var enumerable = scales.ToList();
        _scales = enumerable;
        _defaultScales = enumerable;
        _subLooks = subLooks.ToList();
    }

    /// <summary>
    /// Sets the skins applied to the actor.
    /// </summary>
    /// <param name="skins">The collection of skins to apply.</param>
    public void SetSkins(params ushort[] skins)
    {
        _skins = skins.ToList();
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Adds a skin to the actor.
    /// </summary>
    /// <param name="skin">The skin to add.</param>
    public void AddSkin(ushort skin)
    {
        if (_skins.Contains(skin))
            return;

        _skins.Add(skin);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Adds a skin to the actor at a specified index.
    /// </summary>
    /// <param name="skin">The skin to add.</param>
    /// <param name="index">The index at which to add the skin.</param>
    public void AddSkin(ushort skin, ushort index)
    {
        if (_skins.Contains(skin))
            return;

        _skins.Insert(skin, index);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Adds multiple skins to the actor.
    /// </summary>
    /// <param name="skins">The collection of skins to add.</param>
    public void AddSkins(IEnumerable<ushort> skins)
    {
        _skins.AddRange(skins);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Removes a skin from the actor.
    /// </summary>
    /// <param name="skin">The skin to remove.</param>
    public void RemoveSkin(ushort skin)
    {
        _skins.Remove(skin);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Removes all skins from the actor.
    /// </summary>
    public void RemoveSkins()
    {
        _skins.Clear();
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Resets the scales of the actor to their default values.
    /// </summary>
    public void ResetScales()
    {
        _scales = _defaultScales.ToList();
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Sets the bones ID of the actor.
    /// </summary>
    /// <param name="boneId">The bones ID to set.</param>
    public void SetBone(int boneId)
    {
        _bonesId = (ushort)boneId;
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Sets the scales applied to the actor.
    /// </summary>
    /// <param name="scales">The collection of scales to apply.</param>
    public void SetScales(params short[] scales)
    {
        _scales = scales.ToList();
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Sets a single scale for the actor.
    /// </summary>
    /// <param name="scale">The scale to set.</param>
    public void SetScale(short scale)
    {
        if (_scales.Count is 0)
            _scales.Add(scale);
        else
            _scales[0] = scale;

        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Sets the default scales for the actor.
    /// </summary>
    /// <param name="scales">The collection of default scales to set.</param>
    public void SetDefaultScales(params short[] scales)
    {
        _defaultScales = scales.ToList();
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Adds a scale to the actor.
    /// </summary>
    /// <param name="scale">The scale to add.</param>
    private void AddScale(short scale)
    {
        _scales.Add(scale);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Adjusts the scales of the actor by a specified factor.
    /// </summary>
    /// <param name="factor">The factor by which to adjust the scales.</param>
    public void Rescale(double factor)
    {
        if (_scales.Count is 0)
            AddScale((short)(100 * factor));
        else
            SetScales(_scales.Select(x => (short)(x * factor)).ToArray());
    }

    /// <summary>
    /// Adjusts the scales of the actor by dividing them by a specified factor.
    /// </summary>
    /// <param name="factor">The factor by which to divide the scales.</param>
    public void Unscale(double factor)
    {
        if (_scales.Count is 0)
            AddScale((short)(100 / factor));
        else
            SetScales(_scales.Select(x => (short)(x / factor)).ToArray());
    }

    /// <summary>
    /// Adds a color to the actor at a specified index.
    /// </summary>
    /// <param name="index">The index at which to add the color.</param>
    /// <param name="color">The color to add.</param>
    public void AddColor(int index, Color color)
    {
        _colors[index] = color;

        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Removes a color from the actor at a specified index.
    /// </summary>
    /// <param name="index">The index of the color to remove.</param>
    public void RemoveColor(int index)
    {
        _colors.Remove(index);
    }

    /// <summary>
    /// Sets the colors applied to the actor.
    /// </summary>
    /// <param name="colors">The collection of colors to set.</param>
    public void SetColors(IDictionary<int, Color> colors)
    {
        _colors = new Dictionary<int, Color>(colors);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Sets the colors applied to the actor using an array of colors.
    /// </summary>
    /// <param name="colors">The array of colors to set.</param>
    public void SetColors(params Color[] colors)
    {
        var index = 1;

        _colors = colors.ToDictionary(_ => index++);

        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Sets the colors applied to the actor using arrays of indexes and colors.
    /// </summary>
    /// <param name="indexes">The array of indexes for the colors.</param>
    /// <param name="colors">The array of colors to set.</param>
    public void SetColors(int[] indexes, Color[] colors)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(indexes.Length, colors.Length);

        _colors.Clear();

        for (var i = 0; i < indexes.Length; i++)
            _colors.Add(indexes[i], colors[i]);

        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Sets a single color for the actor at a specified key.
    /// </summary>
    /// <param name="key">The key for the color.</param>
    /// <param name="color">The color to set.</param>
    public void SetColor(int key, Color color)
    {
        _colors[key] = color;
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Removes a sub-look from the actor based on its binding category.
    /// </summary>
    /// <param name="category">The binding category of the sub-look to remove.</param>
    public void RemoveSubLook(SubEntityBindingPointCategories category)
    {
        _subLooks.RemoveAll(x => x.BindingCategory == category);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Removes a sub-look from the actor based on its binding index.
    /// </summary>
    /// <param name="index">The binding index of the sub-look to remove.</param>
    public void RemoveSubLook(int index)
    {
        _subLooks.RemoveAll(x => x.BindingIndex == index);

        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Adds a sub-look to the actor.
    /// </summary>
    /// <param name="subLook">The sub-look to add.</param>
    private void AddSubLook(SubActorLook subLook)
    {
        _subLooks.Add(subLook);

        EntityLookValidator.Invalidate();

        subLook.SubEntityValidator.ObjectInvalidated += OnSubEntityInvalidated;
    }

    /// <summary>
    /// Sets a sub-look for the actor, replacing any existing sub-look with the same binding category.
    /// </summary>
    /// <param name="subLook">The sub-look to set.</param>
    public void SetSubLook(SubActorLook subLook)
    {
        _subLooks.RemoveAll(x => x.BindingCategory == subLook.BindingCategory);
        AddSubLook(subLook);
    }

    /// <summary>
    /// Sets multiple sub-looks for the actor.
    /// </summary>
    /// <param name="subLooks">The collection of sub-looks to set.</param>
    public void SetSubLooks(params SubActorLook[] subLooks)
    {
        foreach (var subLook in _subLooks)
            subLook.SubEntityValidator.ObjectInvalidated -= OnSubEntityInvalidated;

        _subLooks = subLooks.ToList();

        EntityLookValidator.Invalidate();

        foreach (var subLook in _subLooks)
            subLook.SubEntityValidator.ObjectInvalidated += OnSubEntityInvalidated;
    }

    /// <summary>
    /// Sets the pet skin for the actor, including its scales.
    /// </summary>
    /// <param name="skin">The skin of the pet.</param>
    /// <param name="scales">The scales of the pet.</param>
    public void SetPetSkin(ushort skin, short[] scales)
    {
        ActorLook petLook;

        SetSubLook(new SubActorLook(0, SubEntityBindingPointCategories.HookPointCategoryPet, petLook = new ActorLook()));

        petLook.SetScales(scales);
        petLook.BonesId = skin;
    }

    /// <summary>
    /// Removes all pets from the actor.
    /// </summary>
    public void RemovePets()
    {
        _subLooks.RemoveAll(x => x.BindingCategory is SubEntityBindingPointCategories.HookPointCategoryPet);

        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Gets the look of the actor's rider, if any.
    /// </summary>
    /// <returns>The look of the rider, or null if no rider is present.</returns>
    public ActorLook? GetRiderLook()
    {
        return _subLooks.Find(static x => x.BindingCategory is SubEntityBindingPointCategories.HookPointCategoryMountDriver)?.Look;
    }

    /// <summary>
    /// Sets the look of the actor's rider.
    /// </summary>
    /// <param name="look">The look of the rider to set.</param>
    public void SetRiderLook(ActorLook look)
    {
        SetSubLook(new SubActorLook(0, SubEntityBindingPointCategories.HookPointCategoryMountDriver, look));
    }

    /// <summary>
    /// Removes all mounts from the actor.
    /// </summary>
    public void RemoveMounts()
    {
        _subLooks.RemoveAll(static x => x.BindingCategory == SubEntityBindingPointCategories.HookPointCategoryMountDriver);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Removes all auras from the actor.
    /// </summary>
    public void RemoveAuras()
    {
        _subLooks.RemoveAll(static x => x.BindingCategory == SubEntityBindingPointCategories.HookPointCategoryBaseForeground);
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Handles the invalidation of a sub-entity validator by invalidating the entity look validator.
    /// </summary>
    /// <param name="obj">The invalidated sub-entity validator.</param>
    private void OnSubEntityInvalidated(ObjectValidator<SubEntity> obj)
    {
        EntityLookValidator.Invalidate();
    }

    /// <summary>
    /// Creates a deep copy of the actor look.
    /// </summary>
    /// <returns>A new <see cref="ActorLook"/> instance that is a copy of the current instance.</returns>
    public ActorLook Clone()
    {
        return new ActorLook
        {
            BonesId = _bonesId,
            GuildSkin = GuildSkin,
            _colors = _colors.ToDictionary(static x => x.Key, static x => x.Value),
            _skins = _skins.ToList(),
            _scales = _scales.ToList(),
            _defaultScales = _defaultScales.ToList(),
            _subLooks = _subLooks
                .Select(static x => new SubActorLook(x.BindingIndex, x.BindingCategory, x.Look!.Clone()))
                .ToList()
        };
    }

    /// <summary>
    /// Returns a string representation of the actor look.
    /// </summary>
    /// <returns>A string representing the actor look.</returns>
    public override string ToString()
    {
        var result = new StringBuilder();

        result.Append('{');

        var missingBars = 0;

        result.Append(BonesId);

        if (Skins.Count is 0)
            missingBars++;
        else
        {
            result.Append('|');
            missingBars = 0;
            result.Append(string.Join(",", Skins));
        }

        if (!Colors.Any())
            missingBars++;
        else
        {
            result.Append("|".ConcatCopy(missingBars + 1));
            missingBars = 0;
            result.Append(string.Join(",", Colors.Select(static entry => entry.Key + "=" + entry.Value.ToArgb())));
        }

        if (Scales.Count is 0)
            missingBars++;
        else
        {
            result.Append("|".ConcatCopy(missingBars + 1));
            missingBars = 0;
            result.Append(string.Join(",", Scales));
        }

        if (SubLooks.Count is not 0)
        {
            result.Append("|".ConcatCopy(missingBars + 1));
            result.Append(string.Join(",", SubLooks.Select(static entry => entry)));
        }

        return result
            .Append('}')
            .ToString();
    }

    /// <summary>
    /// Parses a string representation of an actor look and constructs an <see cref="ActorLook"/> object.
    /// </summary>
    /// <param name="str">The string representation of the actor look.</param>
    /// <returns>An <see cref="ActorLook"/> object constructed from the string.</returns>
    /// <exception cref="ArgumentException">Thrown if the input string is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the string does not start with '{' or has invalid formatting.</exception>
    public static ActorLook Parse(string str)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);
        ArgumentOutOfRangeException.ThrowIfNotEqual(str[0], '{');

        var cursorPos = 1;
        var separatorPos = str.IndexOf('|', StringComparison.Ordinal);

        if (separatorPos is -1)
        {
            separatorPos = str.IndexOf('}');

            ArgumentOutOfRangeException.ThrowIfEqual(separatorPos, -1);
        }

        var bonesId = ushort.Parse(str.AsSpan(cursorPos, separatorPos - cursorPos), CultureInfo.InvariantCulture);
        cursorPos = separatorPos + 1;

        var skins = Array.Empty<ushort>();

        if ((separatorPos = str.IndexOf('|', cursorPos)) is not -1 || (separatorPos = str.IndexOf('}', cursorPos)) is not -1)
        {
            skins = ParseCollection(str.Substring(cursorPos, separatorPos - cursorPos), ushort.Parse);
            cursorPos = separatorPos + 1;
        }

        var colors = Array.Empty<Tuple<int, int>>();

        if ((separatorPos = str.IndexOf('|', cursorPos)) is not -1 || (separatorPos = str.IndexOf('}', cursorPos)) is not -1)
        {
            colors = ParseCollection(str.Substring(cursorPos, separatorPos - cursorPos), ParseIndexedColor);
            cursorPos = separatorPos + 1;
        }

        var scales = Array.Empty<short>();

        if ((separatorPos = str.IndexOf('|', cursorPos)) is not -1 || (separatorPos = str.IndexOf('}', cursorPos)) is not -1)
        {
            scales = ParseCollection(str.Substring(cursorPos, separatorPos - cursorPos), short.Parse);
            cursorPos = separatorPos + 1;
        }

        var subEntities = new List<SubActorLook>();

        while (cursorPos < str.Length && str.Length - cursorPos >= 3)
        {
            var atSeparatorIndex = str.IndexOf('@', cursorPos, 3);
            var equalsSeparatorIndex = str.IndexOf('=', atSeparatorIndex + 1, 3);
            var category = byte.Parse(str.AsSpan(cursorPos, atSeparatorIndex - cursorPos), CultureInfo.InvariantCulture);
            var index = byte.Parse(str.AsSpan(atSeparatorIndex + 1, equalsSeparatorIndex - (atSeparatorIndex + 1)), CultureInfo.InvariantCulture);

            var hookDepth = 0;
            var i = equalsSeparatorIndex + 1;
            var subEntity = new StringBuilder();

            do
            {
                subEntity.Append(str[i]);

                switch (str[i])
                {
                    case '{':
                        hookDepth++;
                        break;
                    case '}':
                        hookDepth--;
                        break;
                }

                i++;
            } while (hookDepth > 0);

            subEntities.Add(new SubActorLook((sbyte)index, (SubEntityBindingPointCategories)category, Parse(subEntity.ToString())));

            cursorPos = i + 1;
        }

        return new ActorLook(bonesId, skins, colors.ToDictionary(static x => x.Item1, static x => Color.FromArgb(x.Item2)), scales, subEntities.ToArray());
    }

    /// <summary>
    /// Parses a string representation of an indexed color.
    /// </summary>
    /// <param name="str">The string representation of the indexed color.</param>
    /// <returns>A <see cref="Tuple{T1, T2}"/> containing the index and color value.</returns>
    private static Tuple<int, int> ParseIndexedColor(string str)
    {
        var signPos = str.IndexOf('=');

        if (signPos is -1)
            return Tuple.Create(0, 0);

        var hexNumber = str[signPos + 1] is '#';
        var index = int.Parse(str[..signPos], CultureInfo.InvariantCulture);
        var color = int.Parse(str.AsSpan(signPos + (hexNumber ? 2 : 1), str.Length - (signPos + (hexNumber ? 2 : 1))), hexNumber ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture);

        return Tuple.Create(index, color);
    }

    /// <summary>
    /// Parses a collection of items from a string using a specified converter function.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="str">The string representation of the collection.</param>
    /// <param name="converter">The function to convert each string item to the desired type.</param>
    /// <returns>An array of items of type <typeparamref name="T"/>.</returns>
    private static T[] ParseCollection<T>(string str, Func<string, T> converter)
    {
        if (string.IsNullOrEmpty(str))
            return [];

        var cursorPos = 0;
        var subseparatorPos = str.IndexOf(',', cursorPos);

        if (subseparatorPos is -1)
            return [converter(str)];

        var collection = new T[str.CountOccurences(',', cursorPos, str.Length) + 1];
        var index = 0;

        while (subseparatorPos is not -1)
        {
            collection[index] = converter(str.Substring(cursorPos, subseparatorPos - cursorPos));
            cursorPos = subseparatorPos + 1;

            subseparatorPos = str.IndexOf(',', cursorPos);
            index++;
        }

        collection[index] = converter(str[cursorPos..]);
        return collection;
    }

    /// <summary>
    /// Retrieves the actor look without its mount.
    /// </summary>
    /// <returns>An <see cref="ActorLook"/> object representing the actor look without the mount.</returns>
    public ActorLook GetLookWithoutMount()
    {
        var ridderLook = GetRiderLook();

        if (ridderLook is null)
            return this;

        ridderLook = ridderLook.Clone();

        var boneId = ridderLook.BonesId;

        switch (boneId)
        {
            case 1084:
                ridderLook.SetBone(44);
                break;
            case 1068:
                ridderLook.SetBone(113);
                break;
            case 1202:
                ridderLook.SetBone(453);
                break;
            case 1575:
            case 1576:
                ridderLook.SetBone(1);
                break;
            case 2456:
                ridderLook.SetBone(1107);
                break;
        }

        return ridderLook;
    }

    /// <summary>
    /// Builds an <see cref="EntityLook"/> object based on the current state of the actor look.
    /// </summary>
    /// <returns>An <see cref="EntityLook"/> object representing the actor look.</returns>
    private EntityLook BuildEntityLook()
    {
        return new EntityLook
        {
            BonesId = BonesId,
            Skins = Skins,
            IndexedColors = Colors.Select(static x => (x.Key << 24) | (x.Value.ToArgb() & 0xFFFFFF)),
            Scales = Scales,
            Subentities = SubLooks.Select(static x => x.GetSubEntity())
        };
    }

    /// <summary>
    /// Retrieves the validated <see cref="EntityLook"/> object for the actor.
    /// </summary>
    /// <returns>An <see cref="EntityLook"/> object representing the actor look.</returns>
    public EntityLook GetEntityLook()
    {
        return EntityLookValidator;
    }
}
