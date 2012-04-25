//---------------------------------------------------------------------------
//  This file is part of the TwitShot software
//  Copyright (c) 2009, Ernesto Herrera Salinas <ernestohs@gmail.com>
//  All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are 
// permitted provided that the following conditions are met:
//
// - Redistributions of source code must retain the above copyright notice, this list 
//   of conditions and the following disclaimer.
// - Redistributions in binary form must reproduce the above copyright notice, this list 
//   of conditions and the following disclaimer in the documentation and/or other 
//   materials provided with the distribution.
// - Neither the name of TwitShot nor the names of its contributors may be 
//   used to endorse or promote products derived from this software without specific 
//   prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
// IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using TwitShot.API;

namespace TwitShot.GUI
{
    public class frmMain : Form
    {
        private TwitPic TP;

        #region Objetos System.Windows.Forms
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.NotifyIcon gui;
        private System.Windows.Forms.ContextMenuStrip cmsOpciones;
        private System.Windows.Forms.ToolStripMenuItem capturarPantallaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capturarÁreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dibujarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem recordarmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem; 
        #endregion

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region WinAPI32

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        #endregion

        #region Hooks

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc;
        private static IntPtr _hookID = IntPtr.Zero;

        private static bool IsPressed(Keys check)
        {
            return ((Control.ModifierKeys & check) == check);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if ((Keys)vkCode == Keys.PrintScreen)
                {
                    if (IsPressed(Keys.Control))
                    {
                        OnCTRL_PrintScreen();
                    }
                    else if (IsPressed(Keys.Shift))
                    {
                        OnShift_PrintScreen();
                    }
                    else
                    {
                        OnPrintScreen();
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void OnPrintScreen()
        {
            frmMsg fm = new frmMsg(this.TP);
            if (fm.ShowDialog() == DialogResult.Yes)
            {
                gui.BalloonTipText = this.TP.MediaUrl.ToString();
                gui.ShowBalloonTip(300);
            }
            fm.Dispose();
            fm = null;
        }

        private void OnShift_PrintScreen()
        {
            frmCrop fc = new frmCrop();
            fc.ShowDialog();
            frmMsg fm = new frmMsg(this.TP, fc.Captura);
            fc.Dispose();
            fc = null;
            if (fm.ShowDialog() == DialogResult.Yes)
            {
                gui.BalloonTipText = this.TP.MediaUrl.ToString();
                gui.ShowBalloonTip(300);
            }
            fm.Dispose();
            fm = null;
        }

        private void OnCTRL_PrintScreen()
        {
            TwitShot.GUI.Paint.frmImgEditor fie = new TwitShot.GUI.Paint.frmImgEditor(this.TP);
            fie.ShowDialog();
            fie.Dispose();
            fie = null;
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private void ActivarHooks()
        {
            _proc = new LowLevelKeyboardProc(HookCallback);
            _hookID = SetHook(_proc);
        }

        #endregion

        public frmMain()
        {
            InitializeComponent();
            
            TP = new TwitPic(AppConfig.Username, AppConfig.Password);
            ActivarHooks();
        }

        public frmMain(string username, SecureString password)
        {
            InitializeComponent();
            TP = new TwitPic(username, password);
            ActivarHooks();
        }

        public frmMain(TwitPic tp)
        {
            InitializeComponent();
            this.TP = tp;
            ActivarHooks();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gui = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.capturarPantallaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capturarÁreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dibujarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.recordarmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOpciones.SuspendLayout();
            this.SuspendLayout();
            this.gui.ContextMenuStrip = this.cmsOpciones;
            this.gui.Icon = global::TwitShot.Properties.Resources.twitshot;
            this.gui.Text = "TwitShot";
            this.gui.Visible = true;
            this.cmsOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.capturarPantallaToolStripMenuItem,
            this.capturarÁreaToolStripMenuItem,
            this.dibujarToolStripMenuItem,
            this.toolStripSeparator2,
            this.recordarmeToolStripMenuItem,
            this.configuraciónToolStripMenuItem,
            this.toolStripSeparator1,
            this.salirToolStripMenuItem});
            this.cmsOpciones.Name = "cmsOpciones";
            this.cmsOpciones.Size = new System.Drawing.Size(166, 148);
            this.capturarPantallaToolStripMenuItem.Name = "capturarPantallaToolStripMenuItem";
            this.capturarPantallaToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.capturarPantallaToolStripMenuItem.Text = "Capturar &Pantalla";
            this.capturarPantallaToolStripMenuItem.Click += new System.EventHandler(this.capturarPantallaToolStripMenuItem_Click);
            this.capturarÁreaToolStripMenuItem.Name = "capturarÁreaToolStripMenuItem";
            this.capturarÁreaToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.capturarÁreaToolStripMenuItem.Text = "C&apturar Área";
            this.capturarÁreaToolStripMenuItem.Click += new System.EventHandler(this.capturarAreaToolStripMenuItem_Click);
            this.dibujarToolStripMenuItem.Name = "dibujarToolStripMenuItem";
            this.dibujarToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.dibujarToolStripMenuItem.Text = "&Dibujar";
            this.dibujarToolStripMenuItem.Click += new System.EventHandler(this.dibujarToolStripMenuItem_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(162, 6);
            this.recordarmeToolStripMenuItem.Checked = true;
            this.recordarmeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recordarmeToolStripMenuItem.Name = "recordarmeToolStripMenuItem";
            this.recordarmeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.recordarmeToolStripMenuItem.Text = "&Recordarme";
            this.recordarmeToolStripMenuItem.Click += new System.EventHandler(this.recordarmeToolStripMenuItem_Click);
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.configuraciónToolStripMenuItem.Text = "&Configuración";
            this.configuraciónToolStripMenuItem.Click += new System.EventHandler(this.configuracionToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(0, 0);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::TwitShot.Properties.Resources.twitshot;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TwitShot";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.cmsOpciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.SuspendLayout();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cerrar TwitShot?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UnhookWindowsHookEx(_hookID);
                Application.Exit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Configuración
            frmProps fp = new frmProps();
            fp.ShowDialog();
            fp.Dispose();
            fp = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordarmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Recordar
            if (recordarmeToolStripMenuItem.Checked)
            {
                if (AppConfig.SavedCredentials)
                {
                    AppConfig.SavedCredentials = false;
                }
                recordarmeToolStripMenuItem.Checked = false;
            }
            else
            {
                AppConfig.Username = TP.Username;
                AppConfig.Password = TP.Password.ToSecureString();
                AppConfig.SavedCredentials = true;
                recordarmeToolStripMenuItem.Checked = true;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dibujarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Dibujar
            OnCTRL_PrintScreen();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void capturarAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Capturar Area
            OnShift_PrintScreen();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void capturarPantallaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Capturar Pantalla
            OnPrintScreen();
        }
    }
}
