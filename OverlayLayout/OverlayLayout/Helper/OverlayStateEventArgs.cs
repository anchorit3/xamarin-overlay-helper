using System;

namespace OverlayLayout.Helper
{
    public class OverlayStateEventArgs : EventArgs
    {
        private bool isOpen;

        public OverlayStateEventArgs(bool state)
        {
            isOpen = state;
        }
        public bool IsOpen
        {
            get { return isOpen; }
        }
    }
}
