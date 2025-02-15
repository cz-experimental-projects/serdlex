﻿using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UIElements
{
    public class SubmitButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField word;
        [SerializeField] private Toggle validate;
        [SerializeField] private Slider slider;

        private float _timer;

        private void Start()
        {
            _timer = 90;
            slider.maxValue = 90;
            slider.minValue = 0;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            slider.value = _timer;

            if (_timer <= 0)
            {
                Submit();
            }
        }

        public void Submit()
        {
            var game = new WordleGame(
                word.text,
                (int) GlobalData.GetOrDefault("roomChances", () => 6f),
                GlobalData.GetOrDefault("gameValidateWord", () => false)
            );
            GlobalData.Set("submittedGame", game);
            SceneTransitioner.Instance.TransitionToScene(10);
        }
    }
}