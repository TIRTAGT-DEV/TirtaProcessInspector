using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TirtaProccessInspector.ExternalClass
{
    static class ThreadSafe
    {
        private delegate void SetEnabled(Form a, Control b, bool c);
        private delegate void SetTextCallback(Form a, Control b, string c);
        private delegate object DynamicInvoke(Form a, Control b, dynamic c, InvokeProperty d, InvokeMethod f);

        /// <summary>
        /// Set a new text property
        /// </summary>
        /// <param name="a">Form that host the controls</param>
        /// <param name="b">The control resource</param>
        /// <param name="c">The new text</param>
        public static void SetText(Form a, Control b, string c)
        {
            //if (b.Text == null) { return; }
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (b.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                a.Invoke(d, new object[] { a, b, c });
            }
            else
            {
                b.Text = c;
            }
        }

        public static void SetControl(Form a, Control b, bool c)
        {
            if (b.InvokeRequired)
            {
                SetEnabled d = new SetEnabled(SetControl);
                a.Invoke(d, new object[] { a, b, c });
            }
            else
            {
                b.Enabled = false;
            }
        }
        public enum InvokeMethod{GET,SET};
        public enum InvokeProperty{ AutoSize, BackgroundImage, BackgroundImageLayout, CreateParams, DefaultImeMode, DefaultSize, DoubleBuffered, Font, ForeColor, ImeMode, LargeChange, Maximum, Minimum, Orientation, Padding, PreferredDimension, RightToLeftLayout, SmallChange, Text, TickStyle, TickFrequency, Value, DpiAwarenessContext, AccessibilityObject, AccessibleDefaultActionDescription, AccessibleDescription, AccessibleName, AccessibleRole, AllowDrop, Anchor, AutoScrollOffset, LayoutEngine, BackColorBrush, BackColor, BecomingActiveControl, BindingContextInternal, BindingContext, Bottom, Bounds, CanAccessProperties, CanFocus, CanRaiseEvents, CanSelect, Capture, CaptureInternal, CausesValidation, CacheTextInternal, ClientRectangle, ClientSize, CompanyName, ContainsFocus, ContextMenu, ContextMenuStrip, Controls, Created, ValidationCancelled, IsTopMdiWindowClosing, IsCurrentlyBeingScaled, CreateThreadId, Cursor, DataBindings, DefaultCursor, DefaultMargin, DefaultMaximumSize, DefaultMinimumSize, DefaultPadding, DeviceDpi, DisabledColor, DisplayRectangle, IsDisposed, Disposing, Dock, Enabled, Focused, FontHandle, FontHeight, Handle, HandleInternal, HasChildren, HasMenu, Height, HostedInWin32DialogManager, IsHandleCreated, IsLayoutSuspended, IsWindowObscured, InternalHandle, InvokeRequired, IsAccessible, IsActiveX, IsContainerControl, IsIEParent, IsMirrored, IsMnemonicsListenerAxSourced, Left, Location, Margin, MaximumSize, MinimumSize, Name, Parent, ParentInternal, ProductName, ProductVersion, Properties, RawBackColor, RecreatingHandle, Region, RenderRightToLeft, RenderTransparent, RenderTransparencyWithVisualStyles, RequiredScaling, RequiredScalingEnabled, ResizeRedraw, Right, RightToLeft, ScaleChildren, Site, Size, TabIndex, TabStop, TabStopInternal, Tag, Top, TopLevelControl, TopLevelControlInternal, TopMostParent, ShowKeyboardCues, ShowFocusCues, ShowParams, UseWaitCursor, UseCompatibleTextRenderingInt, SupportsUseCompatibleTextRendering, Visible, Width, WindowStyle, WindowTarget, WindowText, PreferredSize, ParentContainerControl, ShouldAutoValidate, SupportsUiaProviders, ToolStripControlHost, CachedImeMode, CanEnableIme, CurrentImeContextMode, DisableImeModeChangedCount, ImeModeBase, ImeWmCharsToIgnore, CanRaiseEventsInternal, Events, Container, DesignMode};
        public static object Invoke(Form a, Control b, dynamic c, InvokeProperty d, InvokeMethod f)
        {
            if (b.InvokeRequired)
            {
                DynamicInvoke e = new DynamicInvoke(Invoke);
                a.Invoke(e, new object[] { a, b, c, d, f });
                return null;
            }
            else if (f == InvokeMethod.SET)
            {   
                b.GetType().GetProperty(d.ToString(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).SetValue(b, c, new object[] { });
                return (bool)true;
            }
            else if (f == InvokeMethod.GET)
            {
                PropertyInfo ab = b.GetType().GetProperty(d.ToString());
                dynamic asp = ab.GetValue(b, null);
                return (int)asp;
            }
            return (bool)true;
        }
    }

}
