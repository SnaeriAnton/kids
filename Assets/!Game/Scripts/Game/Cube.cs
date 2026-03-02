using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Cube : MonoBehaviour
    {
        private const string DELETE_KEY = "Delete";

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Vector2 _scaleBeforeDestroy = new(1.6f, 1.6f);
        [SerializeField] private float _scaleBeforeDelay = 0.1f;
        [SerializeField] private float _scaleDisappearDelay = 0.2f;
        [SerializeField] private float _destroyDelay = 1f;

        private GameConfigData _data;
        
        public Bounds Bounds => _spriteRenderer.bounds;
        public Vector3 OriginalPosition { get; private set; }
        public string ID => _data.ID;

        public void Construct(GameConfigData data, Vector3 position)
        {
            _data = data;
            _spriteRenderer.sprite = _data.Sprite;
            OriginalPosition = position;
        }

        public void SetPosition(Vector3 position) => transform.position = position;

        public void Destroy()
        {
            _collider.enabled = false;
            _animator.SetTrigger(DELETE_KEY);
            Destroy(gameObject, _destroyDelay);
        }

        public void Remove()
        {
            _collider.enabled = false;
            transform.DOKill();
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(_scaleBeforeDestroy, _scaleBeforeDelay).SetEase(Ease.OutQuad));
            seq.Append(transform.DOScale(Vector3.zero, _scaleDisappearDelay).SetEase(Ease.OutQuad));
            seq.OnComplete(() => transform.localScale = Vector3.zero);
            Destroy(gameObject, _destroyDelay);
        }

        public void Move(Vector3 targetPosition, float duration = 0.5f)
        {
            OriginalPosition = targetPosition;
            transform.DOKill();
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMove(targetPosition, duration).SetEase(Ease.OutQuad));
            seq.OnComplete(() => transform.position = targetPosition);
        }
        
        public void Set(Vector3 targetPosition, float jumpPower = 1.2f, float duration = 0.35f)
        {
            OriginalPosition = targetPosition;
            transform.DOKill();

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOJump(targetPosition, jumpPower, 1, duration).SetEase(Ease.OutQuad));
            seq.OnComplete(() => transform.position = targetPosition);
        }
    }
}