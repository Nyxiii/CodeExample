using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AnyListen.Utilities
{
    public static class StringExtensions
    {
        public static string ToSentenceCase(this string s)
        {
            return Regex.Replace(s, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToUpper(m.Value[1]));
        }

        /// <summary>
        /// Remove all invalid chars for a file name
        /// </summary>
        /// <param name="fileNameToEscape">The name of the file which could contain invalid chars</param>
        /// <returns>The <see cref="fileNameToEscape"/> without the invalid chars</returns>
        public static string ToEscapedFilename(this string fileNameToEscape)
        {
            char[] illegalchars