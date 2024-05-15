using System;
using System.Collections.Generic;


[Serializable]
public class StoreData
{
    public List<string> category;
    public List<WordStoreData> word_store;
}

[Serializable]
public class WordStoreData
{
    public string category;
    public List<WordData> words;
}

[Serializable]
public class WordData
{
    public string en;
    public string th;
    public string info;
}

