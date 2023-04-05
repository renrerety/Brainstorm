using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerData : MonoBehaviour
{
    [SerializeField] private Text goldText;
    // Start is called before the first frame update
    void Start()
    {
        BinarySaveFormatter.Deserialize();
        goldText.text = PlayerData.instance.persistentData.gold.ToString();
    }
}
