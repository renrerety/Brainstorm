using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Localization
{
    public class GameLanguage : MonoBehaviour
    {
        public static GameLanguage instance;

        public Languages gameLanguage;

        public Languages[] languagesRef =
            { Languages.EN, Languages.FR, Languages.DE, Languages.ES, Languages.PO, Languages.CH, Languages.RU };
        void Awake()
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

        private void Start()
        {
            if (PlayerPrefs.HasKey("Language"))
            {
                string languageString = PlayerPrefs.GetString("Language");
                gameLanguage = ConvertStringToLanguage(languageString);
            }
            else
            {
                SetLanguageToSystemLanguage();
            }

            foreach (TextTranslator tt in  GameObject.FindObjectsOfType<TextTranslator>())
            {
                tt.Translate();
            }
        }

        public void ChangeGameLanguage(int val)
        {
            gameLanguage = languagesRef[val];

            foreach (TextTranslator tt in GameObject.FindObjectsOfType<TextTranslator>())
            {
                string text = Localization.instance.GetString(tt.key);
                tt.Translate();
            }
            PlayerPrefs.SetString("Language",gameLanguage.ToString());
            PlayerPrefs.SetInt("Dropdown",val);
            PlayerPrefs.Save();
        }

        private void SetLanguageToSystemLanguage()
        {
            Debug.Log("Set to system language");
            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    gameLanguage = Languages.EN;
                    break;
                case SystemLanguage.French:
                    gameLanguage = Languages.FR;
                    break;
                case SystemLanguage.German:
                    gameLanguage = Languages.DE;
                    break;
                case SystemLanguage.Spanish:
                    gameLanguage = Languages.ES;
                    break;
                case SystemLanguage.Portuguese:
                    gameLanguage = Languages.PO;
                    break;
                case SystemLanguage.ChineseSimplified:
                    gameLanguage = Languages.CH;
                    break;
                case SystemLanguage.Russian:
                    gameLanguage = Languages.RU;
                    break;
                default:
                    gameLanguage = Languages.EN;
                    break;
            }
        }

        private Languages ConvertStringToLanguage(string languageString)
        {
            switch (languageString)
            {
                case "EN":
                   return Languages.EN;
                    break;
                case "FR":
                    return Languages.FR;
                    break;
                case "DE":
                    return Languages.DE;
                    break;
                case "ES":
                    return Languages.ES;
                    break;
                case "PO":
                    return Languages.PO;
                    break;
                case "CH":
                    return Languages.CH;
                    break;
                case "RU":
                    return Languages.RU;
                    break;
                default:
                    return Languages.EN;
                    break;
            }
        }
    }
}
