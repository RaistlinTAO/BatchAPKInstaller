// ######################################################################################################################
// #  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the  #
// #  following conditions are met:                                                                                     #
// #    1、Redistributions of source code must retain the above copyright notice, this list of conditions and the       #
// #       following disclaimer.                                                                                        #
// #    2、Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the    #
// #       following disclaimer in the documentation and/or other materials provided with the distribution.             #
// #    3、Neither the name of the D.E.M.O.N, K9998(Wei Tao) nor the names of its contributors may be used to endorse   #
// #       or promote products derived from this software without specific prior written permission.                    #
// #                                                                                                                    #
// #       Project Name:                                                                                                #
// #       Module  Name:                                                                                                #
// #       Part of:                                                                                                     #
// #       Date:                                                                                                        #
// #       Version:                                                                                                     #
// #                                                                                                                    #
// #                                           Copyright © 2011 ORG: D.E.M.O.N K9998(Wei Tao) All Rights Reserved      #
// ######################################################################################################################
namespace BatchAPKInstaller.UI
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using BatchAPKInstaller.Class;
    using BatchAPKInstaller.Properties;

    #endregion

    public partial class frmMian : Form
    {
        private readonly clsINI iniController = new clsINI(Application.StartupPath + "\\BatchAPKInstaller.bin");
        public string iLastStr;
        public string iPhoneSN;
        public bool isConnect;

        public frmMian()
        {
            InitializeComponent();
            //释放adb

            try
            {
                var res = new ResourceManager("BatchAPKInstaller.Properties.Resources", Assembly.GetExecutingAssembly());
                var br = res.GetObject("adb") as byte[];
                var fs = new FileStream(Application.StartupPath + @"\adb.exe", FileMode.Create, FileAccess.Write);
                var fs1 = new FileStream(Application.StartupPath + @"\AdbWinApi.dll", FileMode.Create, FileAccess.Write);
                fs.Write(br, 0, br.Length);
                br = res.GetObject("AdbWinApi") as byte[];
                fs1.Write(br, 0, br.Length);
                fs.Close();
                fs1.Close();
            }
            catch (Exception)
            {
                KillProcess("adb");
            }
        }

        #region UI部分代码

        private void frmMian_Load(object sender, EventArgs e)
        {
            cmdStart.Enabled = false;
        }

        private void frmMian_Shown(object sender, EventArgs e)
        {
            tmCheck.Enabled = true;
            if (iniController.IniReadValue("SYSTEM", "PATH") != "" &&
                iniController.IniReadValue("SYSTEM", "PATH") != null)
            {
                ListSoftware(iniController.IniReadValue("SYSTEM", "PATH"));
                lblPath.Text = iniController.IniReadValue("SYSTEM", "PATH");
                cmdStart.Enabled = true;
            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            isConnect = false;
            tmCheck.Enabled = true;
            lstResult.Items.Clear();
            lsvSoftware.Items.Clear();
            try
            {
                ListSoftware(iniController.IniReadValue("SYSTEM", "PATH"));
                pbMain.Value = 0;
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private void cmdOption_Click(object sender, EventArgs e)
        {
            fbdMain.Reset();
            fbdMain.Description = Resources.frmMian_cmdOption_Click_Select_APK_Files_Path;
            fbdMain.ShowNewFolderButton = true;

            if (fbdMain.ShowDialog() != DialogResult.OK) return;
            lsvSoftware.Items.Clear();

            lblPath.Text = fbdMain.SelectedPath;

            if (lblPath.Text.Substring(lblPath.Text.Length - 1) == "\\")
            {
                lblPath.Text = lblPath.Text.Substring(0, lblPath.Text.Length - 1);
            }

            ListSoftware(fbdMain.SelectedPath);
            cmdStart.Enabled = true;
            iniController.IniWriteValue("SYSTEM", "PATH", fbdMain.SelectedPath);
            //判断是否存在指定文件夹 没有则创建?
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                int j = 0;

                for (int i = 0; i < lsvSoftware.Items.Count; i++)
                {
                    if (lsvSoftware.Items[i].Checked && lsvSoftware.Items[i].BackColor != Color.DodgerBlue)
                    {
                        j = j + 1;
                    }
                }

                var tempList = new string[j];
                var iIndex = new int[j];
                if (j > 0)
                {
                    cmdStart.Enabled = false;

                    //抽取点选的软件
                    int x = 0;
                    for (int i = 0; i < lsvSoftware.Items.Count; i++)
                    {
                        if (!lsvSoftware.Items[i].Checked || lsvSoftware.Items[i].BackColor == Color.DodgerBlue)
                            continue;
                        iIndex[x] = i;
                        tempList[x] = lsvSoftware.Items[i].Text;
                        x++;
                    }
                    //开始批量安装
                    InstallSoftware(iIndex, tempList);
                }
                else
                {
                    MessageBox.Show(Resources.frmMian_cmdStart_Click_No_selected_Software_s__, Application.ProductName,
                                    MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show(Resources.frmMian_cmdStart_Click_No_Device_Connected_, Application.ProductName,
                                MessageBoxButtons.OK);
            }
        }

        #endregion

        #region 安装APK

        private void SetupApk(string APKFile)
        {
            File.Copy(APKFile, lblPath.Text + "\\K9998.apk", true);
        }

        private void InstallSoftware(IList<int> iIndex, IList<string> iList)
        {
            //防止ADB冲突干掉ADB
            tmCheck.Enabled = false;

            pbMain.Maximum = iList.Count;
            pbMain.Value = 0;

            int sum = 0;
            int fsum = 0;

            for (int i = 0; i < iList.Count; i++)
            {
                lstResult.Items.Add(GetTime() + "Try to install " + iList[i]);
                lstResult.SelectedIndex = lstResult.SelectedIndex + 1;


                //APK必须改名 非中文
                DelegateSetupAPK dn = SetupApk;

                IAsyncResult iar = dn.BeginInvoke(iList[i], null, null);

                while (iar.IsCompleted == false)
                {
                    Application.DoEvents();
                }

                DelegateinstallAPK dn1 = installAPK;

                IAsyncResult iar1 = dn1.BeginInvoke(iList[i], null, null);

                while (iar1.IsCompleted == false)
                {
                    Application.DoEvents();
                }

                bool iFlag = dn1.EndInvoke(iar1);

                if (iFlag)
                {
                    lstResult.Items.Add(GetTime() + "Install " + iList[i] + " completed!");
                    lstResult.SelectedIndex = lstResult.SelectedIndex + 1;
                    lsvSoftware.Items[iIndex[i]].BackColor = Color.DodgerBlue;
                    lsvSoftware.Items[iIndex[i]].SubItems.Add("Success");
                    sum = sum + 1;
                }
                else
                {
                    lstResult.Items.Add(GetTime() + "Install " + iList[i] + " FAILED!");
                    lstResult.SelectedIndex = lstResult.SelectedIndex + 1;
                    lsvSoftware.Items[iIndex[i]].BackColor = Color.Red;
                    lsvSoftware.Items[iIndex[i]].SubItems.Add("FAIL");
                    fsum = fsum + 1;
                }
                pbMain.Value = pbMain.Value + 1;
                Application.DoEvents();
            }

            lstResult.Items.Add(GetTime() + "Install Progress completed!  (Success: " + sum + " Fail: " + fsum + ")");
            lstResult.SelectedIndex = lstResult.SelectedIndex + 1;
            MessageBox.Show(Resources.frmMian_InstallSoftware_All_Done, Application.ProductName, MessageBoxButtons.OK);
            cmdStart.Enabled = true;
        }

        private bool installAPK(string APKFile)
        {
            StreamReader outputReader = null;
            StreamReader errorReader = null;
            try
            {
                //Create Process Start information
                var processStartInfo =
                    new ProcessStartInfo(Application.StartupPath + "\\adb.exe",
                                         "install -r \"" + lblPath.Text + "\\K9998.apk\"")
                        {
                            ErrorDialog = false,
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true
                        };
                //Execute the process
                var process = new Process {StartInfo = processStartInfo};

                bool processStarted = process.Start();
                if (processStarted)
                {
                    //Get the output stream
                    outputReader = process.StandardOutput;
                    errorReader = process.StandardError;
                    // process.StandardOutput.ReadToEnd();


                    //Display the result
                    string displayText = ""; //"Output" + Environment.NewLine + "==============" + Environment.NewLine;
                    displayText += outputReader.ReadToEnd();
                    //Application.DoEvents();
                    process.WaitForExit();
                    //displayText += Environment.NewLine + "Error" + Environment.NewLine + "==============" +
                    //               Environment.NewLine;
                    //displayText += errorReader.ReadToEnd();
                    /*
                    F:\>adb install "D:\I9000_tools\外置SD卡补丁\1.apk"
                    1166 KB/s (3659674 bytes in 3.064s)
                    pkg: /data/local/tmp/1.apk
                    Success
                    */
                    string[] tempArr = Regex.Split(displayText, Environment.NewLine, RegexOptions.IgnoreCase);

                    process.Close();
                    process.Dispose();

                    return tempArr[1].Contains("Success");
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);

                return false;
            }
            finally
            {
                if (outputReader != null)
                {
                    outputReader.Close();
                }
                if (errorReader != null)
                {
                    errorReader.Close();
                }
                //btnStart.Enabled = true;
            }

            return false;
        }

        #region Nested type: DelegateSetupAPK

        private delegate void DelegateSetupAPK(string APKFile);

        #endregion

        #region Nested type: DelegateinstallAPK

        private delegate bool DelegateinstallAPK(string APKFile);

        #endregion

        #endregion

        #region 列举软件列表

        private void ListSoftware(string iPath)
        {
            string[] tempList = Directory.GetFiles(iPath, "*.apk");
            foreach (string iName in tempList)
            {
                if (!iName.Contains("\\K9998.apk"))
                    lsvSoftware.Items.Add(iName);
            }
        }

        /// <summary>
        ///   废弃函数 考虑到APK就一个目录
        /// </summary>
        /// <param name = "iPath"></param>
        private void ListSoftware1(string iPath)
        {
            //iID = 0;
            var info = new DirectoryInfo(iPath);
            if (!info.Exists) return;
            DirectoryInfo dir = info;
            //不是目录
            FileSystemInfo[] files;
            try
            {
                files = dir.GetFileSystemInfos();
                for (int i = 0; i < files.Length; i++)
                {
                    var file = files[i] as FileInfo;
                    //是文件
                    if (file != null)
                    {
                        //Console.WriteLine(file.FullName + "\t " + file.Length);
                        try
                        {
                            if (file.FullName.Substring(file.FullName.LastIndexOf(".")) == ".apk")
                                //此处为显示APK格式，不加IF可遍历所有格式的文件
                            {
                                lsvSoftware.Items.Add(file.FullName); //
                                //iID = iID + 1;
                                Application.DoEvents();
                                //MessageBox.Show(file.FullName.Substring(file.FullName.LastIndexOf(".")));
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                        //对于子目录，进行递归调用
                    else
                    {
                        ListSoftware(files[i].FullName);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region 检测手机

        private void Check_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //string[] tempArr = Regex.Split(e.Data, Environment.NewLine, RegexOptions.IgnoreCase);

            try
            {
                //CheckConnectStrDelegate sd = new CheckConnectStrDelegate();
                Invoke(new CheckConnectStrDelegate(CheckConnectStr), e.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CheckConnectStr(string iCon)
        {
            if (iCon.Contains("List of devices attached"))
            {
                iLastStr = iCon;
                return;
            }
            if (iCon.Contains("* daemon"))
            {
                iLastStr = iCon;
                return;
            }
            if (iCon == "" && iLastStr.Contains("List of devices attached"))
            {
                iLastStr = iCon;
                isConnect = false;
                return;
            }

            if (iCon.Contains("device"))
            {
                iLastStr = iCon;
                iPhoneSN = iCon;
                isConnect = true;
            }
        }

        private void tmCheck_Tick(object sender, EventArgs e)
        {
            try
            {
                //Create Process Start information
                var processStartInfo =
                    new ProcessStartInfo(Application.StartupPath + "\\adb.exe", "devices")
                        {
                            ErrorDialog = false,
                            UseShellExecute = false,
                            //RedirectStandardError = true,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true
                        };
                //Execute the process
                var process = new Process {StartInfo = processStartInfo};

                process.OutputDataReceived += Check_OutputDataReceived;
                process.Start();
                process.BeginOutputReadLine();

                //process.WaitForExit();
                process.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (isConnect)
            {
                if (lblPhoneName.Text != iPhoneSN.Replace("device", "") + Resources.frmMian_tmCheck_Tick__Connected_)
                {
                    lblPhoneName.Text = iPhoneSN.Replace("device", "") + Resources.frmMian_tmCheck_Tick__Connected_;
                    lstResult.Items.Add(GetTime() + iPhoneSN.Replace("device", "") +
                                        Resources.frmMian_tmCheck_Tick__Connected_);
                    lstResult.SelectedIndex = lstResult.SelectedIndex + 1;
                }
            }
            else
            {
                lblPhoneName.Text = Resources.frmMian_tmCheck_Tick_No_connect_;
            }
        }

        private delegate void CheckConnectStrDelegate(string iCon);

        #endregion

        #region 私有函数

        private static string GetTime()
        {
            return DateTime.Now.Hour + ":" + DateTime.Now.Minute + "  :  ";
        }

        private static void KillProcess(string processName)
        {
            //System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                    //循环查找
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }
            }
            catch
            {
                //Do Nothing
            }
        }

        #endregion
    }
}