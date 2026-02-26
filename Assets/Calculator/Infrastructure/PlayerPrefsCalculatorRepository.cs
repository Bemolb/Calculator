using Calculator.Domain;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Calculator.Infrastructure
{
    [Serializable]
    public class SaveData
    {
        public string CurrentInput = string.Empty;
        public List<string> History = new();
    }

    public class PlayerPrefsCalculatorRepository : ICalculatorRepository
    {
        private const string KEY = "Calculator/StateHistory";

        public (string currentInput, List<string> history) LoadState()
        {
            if (!PlayerPrefs.HasKey(KEY)) 
                return (string.Empty, new List<string>());

            var json = PlayerPrefs.GetString(KEY);
            var data = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();

            var history = data.History ?? new List<string>();

            return (data.CurrentInput, history);
        }

        public void SaveState(string currentInput, List<string> history)
        {
            var data = new SaveData
            {
                CurrentInput = currentInput ?? string.Empty,
                History = history ?? new List<string>()
            };

            PlayerPrefs.SetString(KEY, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }
    }
}