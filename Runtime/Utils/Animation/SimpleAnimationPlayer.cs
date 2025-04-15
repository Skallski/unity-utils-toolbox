using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UtilsToolbox.Utils.Animation
{
    [RequireComponent(typeof(Animator))]
    public class SimpleAnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _animationClip;

        private PlayableGraph _playableGraph;
        private AnimationClipPlayable _animationClipPlayable;

        private bool _isReversing;
        private bool _isPlaying;
        private float _playSpeed = 1f;

#if UNITY_EDITOR
        private void Reset()
        {
            if (_animator)
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
            AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(_playableGraph, "AnimationOutput", _animator);
            
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
            
            double currentTime = _animationClipPlayable.GetTime();
            double newTime = currentTime + _playSpeed * Time.deltaTime;

            newTime = Mathf.Clamp((float)newTime, 0f, _animationClip.length);
            _animationClipPlayable.SetTime(newTime);

            if ((_isReversing && newTime <= 0f) || (_isReversing == false && newTime >= _animationClip.length))
            {
                Stop();
            }
        }
        
        private void OnDestroy()
        {
            if (_playableGraph.IsValid())
            {
                _playableGraph.Destroy();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"> animation speed (values below 0 plays in reverse) </param>
        /// <param name="startNormalizedTime"></param>
        public void Play(float speed = 1f, float? startNormalizedTime = null)
        {
            if (_animationClip == null || _animationClip.length <= 0f)
            {
                Stop();
                return;
            }

            if (speed == 0f)
            {
                Stop();
                return;
            }

            _playSpeed = speed;
            _isReversing = speed < 0f;
            _isPlaying = true;

            if (startNormalizedTime.HasValue)
            {
                float time = Mathf.Clamp01(startNormalizedTime.Value) * _animationClip.length;
                _animationClipPlayable.SetTime(time);
            }
            else
            {
                if (_isReversing && _animationClipPlayable.GetTime() <= 0f)
                {
                    _animationClipPlayable.SetTime(_animationClip.length);
                }

                if (_isReversing == false && _animationClipPlayable.GetTime() >= _animationClip.length)
                {
                    _animationClipPlayable.SetTime(0f);
                }
            }
        }


        public void Stop()
        {
            _isPlaying = false;
        }

        public float GetAnimationProgress()
        {
            if (_animationClip == null || _animationClip.length <= 0f)
            {
                return 0;
            }

            float time = (float)_animationClipPlayable.GetTime();
            return Mathf.Clamp01(time / _animationClip.length);
        }
    }
}