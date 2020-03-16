﻿using Rofl.Files.Models;
using Rofl.Logger;
using Rofl.Settings.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rofl.Files.Repositories
{
    /// <summary>
    /// Watches given folders defined in config file for new replay files
    /// Once a new file is detected, parse the data and place it in the database
    /// (depends on if database supports on update)
    /// ViewModel asks the database(wrapper???) for the replay data
    /// </summary>
    public class FolderRepository
    {

        private readonly ObservableSettings _settings;
        private readonly Scribe _log;

        public FolderRepository(ObservableSettings settings, Scribe log)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Returns full paths of every replay file to show
        /// </summary>
        /// <returns></returns>
        public ReplayFileInfo[] GetAllReplayFileInfo()
        {
            List<ReplayFileInfo> returnList = new List<ReplayFileInfo>();

            foreach (string folder in _settings.SourceFolders)
            {
                // Grab the contents of the folder
                DirectoryInfo dirInfo = new DirectoryInfo(folder);
                var innerFiles = dirInfo.EnumerateFiles();

                foreach (var file in innerFiles)
                {
                    // If the file is not supported, skip it
                    if (!(file.Name.EndsWith(".rofl", StringComparison.OrdinalIgnoreCase) ||
                          file.Name.EndsWith(".lrf", StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    returnList.Add(new ReplayFileInfo()
                    {
                        Path = file.FullName,
                        CreationTime = file.CreationTime,
                        FileSizeBytes = file.Length,
                        Name = Path.GetFileNameWithoutExtension(file.FullName)
                    });
                }
            }

            return returnList.ToArray();
        }
    }
}