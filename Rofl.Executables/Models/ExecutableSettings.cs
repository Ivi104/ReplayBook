﻿using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Rofl.Executables.Utilities;

namespace Rofl.Executables.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class ExecutableSettings
    {
        public ExecutableSettings()
        {
            DefaultLocale = LeagueLocale.EnglishUS;
            Executables = new ObservableCollection<LeagueExecutable>();
            SourceFolders = new ObservableCollection<string>();
        }

        [JsonProperty("defaultLocale")]
        public LeagueLocale DefaultLocale { get; set; }

        [JsonProperty("executables")]
        public ObservableCollection<LeagueExecutable> Executables { get; private set; }

        [JsonProperty("sourceFolders")]
        public ObservableCollection<string> SourceFolders { get; private set; }

    }
}
