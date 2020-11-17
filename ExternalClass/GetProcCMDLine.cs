using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace TirtaProccessInspector.ExternalClass
{
    static class GetProcCMDLine
    {
        public static Dictionary<int, System.Windows.Forms.TreeNode> GetAllProccess()
        {
            Dictionary<int, System.Windows.Forms.TreeNode> ReturnValue = new Dictionary<int, System.Windows.Forms.TreeNode>();
            try
            {
                int a = 0;
                foreach (Process process in Process.GetProcesses())
                {
                    System.Windows.Forms.TreeNode N = new System.Windows.Forms.TreeNode();
                    N.Name = process.ProcessName + " - (" + process.Id + ")";
                    N.Text = process.ProcessName + " (" + process.Id + ")";
                    N.Tag = process.ProcessName;
                    ReturnValue.Add(a, N);
                    a++;
                    System.Threading.Thread.Sleep(10);
                }
                
                return ReturnValue;
            }
            catch (Exception e)
            {
                System.Windows.Forms.TreeNode N = new System.Windows.Forms.TreeNode();
                N.Name = " ERROR ";
                N.Text = "Auch, Fail to get running proccess :(";
                N.Nodes.Add("Exception Message : ").Nodes.Add(e.Message);
                ReturnValue.Add(0, N);
                return ReturnValue;
            }
        }

        /// <summary>
        /// Try to get the Proccess Information via WMI by the proccess name
        /// </summary>
        /// <param name="name">The proccess name</param>
        /// <returns>A Dictionary of TreeNodes.</returns>
        public static Dictionary<int, System.Windows.Forms.TreeNode> TryGetInformation(string name)
        {
            Process[] PL = Process.GetProcessesByName(name);
            if (PL.Length == 0) { return null; }
            return TryGetInformation(PL);
        }

        private static Dictionary<int, System.Windows.Forms.TreeNode> TryGetInformation(Process[] PL)
        {
            Dictionary<int, System.Windows.Forms.TreeNode> ReturnValue = new Dictionary<int, System.Windows.Forms.TreeNode>();
            if (PL.Length == 0)
            {
                return null;
            }
            int a = 0;
            foreach (Process i in PL)
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE ProcessId = " + i.Id))
                {
                    using (ManagementObjectCollection objects = searcher.Get())
                    {
                        foreach (ManagementObject obj in objects)
                        {
                            PropertyDataCollection Property = obj.Properties;
                            System.Windows.Forms.TreeNode N = new System.Windows.Forms.TreeNode();
                            N.Name = " (" + i.Id + ")";
                            N.Text = i.ProcessName + " (" + i.Id + ")";
                            foreach (PropertyData item in Property)
                            {
                                System.Windows.Forms.TreeNode N2 = new System.Windows.Forms.TreeNode();
                                N2.Name = "DeepName";
                                N2.Text = item.Name;
                                N2.Nodes.Add("Value : " + item.Value);
                                N2.Nodes.Add("Value Type : " + item.Type);
                                N.Nodes.Add(N2);
                                System.Threading.Thread.Sleep(30);
                            }
                            ReturnValue.Add(a, N);
                            System.Threading.Thread.Sleep(30);
                        }
                    }
                }
                a++;
            }
            return ReturnValue;
        }

        private static Dictionary<int, System.Windows.Forms.TreeNode> TryGetInformation(Process i)
        {
            Dictionary<int, System.Windows.Forms.TreeNode> ReturnValue = new Dictionary<int, System.Windows.Forms.TreeNode>();
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE ProcessId = " + i.Id))
            {
                using (ManagementObjectCollection objects = searcher.Get())
                {
                    foreach (ManagementObject obj in objects)
                    {
                        PropertyDataCollection Property = obj.Properties;
                        System.Windows.Forms.TreeNode N = new System.Windows.Forms.TreeNode();
                        N.Name = " (" + i.Id + ")";
                        N.Text = i.ProcessName + " (" + i.Id + ")";
                        foreach (PropertyData item in Property)
                        {
                            System.Windows.Forms.TreeNode N2 = new System.Windows.Forms.TreeNode();
                            N2.Name = "DeepName";
                            N2.Text = item.Name;
                            N2.Nodes.Add("Value : " + item.Value);
                            N2.Nodes.Add("Value Type : " + item.Type);
                            N.Nodes.Add(N2);
                            System.Threading.Thread.Sleep(30);
                        }
                        ReturnValue.Add(0, N);
                        System.Threading.Thread.Sleep(30);
                    }
                }
            }
            return ReturnValue;
        }
        /// <summary>
        /// Try to get the Proccess Information via WMI by the proccess id
        /// </summary>
        /// <param name="id">The proccess ID (PID)</param>
        /// <returns>A Dictionary of TreeNodes.</returns>
        public static Dictionary<int, System.Windows.Forms.TreeNode> TryGetInformation(int id)
        {
            return TryGetInformation(Process.GetProcessById(id));
        }
    }
}
