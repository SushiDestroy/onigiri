﻿using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.UI
{
    public class FloatVariableToSlider : MonoBehaviour
    {
        [InfoBox("DEPRECATED - use ChangeableVariableToSlider", InfoMessageType.Warning)]
        [SerializeField] private Slider mySlider;
        [SerializeField] private FloatVariable value;
        [SerializeField] private bool inverted = false;
        public FloatVariable Value
        {
            set { this.value = value; }
        }
        [SerializeField] private bool initOnStart = true;
        [SerializeField] private bool listenToChange = true;

        private void OnEnable()
        {
            if (listenToChange)
                value.onChange.AddListener(UpdateValue);
        }

        private void OnDisable()
        {
            if (listenToChange)
                value.onChange.RemoveListener(UpdateValue);
        }

        void Start()
        {
            if (mySlider == null) mySlider = GetComponent<Slider>();
            if (initOnStart) UpdateValue();
        }

        public void UpdateValue() => mySlider.value = inverted ? mySlider.maxValue - value.Value : value.Value;
    }
}
