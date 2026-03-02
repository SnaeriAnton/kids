using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public Sprite Sprite => _image.sprite;
        public GameConfigData Data { get; private set; }
        public string ID => Data.ID;

        public void Construct(GameConfigData data)
        {
            Data = data;
            _image.sprite = data.Sprite;
        }
    }
}