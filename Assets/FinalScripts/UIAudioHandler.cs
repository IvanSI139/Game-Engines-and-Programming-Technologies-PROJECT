using UnityEngine;

public class UIAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        // Optionally auto-find the AudioManager if not assigned
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    public void PlayUISound()
    {
        if (audioManager != null && audioManager.ui != null)
        {
            audioManager.PlaySFX(audioManager.ui); // Calls PlaySFX on your AudioManager
        }
        else
        {
            Debug.LogWarning("AudioManager or UI sound clip is not assigned!");
        }
    }
}
