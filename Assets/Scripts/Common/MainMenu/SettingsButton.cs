using UnityEngine;

namespace ZenoJam.Common
{
    public class SettingsButton : MenuButton 
    {
        [SerializeField] private VolumeSlider _volumeSlider;

        private bool _enabled;

        public void ToggleVolumeSlider() 
        {
            if (_enabled) 
            {
                _enabled = !_enabled;

                _volumeSlider.AnimateOut();
            }
            else 
            {
                _enabled = !_enabled;
                _volumeSlider.gameObject.SetActive(true);

                _volumeSlider.AnimateIn();
            }
        }
    }
}
