using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ZenoJam.Common
{
    public class DialogueWindow : UIElement 
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _sentence;
        [SerializeField] private Image _nextSentenceImage;

        [SerializeField] private List<AudioClip> _clips;
        [SerializeField] private AudioSource _audioSource;

        private Vector2 _inPosition = new Vector2(0, 60);
        private Vector2 _outPosition = new Vector2(0, -400);

        private RectTransform _rectTransform;

        public void UpdateName(string name) => _name.text = name;

        public void UpdateSentence(string sentence) => _sentence.text = sentence;

        public void ToggleNextSentenceImage(bool value) 
        {
            if (value)
            {
                var clip = _clips[Random.Range(0, _clips.Count)];
                _audioSource.clip = clip;
                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.Play();
            }
            else 
            {
                _audioSource.Stop();
            }

            _nextSentenceImage.gameObject.SetActive(!value); 
        }

        public void Clear() 
        {
            _name.text = string.Empty;
            _sentence.text = string.Empty;
        }

        public override void Activate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            animator.Slide(_rectTransform, _inPosition);
        }

        public override void Deactivate()
        {
            animator.Slide(_rectTransform, _outPosition);

            DeactivateSelfAsync().Forget();
        }

        private async UniTask DeactivateSelfAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            gameObject.SetActive(false);
        }
    }
}
