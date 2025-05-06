// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Krosmoz.Core.IO.Cryptography;

/// <summary>
/// Provides functionality for encrypting text using the MD5 hashing algorithm.
/// </summary>
public static class Md5
{
    /// <summary>
    /// Encrypts the specified text using the MD5 hashing algorithm and returns the hash as a lowercase hexadecimal string.
    /// </summary>
    /// <param name="text">The text to be encrypted.</param>
    /// <returns>A lowercase hexadecimal string representing the MD5 hash of the input text.</returns>
    [Pure]
    public static string Encrypt(string text)
    {
        return Convert.ToHexStringLower(MD5.HashData(Encoding.UTF8.GetBytes(text)));
    }
}
