using System;
using Entities;
using Managers;
using Models;
using Statics;
using UnityEngine;

namespace Components
{
    public class CannonHandler : MonoBehaviour
    {
        private CannonModel _cannonModel;
        private Cannon _cloneCannon;
        private void Start()
        {
            var level = Prefs.CannonLevel;
            _cannonModel = ContentManager.Instance.GetCannonModel(level + 1);
            _cloneCannon = Instantiate(_cannonModel.Cannon, transform);
        }

        public CannonModel PlayerGotCannon()
        {
            Destroy(_cloneCannon.gameObject);
            return _cannonModel;
        }
    }
}