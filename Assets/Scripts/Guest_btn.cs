using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest_btn : MonoBehaviour
{
    [SerializeField] private LevelData scene;
    [SerializeField] private List<string> labels;
    public void Click()
    {
        SceneLoader.instance.LoadScene(scene,false,labels);
    }
}
