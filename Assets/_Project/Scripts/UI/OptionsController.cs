using FightingRivals.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace FightingRivals.UI
{
    // Panel de Opciones del menú principal: volumen de música y de efectos.
    // Los valores se guardan en el equipo (PlayerPrefs) y se aplican en todas las escenas.
    public class OptionsController : MonoBehaviour
    {
        public Slider musicSlider;
        public Slider sfxSlider;

        private void Start()
        {
            if (musicSlider != null)
            {
                musicSlider.SetValueWithoutNotify(AudioManager.MusicVolume);
            }

            if (sfxSlider != null)
            {
                sfxSlider.SetValueWithoutNotify(AudioManager.SfxVolume);
            }
        }

        public void SetMusicVolume(float volume)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMusicVolume(volume);
            }
            else
            {
                PlayerPrefs.SetFloat(AudioManager.MusicVolumeKey, volume);
            }
        }

        public void SetSfxVolume(float volume)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSfxVolume(volume);
            }
            else
            {
                PlayerPrefs.SetFloat(AudioManager.SfxVolumeKey, volume);
            }
        }
    }
}
