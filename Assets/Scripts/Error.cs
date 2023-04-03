using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Error : MonoBehaviour
{
   public static Error instance;

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

   public GameObject panel;
   public TMP_Text message;
   
   public void DisplayError(string message)
   {
      this.message.text = message;
      panel.SetActive(true);
   }
}
