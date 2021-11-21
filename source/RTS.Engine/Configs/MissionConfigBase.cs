﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MissionSharedLibrary.Utilities;

namespace MissionSharedLibrary.Config
{
    public static class ConfigPath
    {
        private static string ApplicationName = "Mount and Blade II Bannerlord";
        public static string ConfigDir { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ApplicationName, "Configs");
    }

    // Use type T to identify different configs.
    public abstract class MissionConfigBase<T> where T : MissionConfigBase<T>
    {
        public static T Instance { get; set; }

        public static T Get()
        {
            if (Instance == null)
            {
                Instance = Activator.CreateInstance<T>();
                Instance.SyncWithSave();
            }

            return Instance;
        }

        public static void Clear()
        {
            Instance = null;
        }

        protected abstract void CopyFrom(T other);
        protected abstract void UpgradeToCurrentVersion();
        protected virtual XmlSerializer Serializer { get; } = new XmlSerializer(typeof(T));

        public virtual bool Serialize()
        {
            try
            {
                EnsureParentDirectory();
                XmlSerializer serializer = Serializer;

                using (TextWriter writer = new StreamWriter(SaveName))
                {
                    serializer.Serialize(writer, this);
                }

                return true;
            }
            catch (Exception e)
            {
                Utility.DisplayMessage(e.ToString());
                Console.WriteLine(e);
            }

            return false;
        }

        public virtual bool Deserialize()
        {
            try
            {
                EnsureParentDirectory();
                XmlSerializer deserializer = Serializer;
                using (TextReader reader = new StreamReader(SaveName))
                {
                    var config = (T)deserializer.Deserialize(reader);
                    CopyFrom(config);
                }
                UpgradeToCurrentVersion();
                return true;
            }
            catch (Exception e)
            {
                Utility.DisplayMessage(e.ToString());
                Console.WriteLine(e);
            }

            return false;
        }

        protected void SyncWithSave()
        {
            if (File.Exists(SaveName) && Deserialize())
                return;

            ResetToDefault();
            Serialize();
        }

        public void ResetToDefault()
        {
            CopyFrom(Activator.CreateInstance<T>());
        }

        protected abstract string SaveName { get; }

        protected void EnsureParentDirectory()
        {
            var directory = Path.GetDirectoryName(SaveName);
            if (directory != null)
                Directory.CreateDirectory(directory);
        }
    }
}
