using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace UtilsToolbox.Utils.UI.UiPanel
{
    public class UiPanelsManager : MonoBehaviour 
    {
        public static UiPanelsManager Instance { get; private set; }
        
        [SerializeField, CanBeNull] protected UiPanelBase _activePanel;
        [SerializeField, CanBeNull] protected UiPanelBase _homePanel;
        [SerializeField] protected List<UiPanelBase> _panels;

        private bool _isSwitchingPanels;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            UiPanelBase.OnPanelOpened += OnPanelOpened;
            UiPanelBase.OnPanelClosed += OnPanelClosed;
        }

        private void OnDisable()
        {
            UiPanelBase.OnPanelOpened -= OnPanelOpened;
            UiPanelBase.OnPanelClosed -= OnPanelClosed;
        }

        protected virtual void Start()
        {
            if (_homePanel == null)
            {
                Debug.LogError("Home panel cannot be null!");
            }
            else
            {
                _activePanel = _homePanel;
                SwitchToPanel(_activePanel);
            }
        }

        private void OnPanelOpened(UiPanelBase panel)
        {
            _activePanel = panel;
        }
        
        private void OnPanelClosed(UiPanelBase panel)
        {
            if (panel == _activePanel)
            {
                _activePanel = null;
            }
        }

        public void SwitchToPanel(UiPanelBase panel, UiPanelOpeningParameters openingParameters = null)
        {
            if (panel == null)
            {
                Debug.LogError("Panel to open is null!");
                return;
            }

            if (_isSwitchingPanels)
            {
                return;
            }
            
            StartCoroutine(SwitchToPanel_Coroutine());
            IEnumerator SwitchToPanel_Coroutine()
            {
                _isSwitchingPanels = true;

                if (_activePanel != null)
                {
                    UiPanelBase closingPanelBase = _activePanel;
                    closingPanelBase.Close();
                    yield return new WaitUntil(() => closingPanelBase.IsOpened == false);
                }

                panel.Open(openingParameters);
                _activePanel = panel;
                _isSwitchingPanels = false;
            }
        }

        public void SwitchToPanel<TPanelType>(TPanelType panelType, UiPanelOpeningParameters openingParameters = null)
            where TPanelType : System.Enum
        {
            UiPanel<TPanelType> nextPanel = _panels
                .OfType<UiPanel<TPanelType>>()
                .FirstOrDefault(p => p.PanelType.Equals(panelType));

            SwitchToPanel(nextPanel, openingParameters);
        }

        public void SwitchToPanel(int panelIndex, UiPanelOpeningParameters openingParameters = null)
        {
            if (_panels.Count >= panelIndex)
            {
                Debug.LogError($"Panel stack does not contain index of {panelIndex}!");
                return;
            }
            
            SwitchToPanel(_panels[panelIndex], openingParameters);
        }
    }
}