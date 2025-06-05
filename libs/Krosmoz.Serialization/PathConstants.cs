// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization;

/// <summary>
/// Provides constants for directory and file paths.
/// </summary>
public static class PathConstants
{
    /// <summary>
    /// Contains constants for various directory paths.
    /// </summary>
    public static class Directories
    {
        /// <summary>
        /// The base directory path.
        /// </summary>
        public static readonly string DofusPath;

        /// <summary>
        /// The path to the content directory.
        /// </summary>
        public static readonly string ContentPath;

        /// <summary>
        /// The path to the data directory.
        /// </summary>
        public static readonly string DataPath;

        /// <summary>
        /// The path to the source directory.
        /// </summary>
        public static readonly string SourcePath;

        /// <summary>
        /// The path to the registry directory.
        /// </summary>
        public static readonly string RegPath;

        /// <summary>
        /// The path to the binds directory.
        /// </summary>
        public static readonly string BindsPath;

        /// <summary>
        /// The path to the common data directory.
        /// </summary>
        public static readonly string CommonPath;

        /// <summary>
        /// The path to the i18n directory.
        /// </summary>
        public static readonly string I18NPath;

        /// <summary>
        /// The path to the maps directory.
        /// </summary>
        public static readonly string MapsPath;

        /// <summary>
        /// The path to the tiles directory.
        /// </summary>
        public static readonly string TilesPath;

        /// <summary>
        /// The path to the audio directory.
        /// </summary>
        public static readonly string AudioPath;

        /// <summary>
        /// Initializes the directory paths.
        /// </summary>
        static Directories()
        {
            DofusPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Krosmoz");
            ContentPath = Path.Combine(DofusPath, "content");
            DataPath = Path.Combine(DofusPath, "data");
            SourcePath = Path.Combine(DofusPath, "src");
            RegPath = Path.Combine(DofusPath, "reg");
            BindsPath = Path.Combine(DataPath, "binds");
            CommonPath = Path.Combine(DataPath, "common");
            I18NPath = Path.Combine(DataPath, "i18n");
            MapsPath = Path.Combine(ContentPath, "maps");
            TilesPath = Path.Combine(ContentPath, "gfx", "world");
            AudioPath = Path.Combine(RegPath, "content", "audio");
        }
    }

    /// <summary>
    /// Contains constants for various file paths.
    /// </summary>
    public static class Files
    {
        /// <summary>
        /// The path to the elements file.
        /// </summary>
        public static readonly string ElementsPath;

        /// <summary>
        /// The path to the D2I file for French localization.
        /// </summary>
        public static readonly string D2IFrPath;

        /// <summary>
        /// The path to the Dofus executable file.
        /// </summary>
        public static readonly string DofusExecutablePath;

        /// <summary>
        /// The path to the Reg executable file.
        /// </summary>
        public static readonly string RegExecutablePath;

        /// <summary>
        /// Initializes the file paths.
        /// </summary>
        static Files()
        {
            ElementsPath = Path.Combine(Directories.MapsPath, "elements.ele");
            D2IFrPath = Path.Combine(Directories.I18NPath, "i18n_fr.d2i");
            DofusExecutablePath = Path.Combine(Directories.DofusPath, "Dofus.exe");
            RegExecutablePath = Path.Combine(Directories.RegPath, "Reg.exe");
        }
    }
}
