﻿using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649
namespace Arkham.Onigiri.UI
{
    public class FloatVariableToImageFill : MonoBehaviour
    {
        [SerializeField] private Image myImage;
        [SerializeField] private FloatVariable value;
        [SerializeField] private bool initOnStart = true;
        [SerializeField] private bool inverted = false;

        private void OnEnable() => value.onChange.AddListener(UpdateValue);

        private void OnDisable() => value.onChange.RemoveListener(UpdateValue);

        void Start()
        {
            if (myImage == null) myImage = GetComponent<Image>();
            if (initOnStart) UpdateValue();
        }

        public void UpdateValue() => myImage.fillAmount = inverted ? 1 - value.Value : value.Value;
    }
}
