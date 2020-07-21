using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using waRemoteFileSystem.Models;
using waRemoteFileSystem.Utils;

namespace waRemoteFileSystem.Core
{
    public interface IFileSystem
    {
        bool CreateDirectory(string directoryPath);

        bool DirectoryExists(string directoryPath);


        RList GetList(string path);

        bool FileExists(string filePath);

        bool DeleteFile(string filePath);

        Task<bool> CreateFileAsync(IFormFile userfile);

        (byte[] data, string extension, bool res) GetFile(string path);

        string GetFileCrc32(string path);

        void RenameFile(string OldFileName, string NewFileName);
        void RenameDirectory(string OldFileName, string NewFileName);
    }

    public class FileSystemService : IFileSystem
    {
        private string RootPath;
        private IConfiguration Conf { get; }

        public FileSystemService(IConfiguration _conf)
        {
            Conf = _conf;
            RootPath = Conf.GetSection("SystemParams:RemoteFilesPath").Value;
        }


        public bool CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(Path.Combine(RootPath, directoryPath)))
            {
                Directory.CreateDirectory(Path.Combine(RootPath, directoryPath));
                return true;
            }

            return false;
        }

        public async Task<bool> CreateFileAsync(IFormFile userfile)
        {
            using (var fileStream = new FileStream(Path.Combine(RootPath, userfile.FileName), FileMode.Create))
            {
                await userfile.CopyToAsync(fileStream);
                return true;
            }
        }

        public bool DeleteFile(string filePath)
        {
            if (!File.Exists(Path.Combine(RootPath, filePath)))
            {
                File.Create(Path.Combine(RootPath, filePath));
                return true;
            }

            return false;
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(Path.Combine(RootPath, directoryPath));
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(Path.Combine(RootPath, filePath));
        }

        public (byte[] data, string extension, bool res) GetFile(string path)
        {
            var fi = new FileInfo(Path.Combine(RootPath, path));

            if (fi.Exists)
            {
                return (File.ReadAllBytes(Path.Combine(RootPath, path)), fi.Extension, true);
            }
            else
            {
                return (null, "", false);
            }
        }

        public RList GetList(string path)
        {
            RList ls = new RList();

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(RootPath, path));

            foreach (var i in dirInfo.GetDirectories())
            {
                ls.Directories.Add(new RDirectory() { Name = i.Name, CreateDate = i.CreationTime, UpdateDate = i.LastWriteTime });
            }

            foreach (var i in dirInfo.GetFiles())
            {
                ls.Files.Add(new RFile() { Name = i.Name, Type = i.Extension, Size = i.Length, CreateDate = i.CreationTime, UpdateDate = i.LastWriteTime });
            }

            return ls;
        }

        public string GetFileCrc32(string path)
        {
            var pt = Path.Combine(RootPath, path);
            return CCRC32.GetCRC32File(pt);
        }

        public void RenameFile(string OldFileName, string NewFileName)
        {
            if (File.Exists(Path.Combine(RootPath, OldFileName)))
                File.Move(Path.Combine(RootPath, OldFileName), Path.Combine(RootPath, NewFileName));
        }

        public void RenameDirectory(string OldFileName, string NewFileName)
        {
            if (Directory.Exists(Path.Combine(RootPath, OldFileName)))
                Directory.Move(Path.Combine(RootPath, OldFileName), Path.Combine(RootPath, NewFileName));
        }
    }
}
