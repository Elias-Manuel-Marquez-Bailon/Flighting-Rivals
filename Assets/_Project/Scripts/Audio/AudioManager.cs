using UnityEngine;

namespace FightingRivals.Audio
{
    // Reproduce la música de la escena y los efectos de sonido.
    // Hay uno en cada escena (en el objeto GameSystems).
    //
    // PARA EL EQUIPO DE AUDIO:
    //   - Arrastra la música de la escena al campo "Music".
    //   - "Ui Click Clip" suena al pulsar botones del menú.
    //   - Los sonidos de golpes, saltos, etc. se asignan en FighterAudio (en cada luchador)
    //     y en los archivos de ataque (Data/Combat).
    //   - Si un campo está vacío simplemente no suena nada; no da error.
    public class AudioManager : MonoBehaviour
    {
        public const string MusicVolumeKey = "MusicVolume";
        public const string SfxVolumeKey = "SfxVolume";
        private const float DefaultMusicVolume = 0.6f;
        private const float DefaultSfxVolume = 0.8f;

        public static AudioManager Instance { get; private set; }

        [Header("Música")]
        public AudioClip music;

        [Header("Interfaz")]
        public AudioClip uiClickClip;

        private AudioSource musicSource;
        private AudioSource sfxSource;

        public static float MusicVolume => PlayerPrefs.GetFloat(MusicVolumeKey, DefaultMusicVolume);
        public static float SfxVolume => PlayerPrefs.GetFloat(SfxVolumeKey, DefaultSfxVolume);

        private void Awake()
        {
            Instance = this;

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;

            ApplyVolumes();
        }

        private void Start()
        {
            if (music != null)
            {
                musicSource.clip = music;
                musicSource.Play();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void PlaySfx(AudioClip clip)
        {
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayUIClick()
        {
            PlaySfx(uiClickClip);
        }

        public void SetMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, Mathf.Clamp01(volume));
            PlayerPrefs.Save();
            ApplyVolumes();
        }

        public void SetSfxVolume(float volume)
        {
            PlayerPrefs.SetFloat(SfxVolumeKey, Mathf.Clamp01(volume));
            PlayerPrefs.Save();
            ApplyVolumes();
        }

        private void ApplyVolumes()
        {
            musicSource.volume = MusicVolume;
            sfxSource.volume = SfxVolume;
        }
    }
}
