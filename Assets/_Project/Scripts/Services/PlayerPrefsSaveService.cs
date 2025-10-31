using UnityEngine;
using System;

namespace Project.Services.SaveSystem
{
    public class PlayerPrefsSaveService : ISaveService
    {
        public void Save<T>(string key, T value)
        {
            switch (value)
            {
                case int i:
                    PlayerPrefs.SetInt(key, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(key, f);
                    break;
                case string s:
                    PlayerPrefs.SetString(key, s);
                    break;
                case bool b:
                    PlayerPrefs.SetInt(key, b ? 1 : 0);
                    break;
                default:
                    var json = JsonUtility.ToJson(value);
                    PlayerPrefs.SetString(key, json);
                    break;
            }
            PlayerPrefs.Save();
            Debug.Log($"[SaveService] Saved key '{key}' ({typeof(T).Name})");
        }

        public T Load<T>(string key, T defaultValue = default)
        {
            if (!PlayerPrefs.HasKey(key))
                return defaultValue;

            object result = typeof(T) switch
            {
                Type t when t == typeof(int) => PlayerPrefs.GetInt(key),
                Type t when t == typeof(float) => PlayerPrefs.GetFloat(key),
                Type t when t == typeof(string) => PlayerPrefs.GetString(key),
                Type t when t == typeof(bool) => PlayerPrefs.GetInt(key) == 1,
                _ => JsonUtility.FromJson<T>(PlayerPrefs.GetString(key))
            };

            return (T)result;
        }

        public bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log($"[SaveService] Deleted key '{key}'");
        }

        public void ClearAll()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("[SaveService] Cleared all PlayerPrefs data");
        }
    }
}
