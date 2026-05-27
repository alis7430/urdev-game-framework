using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace URDev.GameFramework
{
    public class InputManager : IGameSystem, IUpdatableSystem
    {
        public Action KeyAction = null;
        public Action<Define.MouseEvent> MouseAction = null;

        private bool _mousePressed = false;

        public void Init()
        {
        }

        public void Tick(float deltaTime)
        {
            OnUpdate();
        }

        public void OnUpdate()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            OnUpdateKeyAction();
            OnUpdateMouseAction();
        }

        public void Clear()
        {
            KeyAction = null;
            MouseAction = null;
        }

        private void OnUpdateKeyAction()
        {
            if (Input.anyKey)
                KeyAction?.Invoke();
        }

        private void OnUpdateMouseAction()
        {
            if (MouseAction == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _mousePressed = true;
            }
            else
            {
                if (_mousePressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _mousePressed = false;
            }
        }
    }
}
