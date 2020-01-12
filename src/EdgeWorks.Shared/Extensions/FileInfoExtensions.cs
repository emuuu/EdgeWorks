using System;
using System.IO;
using System.Security.Cryptography;

namespace EdgeWorks.Shared.Extensions
{
    public static class FileInfoExtensions
    {
        public static string CalculateMD5(this FileInfo file)
        {
            if (!File.Exists(file.FullName))
            {
                return null;
            }
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static string CalculateSHA256(this FileInfo file)
        {
            if (!File.Exists(file.FullName))
            {
                return null;
            }
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
