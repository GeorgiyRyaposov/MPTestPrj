using Unity.Netcode.Components;

namespace Code.Scripts.Components
{
    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}