using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SolarSystem
{
    public class NavigationController : MonoBehaviour
    {
        [SerializeField]
        private NavigationPanel navigationPanel;

        [SerializeField]
        private Slider scaleSlider;

        [Space]
        [SerializeField]
        private OrbitCamera orbitCamera;

        [SerializeField]
        private TimeScale timeScale;

        [SerializeField]
        private Transform[] planets;

        private void OnEnable()
        {
            scaleSlider.onValueChanged.AddListener(OnSystemScale);
        }

        private void OnDisable()
        {
            scaleSlider.onValueChanged.RemoveAllListeners();
        }

        private IEnumerator Start()
        {
            yield return StartCoroutine(Storage.LoadTextFieldAsync("Data", "Data"));

            Setup();
        }

        private void Setup()
        {
            var data = Storage.PlanetModelData;

            navigationPanel.Setup(data);
            navigationPanel.OnClick += OnNavigationPanel;
        }

        // Events

        private void OnNavigationPanel(PlanetType planetType)
        {
            orbitCamera.Target = planetType switch
            {
                PlanetType.Default => planets[0],
                PlanetType.Mercury => planets[1],
                PlanetType.Venus => planets[2],
                PlanetType.Earth => planets[3],
                PlanetType.Mars => planets[4],
                PlanetType.Jupiter => planets[5],
                PlanetType.Saturn => planets[6],
                PlanetType.Uranus => planets[7],
                PlanetType.Neptune => planets[8],
                PlanetType.Pluto => planets[9],
                _ => orbitCamera.Target
            };
        }

        private void OnSystemScale(float value)
        {
            timeScale.TimeScaleValue = value;
        }
    }
}