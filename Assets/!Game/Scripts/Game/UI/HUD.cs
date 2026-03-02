using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Zenject;
using Game.Contracts;

namespace Game
{
    public class HUD : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform _content;
        [SerializeField] private CubeView _cubeViewTemplate;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _scrollRectTrasnsform;
        [SerializeField] private float _delay = 2f;

        private WaitForSeconds _wait;
        private Coroutine _coroutine;
        private Tower _tower;
        private ILocalization _localization;
        
        public float HeightScroll => _scrollRectTrasnsform.rect.height;
        
        [Inject]
        public void Construct(Tower tower, ILocalization localization, IGameConfigProvider cubeData)
        {
            _tower = tower;
            _localization = localization;
            _wait = new WaitForSeconds(_delay);
            List<GameConfigData> configs = new(cubeData.GetConfigs());
            
            foreach (GameConfigData info in configs)
                Instantiate(_cubeViewTemplate, _content).Construct(info);

            _tower.OnAddedCube += ShowSetCubeText;
            _tower.OnReachedLimit += ShowReachedLimitText;
        }

        public void Dispose()
        {
            _tower.OnAddedCube -= ShowSetCubeText;
            _tower.OnReachedLimit -= ShowReachedLimitText;
            
            if (this == null) return;
            StopAllCoroutines();
            _coroutine = null;
        }

        public void ShowSetCubeText()=>StartShow("SET_CUBE");
        public void ShowReachedLimitText() => StartShow("REACHED_LIMIT");
        public void ShowThrowAwayCubText()=>StartShow("THROW_AWAY_CUBE");
        public void ShowCubeDisappearingText()=>StartShow("CUBE_DISAPPEAR");

        public void StopScroll()
        {
            _scrollRect.OnEndDrag(new PointerEventData(EventSystem.current));

            _scrollRect.velocity = Vector2.zero;
        }

        private void StartShow(string key)
        {
            StopAllCoroutines();
            _coroutine = null;
            _coroutine ??= StartCoroutine(ShowText(key));
        }
        
        private IEnumerator ShowText(string key)
        {
            _text.text = _localization.GetTranslate(key);
            _text.enabled = true;
            yield return _wait;
            _text.enabled = false;
            _coroutine = null;
        }
    }
}