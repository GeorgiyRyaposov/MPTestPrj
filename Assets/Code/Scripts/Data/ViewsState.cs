using System;

namespace Code.Scripts.Data
{
    public class ViewsState
    {
        public event Action StateChanged = delegate {};
        private bool _isMainMenuVisible;

        public bool IsMainMenuVisible
        {
            get => _isMainMenuVisible;
            set
            {
                var changed = value != _isMainMenuVisible;
                _isMainMenuVisible = value;
                
                if (changed)
                {
                    StateChanged.Invoke();
                }
            }
        }
    }
}