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
         private bool started = false;

        private void Awake()
        {
            text = GetComponent<Text>();
            //text.text = Localization.instance.GetString(key);
        }

        private void Start()
        {
            string txt = Localization.instance.GetString(key);
            text.text = txt;
            started = true;
        }

        private void OnEnable()
        {
            if (started)
            {
                string txt = Localization.instance.GetString(key);
                text.text = txt;
            }
        }

        public void Translate()
        {
            string txt = Localization.instance.GetString(key);
            text.text = txt;
        }
    }
}
