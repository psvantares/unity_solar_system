using System;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem
{
    public class NavigationPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject planetView;

        public event Action<PlanetType> OnClick;

        public void Setup(List<PlanetModel> data)
        {
            foreach (var model in data)
            {
                var go = Instantiate(planetView, transform, false);
                go.name = model.Name;
                var view = go.GetComponent<PlanetView>();
                view.Setup((PlanetType)model.Planet, model.Name);
                view.OnClick += OnPlanet;
            }
        }

        // Events
        
        private void OnPlanet(PlanetType planetType)
        {
            OnClick?.Invoke(planetType);
        }
    }
}