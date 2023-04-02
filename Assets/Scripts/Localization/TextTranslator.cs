using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class TextTranslator : MonoBehaviour
    {
        [SerializeField] public string key;
         public Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            //text.text = Localization.instance.GetString(key);
        }

        private void OnEnable()
        {
            string txt = Localization.instance.GetString(key);
            text.text = txt;
        }

        public void Translate()
        {
            string txt = Localization.instance.GetString(key);
            text.text = txt;
        }
    }
}
