using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TirtaProccessInspector.ExternalClass;

namespace TirtaProccessInspector
{
    public partial class TirtaProccessInspector : Form
    {
        private NodeClickEvent NodeClickAction = NodeClickEvent.CopyToClipBoard;
        enum NodeClickEvent {CopyToClipBoard, UseAsProccess}
        public TirtaProccessInspector()
        {
            InitializeComponent();
        }

        private void TirtaProccessInspector_Load(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                this.Text = "TirtaProccessInspector ( Running As Administrator )";
                this.Update();
            }
            else
            {
                this.Text = "TirtaProccessInspector ( NOT Running As Administrator )";
                this.Update();
            }
        }

        public static bool IsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #region Thread Invokes
        //Invoke Manager
        private delegate object DynamicInvoke(Form a, TreeView b, TreeNode c, bool f);
        //Invoke Methode
        public static object InvokeTreeNodes(Form a, TreeView b, TreeNode c, bool f)
        {
            if (b.InvokeRequired)
            {
                DynamicInvoke e = new DynamicInvoke(InvokeTreeNodes);
                a.Invoke(e, new object[] { a, b, c, f });
                return null;
            }
            else if (f == true)
            {
                b.Nodes.Add(c);
                //b.GetType().GetProperty("Nodes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).SetValue(b, c, new object[] { });
                return (bool)true;
            }
            else if (f == false)
            {
                System.Reflection.PropertyInfo ab = b.GetType().GetProperty("Nodes");
                dynamic asp = ab.GetValue(b, null);
                return (int)asp;
            }
            return (bool)true;
        }
        #endregion

        private void ProccessGrabber_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadSafe.Invoke(this, GetAllProc, false, ThreadSafe.InvokeProperty.Enabled, ThreadSafe.InvokeMethod.SET);
            ThreadSafe.Invoke(this, StartMonitoring, false, ThreadSafe.InvokeProperty.Enabled, ThreadSafe.InvokeMethod.SET);
            if (e.Argument.ToString() == "list-all")
            {
                ThreadSafe.Invoke(this, label1, "Working on it....", ThreadSafe.InvokeProperty.Text, ThreadSafe.InvokeMethod.SET);
                try
                {
                    Dictionary<int, TreeNode> R = GetProcCMDLine.GetAllProccess();
                    R = R.OrderBy(o => o.Value.Name).ToDictionary(o => o.Key, oa => oa.Value);

                    foreach (KeyValuePair<int, TreeNode> Pairs in R)
                    {
                        InvokeTreeNodes(this, TreeDisplay1, Pairs.Value, true);
                        Thread.Sleep(15);
                    }
                    e.Result = "SUCCESS !";
                }
                catch (Exception a)
                {
                    if (a.Message == "ENOENT") { e.Result = "ENOENT - Proccess Not Found"; }
                }
            }
            else if (e.Argument.ToString().StartsWith("catch-start"))
            {
                if (e.Argument.ToString().Contains("--retry")) {
                    ThreadSafe.Invoke(this, label1, "Still nothing, sleeping for 0.3 seconds !", ThreadSafe.InvokeProperty.Text, ThreadSafe.InvokeMethod.SET);
                }
                //Thread.Sleep(300);
                Thread.Sleep(2000); // agar ada waktu untuk stabil dulu
                ThreadSafe.Invoke(this, label1, "Checking Proccess", ThreadSafe.InvokeProperty.Text, ThreadSafe.InvokeMethod.SET);
                try
                {
                    Dictionary<int, TreeNode> R = GetProcCMDLine.TryGetInformation(ProcNameInput.Text);
                    if (R == null)
                    {
                        e.Result = "ENOENT - Proccess Not Found";
                    }
                    else
                    {
                        foreach (KeyValuePair<int, TreeNode> Pairs in R)
                        {
                            InvokeTreeNodes(this, TreeDisplay1, Pairs.Value, true);
                            Thread.Sleep(15);
                        }
                        ThreadSafe.Invoke(this, label1, "Catched, xD", ThreadSafe.InvokeProperty.Text, ThreadSafe.InvokeMethod.SET);
                        e.Result = "SUCCESS !";
                    }
                }
                catch (Exception a)
                {
                    if (a.Message == "ENOENT") { e.Result = "ENOENT - Proccess Not Found"; }
                }
            }
            else
            {
                e.Result = "Unexpected Task.";
            }
        }

        private void GetAllProc_Click(object sender, EventArgs e)
        {
            TreeDisplay1.Nodes.Clear();
            NodeClickAction = NodeClickEvent.UseAsProccess;
            ProccessGrabber.RunWorkerAsync("list-all");
        }

        private void StartMonitoring_Click(object sender, EventArgs e)
        {
            if (ProcNameInput.Text.Trim() == string.Empty) { label1.Text = "Proccess Field are Empty ???"; return; }
            TreeDisplay1.Nodes.Clear();
            NodeClickAction = NodeClickEvent.CopyToClipBoard;
            ProccessGrabber.RunWorkerAsync("catch-start");
            label1.Text = "Waiting for apps.....";
            MessageBox.Show("Run you apps now and I'll try to grab the information !");
        }

        private void ProccessGrabber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ThreadSafe.Invoke(this, GetAllProc, true, ThreadSafe.InvokeProperty.Enabled, ThreadSafe.InvokeMethod.SET);
            ThreadSafe.Invoke(this, StartMonitoring, true, ThreadSafe.InvokeProperty.Enabled, ThreadSafe.InvokeMethod.SET);

            if (e.Result.ToString().StartsWith("ENOENT - Proccess Not Found"))
            {
                GC.Collect();
                ProccessGrabber.RunWorkerAsync("catch-start --retry");
            }
            else if (e.Result.ToString() == "SUCCESS !")
            {
                ThreadSafe.Invoke(this, label1, String.Empty, ThreadSafe.InvokeProperty.Text, ThreadSafe.InvokeMethod.SET);
                TaskBarFlash.Flash(this);
                GC.Collect();
                return;
            }
        }

        private void TreeDisplay1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (NodeClickAction == NodeClickEvent.CopyToClipBoard)
            {
                Clipboard.SetText(e.Node.Text);
            }
            else if (NodeClickAction == NodeClickEvent.UseAsProccess)
            {
                ProcNameInput.Text = e.Node.Tag.ToString();
            }
        }
    }
}
