using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Drag the slider here in the inspector
    [SerializeField] private AudioSource backgroundAudioSource; // Drag your background AudioSource here

    private const string VolumePrefKey = "Volume";

    private void Start()
    {
        // Load saved volume or set default
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
        volumeSlider.value = savedVolume;
        backgroundAudioSource.volume = savedVolume;

        // Add listener to slider
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
    }

    private void OnVolumeSliderChanged(float value)
    {
        // Update background music volume
        backgroundAudioSource.volume = value;

        // Save the volume setting
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

   public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
