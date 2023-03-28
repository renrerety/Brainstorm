using System;

namespace Localization
{
    [Serializable]
    public class LanguageData
    {
        public string key;
        public string en;
        public string fr;
        public string de;
        public string es;
        public string po;
        public string ch;
        public string ru;
        public LanguageData(string key, string en,string fr,string de,string es,string po,string ch,string ru)
        {
            this.key = key;
            this.en = en;
            this.fr = fr;
            this.de = de;
            this.es = es;
            this.po = po;
            this.ch = ch;
            this.ru = ru;
        }
    }
    [Serializable]
    public class StringsData
    {
        public LanguageData[] strings;
    }
}