using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Utility_Library
{
    public class clsCryptography
    {
        private static byte[] SaltForUsernameHash = new byte[16];

        public static string HashWithSaltPassword(string Password, ref byte[] Salt)
        {
            if (Salt == null)
            {
                Salt = new byte[32];

                RandomNumberGenerator rn = RandomNumberGenerator.Create();
                rn.GetBytes(Salt);
                rn.Dispose();
            }

            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(Password, Salt, 50000, HashAlgorithmName.SHA256);
            byte[] HashWithSalt = PBKDF2.GetBytes(32);
            PBKDF2.Dispose();

            return Convert.ToBase64String(HashWithSalt);
        }

        /// <summary>
        /// Hash username to search for the hashed value of it in the database to find specific user when login or when checking for an entered username if it is exists.
        /// </summary>

        public static string HashUsernameForLookUp(string StringToHash)
        {
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(StringToHash, SaltForUsernameHash, 1, HashAlgorithmName.SHA256);
            byte[] HashWithSalt = PBKDF2.GetBytes(16);
            PBKDF2.Dispose();

            return Convert.ToBase64String(HashWithSalt);
        }

        private static void _GetAppriateEnKeySize(ref byte KeySize)
        {
            if (KeySize > 32 || KeySize > 16)
                KeySize = 32;

            else if (KeySize > 8)
                KeySize = 16;

            else
                KeySize = 8;
        }

        /// <summary>
        /// Generate encryption key for one time and store it in credntial manager using CredentialManager class and reuse the encryption key when you want.
        /// </summary>
        public static byte[] GenerateEncryptionKey(byte KeySize)
        {
            _GetAppriateEnKeySize(ref KeySize);
            byte[] EncryptionKey = new byte[KeySize];

            RandomNumberGenerator rn = RandomNumberGenerator.Create();
            rn.GetBytes(EncryptionKey);
            rn.Dispose();

            return EncryptionKey;
        }

        /// <summary>
        /// Generates a new IV for each encrypted field and each time that field's value is updated, following security best practices.
        /// </summary>
        public static byte[] GenerateNewInitializationVector()
        {
            byte[] IV = new byte[16];

            RandomNumberGenerator rn = RandomNumberGenerator.Create();
            rn.GetBytes(IV);
            rn.Dispose();

            return IV;
        }

        public static class CredentialManager
        {
            private const int Credential_GenericType = 1;
            private const int CredentialLocation_LocalMachine = 2;

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            private struct Credential
            {
                public int Flags;
                public int Type;

                public string TargetName;
                public string Comment;

                public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;

                public int CredentialBlobSize;
                public IntPtr CredentialBlob_Ptr;

                public int CredentialPersistLocation;

                public int AttributeCount;
                public IntPtr Attributes;

                public string TargetAlias;
                public string UserName;
            }

            [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern bool CredWrite(ref Credential Credential, uint Flags);

            [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern bool CredRead(string TargetName, int Type, int Flags, out IntPtr Credential);

            [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern bool CredDelete(string TargetName, int Type, int Flags);

            [DllImport("Advapi32.dll")]
            private static extern void CredFree(IntPtr Credential);

            /// <summary>
            /// Store encryption key securly in windows credintial manager one time and re use in application using GetEncryptionKeyMethod.
            /// </summary>
            public static void StoreCredential(string TargetName, byte[] CredentialToStore)
            {
                IntPtr CredentialBlob_Ptr = Marshal.AllocCoTaskMem(CredentialToStore.Length);

                Marshal.Copy(CredentialToStore, 0, CredentialBlob_Ptr, CredentialToStore.Length);

                Credential credential = new Credential
                {
                    Type = Credential_GenericType,
                    TargetName = TargetName,
                    CredentialBlobSize = CredentialToStore.Length,
                    CredentialBlob_Ptr = CredentialBlob_Ptr,
                    CredentialPersistLocation = CredentialLocation_LocalMachine
                };

                if (!CredWrite(ref credential, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                Marshal.FreeCoTaskMem(CredentialBlob_Ptr);
            }

            public static void StoreCredential(string TargetName, string Credential)
            {
                byte[] CredentialByteArray = Encoding.UTF8.GetBytes(Credential);
                StoreCredential(TargetName, CredentialByteArray);
            }

            public static bool GetStoredCredential(string TargetName, out byte[] Credential)
            {
                IntPtr credentialPointer;

                if (!CredRead(TargetName, Credential_GenericType, 0, out credentialPointer))
                {
                    Credential = new byte[] { };
                    return false;
                }

                Credential credential = Marshal.PtrToStructure<Credential>(credentialPointer);

                Credential = new byte[credential.CredentialBlobSize];

                Marshal.Copy(credential.CredentialBlob_Ptr, Credential, 0, credential.CredentialBlobSize);

                CredFree(credentialPointer);

                return true;
            }

            public static string GetStoredCredential(string TargetName)
            {
                if (GetStoredCredential(TargetName, out byte[] StoredCredential))
                {
                    return Encoding.UTF8.GetString(StoredCredential);
                }

                else
                    return null;
            }

            public static bool DeleteStoredCredential(string TargetName)
            {
                return CredDelete(TargetName, Credential_GenericType, 0);
            }
        }

        public static string EncryptString(byte[] EncryptionKey, string StringToEncrypt, out byte[] IV)
        {
            byte[] SendedStringInBytes = Encoding.UTF8.GetBytes(StringToEncrypt);

            Aes aes = Aes.Create();
            aes.Key = EncryptionKey;

            IV = GenerateNewInitializationVector();
            aes.IV = IV;

            ICryptoTransform Encryptor = aes.CreateEncryptor();
            aes.Dispose();

            byte[] EncryptedString = Encryptor.TransformFinalBlock(SendedStringInBytes, 0, SendedStringInBytes.Length);
            Encryptor.Dispose();

            return Convert.ToBase64String(EncryptedString);
        }

        public static string DecryptString(byte[] EncryptionKey, string StringToDecrypt, string IV)
        {
            byte[] EncryptedString = Convert.FromBase64String(StringToDecrypt);

            Aes aes = Aes.Create();
            aes.Key = EncryptionKey;

            byte[] OriginalIV = Convert.FromBase64String(IV);
            aes.IV = OriginalIV;

            ICryptoTransform Decryptor = aes.CreateDecryptor();
            aes.Dispose();

            byte[] DecryptedString = Decryptor.TransformFinalBlock(EncryptedString, 0, EncryptedString.Length);
            Decryptor.Dispose();

            return Encoding.UTF8.GetString(DecryptedString);
        }

    }
}
