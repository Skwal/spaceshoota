using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.StateManager
{
    public class PlayerSettings : MonoBehaviour
    {
        public float musicVolumeValue, sfxVolumeValue;
        public GameObject musicVolume, sfxVolume, UIElement;

        private Slider musicVolumeSlider, sfxVolumeSlider;
        private AudioSource bgMusic;

        private void Awake()
        {
            if (musicVolume == null)
            {
                musicVolume = GameObject.FindGameObjectWithTag("MusicVolumeSlider");
                sfxVolume = GameObject.FindGameObjectWithTag("SfxVolumeSlider");
                UIElement = GameObject.FindGameObjectWithTag("BgMusicObject");
            }

            musicVolumeSlider = musicVolume.GetComponent<Slider>();
            sfxVolumeSlider = sfxVolume.GetComponent<Slider>();

            bgMusic = UIElement.GetComponent<AudioSource>();
            LoadSettings();
        }

        public void OnMusicVolumeChanged()
        {
            musicVolumeValue = musicVolumeSlider.value;
            bgMusic.volume = musicVolumeValue;
        }

        public void OnSfxVolumeChanged()
        {
            sfxVolumeValue = sfxVolumeSlider.value;
        }

        public void OnSfxVolumeDragEnd()
        {
            AudioSource fx = sfxVolumeSlider.GetComponent<AudioSource>();
            fx.volume = sfxVolumeValue;
            fx.Play();
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("Music Volume", musicVolumeValue);
            PlayerPrefs.SetFloat("SFX Volume", sfxVolumeValue);
            PlayerPrefs.Save();
        }

        private void LoadSettings()
        {
            if (PlayerPrefs.HasKey("Music Volume"))
            {
                musicVolumeValue = PlayerPrefs.GetFloat("Music Volume");
                sfxVolumeValue = PlayerPrefs.GetFloat("SFX Volume");

                musicVolumeSlider.value = musicVolumeValue;
                sfxVolumeSlider.value = sfxVolumeValue;
            }
        }
    }
}