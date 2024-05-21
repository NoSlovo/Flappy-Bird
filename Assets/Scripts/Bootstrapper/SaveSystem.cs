using UnityEngine;

namespace Bootstrapper
{
    public struct SaveSystem
    {
        private const string _keySave = "SaveBestScore";

        public static void SaveValue(int score)
        {
            var lasScore = PlayerPrefs.GetInt(_keySave);

            if (score > lasScore)
                PlayerPrefs.SetInt(_keySave, score);
        }

        public static int GetBestScore()
        {
            var loadValue = PlayerPrefs.GetInt(_keySave);
            return loadValue;
        }
    }
}