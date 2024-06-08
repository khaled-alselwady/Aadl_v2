using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myControlLibrary
{
    public partial class myCustomMessageBoxFrm : Form
    {
        private int _MaxWidth = (int)(SystemInformation.WorkingArea.Width * 0.60);

        private int _MaxHeight = (int)(SystemInformation.WorkingArea.Height * 0.90);
        public myCustomMessageBoxFrm()
        {
            InitializeComponent();
          

            // Set maximum height for the form
            this.MaximumSize = new Size(_MaxWidth,_MaxHeight);

        }

        /// <summary>
        /// Measures a string using the Graphics object for this form with
        /// the specified font
        /// </summary>
        /// <param name="str">The string to measure</param>
        /// <param name="maxWidth">The maximum width
        ///          available to display the string</param>
        /// <param name="font">The font with which to measure the string</param>
        /// <returns></returns>
        private Size MeasureString(string str, int maxWidth, Font font)
        {
            Graphics g = this.CreateGraphics();
            SizeF strRectSizeF = g.MeasureString(str, font, maxWidth);
            g.Dispose();

            return new Size((int)Math.Ceiling(strRectSizeF.Width),
                       (int)Math.Ceiling(strRectSizeF.Height));
        }

        private Font GetCaptionFont()
        {

            NONCLIENTMETRICS ncm = new NONCLIENTMETRICS();
            ncm.cbSize = Marshal.SizeOf(typeof(NONCLIENTMETRICS));
            try
            {
                bool result = SystemParametersInfo(SPI_GETNONCLIENTMETRICS,
                                                   ncm.cbSize, ref ncm, 0);

                if (result)
                {
                    return Font.FromLogFont(ncm.lfCaptionFont);
                }
                else
                {
                    int lastError = Marshal.GetLastWin32Error();
                    return null;
                }
            }
            catch (Exception /*ex*/)
            {
                //System.Console.WriteLine(ex.Message);
            }

            return null;
        }

        private const int SPI_GETNONCLIENTMETRICS = 41;
        private const int LF_FACESIZE = 32;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceSize;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct NONCLIENTMETRICS
        {
            public int cbSize;
            public int iBorderWidth;
            public int iScrollWidth;
            public int iScrollHeight;
            public int iCaptionWidth;
            public int iCaptionHeight;
            public LOGFONT lfCaptionFont;
            public int iSmCaptionWidth;
            public int iSmCaptionHeight;
            public LOGFONT lfSmCaptionFont;
            public int iMenuWidth;
            public int iMenuHeight;
            public LOGFONT lfMenuFont;
            public LOGFONT lfStatusFont;
            public LOGFONT lfMessageFont;
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SystemParametersInfo(int uiAction,
           int uiParam, ref NONCLIENTMETRICS ncMetrics, int fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem,
        uint uEnable);

        private const int SC_CLOSE = 0xF060;
        private const int MF_BYCOMMAND = 0x0;
        private const int MF_GRAYED = 0x1;
        private const int MF_ENABLED = 0x0;

        private void DisableCloseButton(Form form)
        {
            try
            {
                EnableMenuItem(GetSystemMenu(form.Handle, false),
                           SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
            }
            catch (Exception /*ex*/)
            {
                //System.Console.WriteLine(ex.Message);
            }
        }
    }
}
