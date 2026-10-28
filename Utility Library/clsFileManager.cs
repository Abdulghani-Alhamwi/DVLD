using System;
using System.IO;
using System.Windows.Forms;

namespace Utility_Library
{
    public class clsFileManager
    {
        public static bool CreateDirectoryIfNotExists(string CreationPath)
        {
            if(!File.Exists(CreationPath))
            {
                try
                {
                    Directory.CreateDirectory(CreationPath);
                }

                catch(Exception e)
                {
                    MessageBox.Show(e.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
                
            return true;
        }

        public static string ReplaceFileNameWithGuid(string FileName)
        {
            FileInfo fileInfo = new FileInfo(FileName);
            return clsUtility.GenerateGuid() + fileInfo.Extension;
        }

        public static bool CopyImageToProjectImageFolder(string ProjectImageFolderPath,ref string ImageSourcePath)
        {
            if (!CreateDirectoryIfNotExists(ProjectImageFolderPath))
            {
                return false;
            }

            string DestinationFilePath = ProjectImageFolderPath + ReplaceFileNameWithGuid(ImageSourcePath);

            try
            {
                File.Copy(ImageSourcePath, DestinationFilePath, true);
            }

            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ImageSourcePath = DestinationFilePath;
            return true;
        }
    }
}
