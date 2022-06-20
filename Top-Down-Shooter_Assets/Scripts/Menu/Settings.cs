using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle fullscreen_toggle;
    private Resolution[] rsl;
    private List<string> resolutions;
    public Dropdown resolution_dropdown;


    public void Awake()
    {
        // ���������� ���������� ������� 
        fullscreen_toggle.isOn = Screen.fullScreen; 
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (Resolution i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        resolution_dropdown.ClearOptions();
        resolution_dropdown.AddOptions(resolutions);
    }

    public void quality(int qual) // ��������� ��������
    {
        QualitySettings.SetQualityLevel(qual);
    }
    public void fullScreenToggle() // ��������� �������������� ������
    {
        Screen.fullScreen = fullscreen_toggle.isOn;
    }

    public void resolution(int r) // ��������� ����������
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, fullscreen_toggle.isOn);
    }
}
