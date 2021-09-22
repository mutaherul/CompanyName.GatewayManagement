using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompanyName.GatewayManagement.Api.Helper
{
    public static class FormFileHelper
    {
        public static bool IsValidSize(IFormFile formFile, long maxLengthMB)
        {
            if (formFile == null) return false;

            return maxLengthMB >= ((formFile.Length / 1024.0) / 1024.0);
        }

        public static bool IsValidSize(List<IFormFile> formFiles, long maxLengthMB)
        {
            bool isValid = false;
            foreach (var formFile in formFiles)
            {
                if (formFile != null)
                    isValid = maxLengthMB >= ((formFile.Length / 1024.0) / 1024.0);
            }

            return isValid;
        }

        public static bool IsValidExtension(IFormFile formFile, params string[] fileTypes)
        {
            if (formFile == null) return false;

            return fileTypes.Any(type => formFile.FileName.ToLower().EndsWith(type));
        }

        public static bool IsValidExtension(List<IFormFile> formFiles, params string[] fileTypes)
        {
            bool isValid = false;

            foreach (var formFile in formFiles)
            {
                if (formFile != null)
                    isValid = fileTypes.Any(type => formFile.FileName.EndsWith(type));
            }

            return isValid;
        }

        public static bool IsValidImage(IFormFile formFile, params string[] fileTypes)
        {
            if (formFile == null) return false;

            Dictionary<string, byte[]> imageHeader = new Dictionary<string, byte[]>
            {
                { "jpg", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 } },
                { "png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
                { "bmp", new byte[] { 0x42, 0x4D } }
            };

            byte[] validImageHeader = imageHeader[fileTypes.FirstOrDefault(type => formFile.FileName.EndsWith(type))];

            using (var stream = formFile.OpenReadStream())
            {
                var imageHeaderContainer = new byte[validImageHeader.Length];
                stream.Read(imageHeaderContainer, 0, validImageHeader.Length);
                if (CompareArray(imageHeaderContainer, validImageHeader)) return true;
            }

            return false;
        }
        private static bool CompareArray(byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
                return false;

            return first.SequenceEqual(second);
        }

        public static void SaveTo(IFormFile formFile, string folderPath, string fileName)
        {
            fileName ??= formFile.FileName;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            using var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create);
            formFile.CopyTo(fileStream);
        }

        public static void SaveTo(Stream stream, string folderPath, string fileName)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            using var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create);
            stream.CopyTo(fileStream);
        }

        public static string GetSystemDirectory()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                return Environment.GetEnvironmentVariable("HOME");

            return Environment.SystemDirectory;
        }
    }
}
