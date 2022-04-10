using System;
using System.Security.Cryptography;
using System.Text;

namespace AnyListen.Utilities
{
    public class PasswordGenerator
    {
        private static string _handshake;
        public static