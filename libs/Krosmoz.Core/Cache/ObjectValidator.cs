// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Cache;

/// <summary>
/// A thread-safe object validator that ensures the validity of an object instance
/// and recreates it if necessary based on a specified lifetime.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public sealed class ObjectValidator<T>
{
    private readonly Func<T> _creator;
    private readonly TimeSpan? _lifetime;
    private readonly Lock _sync;
    private T? _instance;

    private bool _isValid;
    private DateTime _lastCreationDate;

    /// <summary>
    /// Gets a value indicating whether the current instance is valid.
    /// </summary>
    private bool IsValid =>
        _isValid && (_lifetime is null || DateTime.Now - _lastCreationDate < _lifetime.Value);

    /// <summary>
    /// Event triggered when the object is invalidated.
    /// </summary>
    public event Action<ObjectValidator<T>>? ObjectInvalidated;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectValidator{T}"/> class.
    /// </summary>
    /// <param name="creator">A function to create a new instance of the object.</param>
    /// <param name="lifeTime">Optional lifetime for the object instance.</param>
    public ObjectValidator(Func<T> creator, TimeSpan? lifeTime = null)
    {
        _lifetime = lifeTime;
        _creator = creator;
        _sync = new Lock();
    }

    /// <summary>
    /// Invalidates the current object instance, marking it as no longer valid.
    /// </summary>
    public void Invalidate()
    {
        _isValid = false;
        ObjectInvalidated?.Invoke(this);
    }

    /// <summary>
    /// Implicitly converts the <see cref="ObjectValidator{T}"/> to an instance of type <typeparamref name="T"/>.
    /// If the current instance is invalid, a new instance is created.
    /// </summary>
    /// <param name="validator">The object validator to convert.</param>
    /// <returns>The validated or newly created object instance.</returns>
    public static implicit operator T(ObjectValidator<T> validator)
    {
        if (validator is { IsValid: true, _instance: not null })
            return validator._instance;

        lock (validator._sync)
        {
            if (validator is { IsValid: true, _instance: not null })
                return validator._instance;

            validator._instance = validator._creator();
            validator._lastCreationDate = DateTime.Now;
            validator._isValid = true;
        }

        return validator._instance;
    }
}
