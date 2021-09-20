using System;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace AnyListen.Settings.RegistryManager
{
    class RegistryRegister
    {
        /// <summary>
        /// Creates a new item in the windows explorer context menu
        /// </summary>
        /// <param name="extension">The file extension (like .mp3)</param>
        /// <param name="header">The text shown in the context menu</param>
        /// <param name="name">The name of the subkey</param>
        /// <param name="applicationpath">The path with paramters</param>
        /// <param name="iconpath">The path of the icon</param>
        /// <returns>Fals