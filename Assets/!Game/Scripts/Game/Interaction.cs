using System;
using UnityEngine;
using Zenject;
using Game.Contracts;
using Game.Raycast;

namespace Game
{
    public class Interaction : IInitializable, IDisposable
    {
        private readonly Tower _tower;
        private readonly Camera _camera;
        private readonly Raycaster _raycaster;
        private readonly Factory _factory;
        private readonly HUD _hud;
        private readonly IInput _input;

        private Cube _cube;
        private Vector3 _newPosition;
        private Vector3 _cubePosition;
        private Vector2 _startPosition;
        private CubeView _cubeView;

        public Interaction(Camera camera, Factory factory, Raycaster raycaster, Tower tower, HUD hud, IInput input)
        {
            _camera = camera;
            _factory = factory;
            _raycaster = raycaster;
            _tower = tower;
            _hud = hud;
            _input = input;
        }
        
        public void Initialize()
        {
            _input.Pressed += Start;
            _input.Move += Move;
            _input.Released += Stop;
        }

        public void Dispose()
        {
            _input.Pressed -= Start;
            _input.Move -= Move;
            _input.Released -= Stop;
        }

        private void Start(Vector2 mousePosition)
        {
            _cubePosition = Vector3.zero;
            _startPosition = mousePosition;

            if (_raycaster.TryGetUIComponent(mousePosition, out CubeView cubeView))
            {
                _cubeView = cubeView;
            }
            else if (_raycaster.TryGetComponent(mousePosition, out Cube cube))
            {
                _cube = cube;
                _cubePosition = _cube.transform.position;
            }
        }

        private void Move(Vector2 mousePosition)
        {
            if (_startPosition.y + _hud.HeightScroll / 2 < mousePosition.y && _cube == null && _cubeView != null)
            {
                _newPosition = _camera.ScreenToWorldPoint(mousePosition);
                _newPosition.z = 0;
                _cube = _factory.Create(_cubeView.Data, _newPosition);
                _hud.StopScroll();
            }

            if (!_cube) return;

            _newPosition = _camera.ScreenToWorldPoint(mousePosition);
            _newPosition.z = 0;
            _cube.SetPosition(_newPosition);
        }

        private void Stop(Vector2 mousePosition)
        {
            if (!_cube) return;

            if (_raycaster.TryGetComponent<Hole>(mousePosition, out _))
            {
                _hud.ShowThrowAwayCubText();
                _tower.RemoveCube(_cube);
                _cube.Destroy();
                _cubeView = null;
                _cube = null;
                return;
            }

            if (_raycaster.TryGetComponent<TowerZone>(mousePosition, out _))
            {
                if (_cubeView)
                {
                    if (_tower.TrySetCube(_cube))
                    {
                        _cubeView = null;
                        _cube = null;
                        return;
                    }

                    _cube.Remove();
                    _cubeView = null;
                    _cube = null;
                    return;
                }
            }

            if (_cubeView)
            {
                _hud.ShowCubeDisappearingText();
                _cube.Remove();
            }
            else
            {
                _cube.SetPosition(_cubePosition);
            }

            _cubeView = null;
            _cube = null;
        }
    }
}