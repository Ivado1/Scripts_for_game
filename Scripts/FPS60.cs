using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class FPS60 : MonoBehaviour
{
    [SerializeField] private Slider FPSslider;
    [SerializeField] private Slider ShadowSlider;
    [SerializeField] private AudioMixer myAudioMixer;

    void Start()
    {
        if (PlayerPrefs.HasKey("fps"))
        {
            FPSslider.value = PlayerPrefs.GetFloat("fps", FPSslider.value);
            if (FPSslider.value == 1)
            {
                Application.targetFrameRate = 60;
                QualitySettings.vSyncCount = 0;
                PlayerPrefs.SetFloat("fps", FPSslider.value);
                Debug.Log(FPSslider.value+" 60fps");
            }
            if (FPSslider.value == 0)
            {
                Application.targetFrameRate = 30;
                QualitySettings.vSyncCount = 0;
                PlayerPrefs.SetFloat("fps", FPSslider.value);
                Debug.Log(FPSslider.value+ " 30fps");
            }
        }
        else
        {
            FPSslider.value = 1;
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetFloat("fps", FPSslider.value);
            //Debug.Log(FPSslider.value + " 30fps");
        }
        if (PlayerPrefs.HasKey("Shadow"))
        {
            ShadowSlider.value = PlayerPrefs.GetFloat("Shadow", ShadowSlider.value);
            if (ShadowSlider.value == 1)
            {
                QualitySettings.shadows = ShadowQuality.HardOnly;
                PlayerPrefs.SetFloat("Shadow", ShadowSlider.value);
                Debug.Log(ShadowSlider.value + " Shadows ON");
            }
            if (ShadowSlider.value == 0)
            {
                QualitySettings.shadows = ShadowQuality.Disable;
                PlayerPrefs.SetFloat("Shadow", ShadowSlider.value);
                Debug.Log(ShadowSlider.value + " Shadows OFF");
            }
        }
    }

    public void SetVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        //myAudioMixer.SetFloat("Master", Math)
    }
    public void SetFPS(float sliderValue)
    {
        if (sliderValue == 1)
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetFloat("fps", FPSslider.value);
            Debug.Log(sliderValue);
        }
        if (sliderValue == 0)
        {
            Application.targetFrameRate = 30;
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetFloat("fps", FPSslider.value);
            Debug.Log(sliderValue);
        }
    }
    public void SetShadow(float sliderValue)
    {
        if (ShadowSlider.value == 1)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            PlayerPrefs.SetFloat("Shadow", ShadowSlider.value);
            Debug.Log(sliderValue);
        }
        if (ShadowSlider.value == 0)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            PlayerPrefs.SetFloat("Shadow", ShadowSlider.value);
            Debug.Log(sliderValue);
        }
    }
}
