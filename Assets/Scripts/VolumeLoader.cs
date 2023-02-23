using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeLoader : MonoBehaviour
{
    [SerializeField] VolumeSlider[] sliders = new VolumeSlider[3];
    // Start is called before the first frame update
    void Start()
    {
        foreach (VolumeSlider slider in sliders)
        {
            slider.Load();
        }
    }
}
