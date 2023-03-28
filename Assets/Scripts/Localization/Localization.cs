using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Localization
{
    public class Localization : MonoBehaviour
    {
        public static Localization instance;

        [SerializeField] Dictionary<string, LanguageData> strings = new Dictionary<string, LanguageData>();

        private string path = Path.Combine(Application.streamingAssetsPath, "strings.tsv");

        private void Start()
        {
            string[] text = File.ReadAllLines(path);
            

            foreach (string data in text)
            {
                string[] datas = data.Split("	");
                LanguageData languageData = new LanguageData(datas[0],datas[1] ,datas[2],datas[3],datas[4],datas[5],datas[6],datas[7]);
                strings.Add(datas[0],languageData);
            }
        }
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else if (instance == null)
            {
                instance = this;
            }
        }
        public string GetString(string stringKey)
        {
            foreach (var key in strings.Keys)
            {
                if (key.Equals(stringKey))
                {
                    switch (GameLanguage.instance.gameLanguage)
                    {
                        case Languages.EN:
                            return strings[key].en;
                            break;
                        case Languages.FR:
                            return strings[key].fr;
                            break;
                        case  Languages.DE:
                            return strings[key].de;
                            break;
                        case Languages.ES:
                            return strings[key].es;
                            break;
                        case Languages.PO:
                            return strings[key].po;
                            break;
                        case Languages.CH:
                            return strings[key].ch;
                        case Languages.RU:
                            return strings[key].ru;
                            break;
                    }
                }
            }
            return "null";
        }
    }
    public enum Languages
    {
        EN,
        FR,
        DE,
        ES,
        PO,
        CH,
        RU
    }
}