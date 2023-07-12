using System;
using UnityEngine;
using UnityEngine.UI;

namespace SolarSystem
{
    public class PlanetView : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Text text;

        private PlanetType planetType;

        public event Action<PlanetType> OnClick;

        private void OnEnable()
        {
            button.onClick.AddListener(OnPlanet);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        public void Setup(PlanetType type, string namePlanet)
        {
            planetType = type;
            text.text = namePlanet;
        }

        // Events

        private void OnPlanet()
        {
            OnClick?.Invoke(planetType);
        }
    }
}