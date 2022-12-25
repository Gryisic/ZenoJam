using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    [System.Serializable]
    public class DialogueProvider
    {
        [SerializeField] private List<Monologue> _mainMonologues;
        [SerializeField] private List<Monologue> _additionalMonologues;

        private IEnumerable<MonologueData> _nextMonologues;

        private int _additionalMonologuesIndex = 0;

        public IEnumerable<MonologueData> NextMonologues => _nextMonologues;

        public Transform Transform { get; private set; }

        public void SetListened() => _mainMonologues.First(m => m.CanBeListened).SetListened();

        public void SetTransform(Transform transform) => Transform = transform;

        public bool ContainsListenableMonologues(MonologueType type) 
        {
            switch (type)
            {
                case MonologueType.Main:
                    var listenableMain = _mainMonologues.Where(d => d.CanBeListened);

                    if (listenableMain.Count() > 0)
                    {
                        _nextMonologues = listenableMain.First().Data;
                        return true;
                    }

                    break;

                case MonologueType.Additional:
                    var listenableAdditional = _additionalMonologues.Where(d => d.CanBeListened).ToList();

                    if (listenableAdditional.Count() > 0)
                    {
                        if (_additionalMonologuesIndex >= listenableAdditional.Count())
                            _additionalMonologuesIndex = 0;

                        _nextMonologues = listenableAdditional[_additionalMonologuesIndex].Data;

                        _additionalMonologuesIndex++;

                        return true;
                    }

                    break;
            }

            _nextMonologues = null;
            return false;
        }
    }
}