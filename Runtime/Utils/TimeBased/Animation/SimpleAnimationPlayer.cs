using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UtilsToolbox.Utils.TimeBased.Animation
{
    [RequireComponent(typeof(Animator))]
    public class SimpleAnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _animationClip;

        [field: SerializeField] public float AnimationProgress { get; private set; }
        [SerializeField] private float _animationSpeed = 1f;

        public float AnimationSpeed
        {
            get => _animationSpeed;
            set
            {
                if (value == 0f)
                {
                    throw new ArgumentOutOfRangeException(nameof(_animationSpeed), "Cannot be 0!");
                }
                
                _animationSpeed = value;
            }
        }
        
        private PlayableGraph _playableGraph;
        private AnimationClipPlayable _animationClipPlayable;

        private bool _isReversing;
        private bool _isPlaying;

#if UNITY_EDITOR
        private void Reset()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
        }
#endif

        private void Start()
        {
            if (_animationClip == null)
            {
                return;
            }

            _playableGraph = PlayableGraph.Create("SimpleAnimationGraph");
            AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create
            (
                _playableGraph, 
                "AnimationOutput", 
                _animator
            );
            
            _animationClipPlayable = AnimationClipPlayable.Create(_playableGraph, _animationClip);
            _animationClipPlayable.SetSpeed(0);
            _animationClipPlayable.SetTime(0);

            playableOutput.SetSourcePlayable(_animationClipPlayable);
            _playableGraph.Play();
        }

        private void Update()
        {
            if (_isPlaying == false)
            {
                return;
            }
            
            float direction = _isReversing ? -1f : 1f;
            double newTime = _animationClipPlayable.GetTime() + direction * AnimationSpeed * Time.deltaTime;
            newTime = Mathf.Clamp((float)newTime, 0f, _animationClip.length);
            _animationClipPlayable.SetTime(newTime);
            
            AnimationProgress = Mathf.Clamp01((float)(_animationClipPlayable.GetTime() / _animationClip.length));
            
            if ((_isReversing == false && newTime >= _animationClip.length) || (_isReversing && newTime <= 0f))
            {
                _isPlaying = false;
            }
        }
        
        private void OnDestroy()
        {
            if (_playableGraph.IsValid())
            {
                _playableGraph.Destroy();
            }
        }
        
        private void PlayInternal(float speed, float startNormalizedTime, bool isReversing)
        {
            if (_animationClip == null)
            {
                throw new NullReferenceException("Animation clip cannot be null!");
            }
            
            if (speed <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed),
                    "Animation speed has to be positive value!");
            }

            AnimationSpeed = speed;
            AnimationProgress = startNormalizedTime;
            _isReversing = isReversing;
            _isPlaying = true;
            
            float time = Mathf.Clamp01(startNormalizedTime) * _animationClip.length;
            _animationClipPlayable.SetTime(time);
        }
        
        public void Play(float? speed = null, float? startNormalizedTime = null)
        {
            PlayInternal(speed ?? AnimationSpeed, startNormalizedTime ?? AnimationProgress, false);
        }
        
        public void PlayReverse(float? speed = null, float? startNormalizedTime = null)
        {
            PlayInternal(speed ?? AnimationSpeed, startNormalizedTime ?? AnimationProgress, true);
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void Stop()
        {
            Pause();
            RewindAnimationForward();
        }

        public void RewindAnimationForward()
        {
            _animationClipPlayable.SetTime(_animationClip.length);
            AnimationProgress = 1;
        }

        public void RewindAnimationBackwards()
        {
            _animationClipPlayable.SetTime(0f);
            AnimationProgress = 0;
        }
    }
}