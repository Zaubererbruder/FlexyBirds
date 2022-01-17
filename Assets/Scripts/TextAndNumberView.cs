using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Text))]
    public class TextAndNumberView : MonoBehaviour
    {
        [SerializeField] private string _text;
        [SerializeField] private Component _numberSource;
        [SerializeField] private string _numberSourceProperty;

        private PropertyInfo _cachedNumberSourceProperty;
        private Text _textComponent;

        public Component NumberSource { get => _numberSource; set => _numberSource = value; }
        public string NumberSourceProperty { get => _numberSourceProperty; set => _numberSourceProperty = value; }

        private void Awake()
        {
            _textComponent = GetComponent<Text>();
        }

        private void Update()
        {
            //events? INotify?
            UpdateBind();
        }

        private void UpdateBind()
        {
            if (_numberSourceProperty == null)
                return;

            if (_cachedNumberSourceProperty == null || _cachedNumberSourceProperty.Name != _numberSourceProperty)
                Cache();

            _textComponent.text = $"{_text}: {_cachedNumberSourceProperty.GetValue(_numberSource)}";
        }

        private void Cache()
        {
            _cachedNumberSourceProperty = _numberSource.GetType().GetProperty(_numberSourceProperty);
        }
    }
}