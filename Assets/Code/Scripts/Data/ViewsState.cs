using UniRx;

namespace Code.Scripts.Data
{
    public class ViewsState
    {
        public ReactiveProperty<bool> IsMainMenuVisible = new();
        public ReactiveProperty<bool> IsGameOverVisible = new();
        public ReactiveProperty<bool> IsDisconnectedPanelVisible = new();
    }
}