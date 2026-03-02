using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Game.Contracts;
using Game.SaveSystem;

namespace Game
{
    public class Tower
    {
        private readonly List<Cube> _cubes = new();

        private readonly CameraBounds _cameraBounds;
        private readonly ISaveStorage _save;

        public Tower(CameraBounds cameraBounds, ISaveStorage save)
        {
            _cameraBounds = cameraBounds;
            _save = save;
        }

        public event Action OnAddedCube;
        public event Action OnReachedLimit;

        public bool TrySetCube(Cube cube)
        {
            if (_cubes.Count == 0)
            {
                _cubes.Add(cube);
                cube.Set(cube.transform.position);
                OnAddedCube?.Invoke();
                Save();
                return true;
            }

            Cube lastCube = _cubes[^1];
            
            float firstX = lastCube.transform.position.x;
            float cubeX = cube.transform.position.x;
            float range = lastCube.Bounds.size.x / 2;

            bool inRange = cubeX >= firstX - range && cubeX <= firstX + range;

            if (!inRange) return false;

            if (cube.transform.position.y > lastCube.transform.position.y)
            {
                Vector3 newPosition = new Vector3(lastCube.transform.position.x, lastCube.transform.position.y + lastCube.Bounds.size.y, lastCube.transform.position.z);
                newPosition.x = lastCube.transform.position.x + Random.Range(-(cube.Bounds.size.x / 2), cube.Bounds.size.x / 2);
                cube.Set(newPosition);
                _cubes.Add(cube);
                OnAddedCube?.Invoke();

                if (_cameraBounds.CheckScreenBoard(cube.Bounds))
                    OnReachedLimit?.Invoke();

                Save();
                return true;
            }

            return false;
        }

        public void AddCube(Cube cube) =>
            _cubes.Add(cube);

        public void RemoveCube(Cube cube)
        {
            if (!_cubes.Contains(cube)) return;

            if (cube == _cubes[^1])
            {
                _cubes.Remove(cube);
                Save();
                return;
            }

            int index = 0;
            if (cube != _cubes[0])
                index = _cubes.IndexOf(cube);

            _cubes.Remove(cube);

            for (int i = index; i < _cubes.Count; i++)
            {
                float y = _cubes[i].transform.position.y - _cubes[i].Bounds.size.y;
                _cubes[i].Move(new Vector3(_cubes[i].transform.position.x, y, _cubes[i].transform.position.z));
            }

            Save();
        }

        private void Save()
        {
            _save.Data.Cubes = new();
            foreach (Cube cube in _cubes)
            {
                _save.Data.Cubes.Add(new SaveSystem.CubeData()
                {
                    CubeID = cube.ID,
                    Position = cube.OriginalPosition,
                });
            }

            _save.Save();
        }
    }
}