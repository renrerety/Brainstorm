using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;
    public string group;
    private void Start()
    {
        /*if (!PlayerPrefs.HasKey(group))
        {
            PlayerPrefs.SetFloat(group, 1);
        }
        else
        {
            Load();
        }*/
        Load();
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
    public void Load()
    {
        if (PlayerPrefs.HasKey(group))
        {
            slider.value = PlayerPrefs.GetFloat(group);
        }
        else
        {
            slider.value = 1;
        }
        audioMixer.SetFloat(group, Mathf.Clamp((Mathf.Log10(slider.value) * 20),-79,0));
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
