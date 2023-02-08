using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string group;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey(group))
        {
            PlayerPrefs.SetFloat(group, 1);
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        audioMixer.SetFloat(group, Mathf.Clamp((Mathf.Log10(slider.value) * 20),-79,0));
        Save();
    }
    void Save()
    {
        PlayerPrefs.SetFloat(group, slider.value);
    }
    void Load()
    {
        slider.value = PlayerPrefs.GetFloat(group);
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
