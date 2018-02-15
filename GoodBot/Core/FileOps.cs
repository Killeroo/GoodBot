using System;
using System.Net;
using System.Collections.Generic;
using System.IO;

using GoodBot.Logger;
using GoodBot.Net;

namespace GoodBot.Core
{
    /// <summary>
    /// Stores static helper methods for file operations 
    /// </summary>
    public class FileOps
    {
        private static List<FileInfo> files = new List<FileInfo>();

        /// <summary>
        /// Discover all files at a path put in list
        /// </summary>
        public static List<FileInfo> DiscoverFiles(string path, bool recursing = false)
        {
            if (!recursing)
            {
                // Check path exits
                if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                {
                    Log.Warning("Could not discover files at [" + path + "]");
                    return null;
                }

                // Clear temp list
                files.Clear();

                // Start recursive discovery
                Log.Info("Discovering files at \"" + path + "\"...");
                DiscoverFiles(path, true);
                Log.Info(files.Count.ToString() + " files found");

                return files;
            }
            else
            {
                // Recursive file discovery //
                DirectoryInfo dir = new DirectoryInfo(path);

                try
                {
                    // Get all files at path
                    foreach (FileInfo file in dir.GetFiles())
                        files.Add(file);
                }
                catch (DirectoryNotFoundException e)
                {
                    Log.Error(new Error(e, "Directory not found \"" + path + "\""));
                }

                try
                {
                    // Recurse through each sub directory to find files
                    foreach (DirectoryInfo di in dir.GetDirectories())
                        DiscoverFiles(di.FullName, true);
                }
                catch (DirectoryNotFoundException e)
                {
                    Log.Error(new Error(e, "Directory not found \"" + path + "\""));
                }
                catch (UnauthorizedAccessException e)
                {
                    Log.Error(new Error(e, "Access denied \"" + path + "\""));
                }

                return null;
            }
        }
    }
}
