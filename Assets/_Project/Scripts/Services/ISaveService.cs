namespace Project.Services.SaveSystem
{
    public interface ISaveService
    {
        /// <summary>
        /// Saves a value with the specified key.
        /// </summary>
        void Save<T>(string key, T value);

        /// <summary>
        /// Loads a value by key, or returns a default value.
        /// </summary>
        T Load<T>(string key, T defaultValue = default);

        /// <summary>
        /// Checks if a key exists in the storage.
        /// </summary>
        bool HasKey(string key);

        /// <summary>
        /// Deletes a key.
        /// </summary>
        void Delete(string key);

        /// <summary>
        /// Clears all saves.
        /// </summary>
        void ClearAll();
    }
}
