using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class WordFactory
{
    public enum Category { fruit }

    private static StoreData _storeData;
    public static StoreData StoreData
    {
        get
        {
            if (_storeData == null)
            {
                var jsonStoreData = Resources.Load<TextAsset>("word.store");
                _storeData = JsonUtility.FromJson<StoreData>(jsonStoreData.text);
            }
            return _storeData;
        }
    }

    public static WordData GetWord()
    {
        var categoryData = StoreData.word_store.Find(e => e.category == User.category);

        if (User.wordIndex >= categoryData.words.Count)
        {
            User.wordIndex = 0;
        }
        var wordData = categoryData.words[User.wordIndex];
        return wordData;
    }
}