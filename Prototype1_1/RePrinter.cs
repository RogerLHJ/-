using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;

namespace Prototype1_1
{
    public class RePrinter
    {
        PrintDocument printDoc;
        PageSetupDialog pageSetupDlg;
        PrintPreviewDialog previewDlg;
        PrintDialog printDlg;

        

        int printedRowsCount = 0;
        public Font printFont;

        public string mainTitle = "挂号单";
        public string subTitle = "";
        public string[] str;

        public RePrinter(string[] str)
        {
            printFont = SystemFonts.DefaultFont;
            this.str = str; 
            printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            pageSetupDlg = new PageSetupDialog();
            pageSetupDlg.Document = printDoc;
            pageSetupDlg.MinMargins = new Margins(10, 10, 10, 10);

            previewDlg = new PrintPreviewDialog();
            previewDlg.Document = printDoc;
            previewDlg.Width = 300;
            previewDlg.Height = 300;
            previewDlg.PrintPreviewControl.Zoom = 1.0;

            printDlg = new PrintDialog();
            printDlg.Document = printDoc;

        }
        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            Pen myPen = new Pen(Color.Gray);
            Brush brush = Brushes.LightGray;

            //////////////////////////打印表头///////////////////////////////////
            if (printedRowsCount == 0)
            {
                if (mainTitle != "")
                {
                    Font mainTitleFont = new Font(new FontFamily("宋体"), 20, FontStyle.Bold);
                    SizeF strSize = e.Graphics.MeasureString(mainTitle, mainTitleFont, int.MaxValue);
                    if (e.PageBounds.Width > strSize.Width)
                    {
                        x = (e.PageBounds.Width - Convert.ToInt32(strSize.Width)) / 2;
                    }
                    e.Graphics.DrawString(mainTitle, mainTitleFont, Brushes.Black, x, y);
                    y += Convert.ToInt32(strSize.Height) + 5;
                    x = e.MarginBounds.Left;
                }
                if (subTitle != "")
                {
                    Font subTitleFont = new Font(new FontFamily("宋体"), 12, FontStyle.Bold);
                    SizeF strSize = e.Graphics.MeasureString(subTitle, subTitleFont, int.MaxValue);
                    if (e.PageBounds.Width > strSize.Width + 80)
                    {
                        x = (e.PageBounds.Width - Convert.ToInt32(strSize.Width)) * 2 / 3;
                    }
                    e.Graphics.DrawString(subTitle, subTitleFont, Brushes.Black, x, y);
                    y += Convert.ToInt32(strSize.Height) + 5;
                    x = e.MarginBounds.Right;
                }

                for (int i = 0; i < str.Length; i++)
                {
                    Font font = new Font(new FontFamily("宋体"), 10, FontStyle.Bold);
                    SizeF strsize = e.Graphics.MeasureString(str[i], font, int.MaxValue);
                    if (e.PageBounds.Width > strsize.Width + 80)
                    {
                        x = e.PageBounds.Width * 1/3;
                    }
                    e.Graphics.DrawString(str[i], font, Brushes.Black, x, y);

                    y += Convert.ToInt32(strsize.Height) + 5;
                    x = e.MarginBounds.Right;
                }
            }



            //////////////////////////////////////////////////////////////////////
            ///



            
          
          

            printedRowsCount = 0;
        }

        /// <summary>
        /// 将字符串打印在指定矩形的中间
        /// </summary>
        /// <param name="g"></param>
        /// <param name="str">要打印的字符串</param>
        /// <param name="rect">目标位置矩形</param>
        private void DrawStringCenter(Graphics g, string str, Rectangle rect)
        {
            PointF resPos = new PointF(rect.X, rect.Y);
            SizeF strSize = g.MeasureString(str, printFont, int.MaxValue);
            if (rect.Width > strSize.Width)
            {
                resPos.X = rect.X + (rect.Width - Convert.ToInt32(strSize.Width)) / 2;
            }
            if (rect.Height > strSize.Height)
            {
                resPos.Y = rect.Y + (rect.Height - Convert.ToInt32(strSize.Height)) / 2;
            }
            Rectangle newRect = new Rectangle(Convert.ToInt32(resPos.X), Convert.ToInt32(resPos.Y), rect.Width, rect.Height);
            g.DrawString(str, printFont, Brushes.Black, newRect, StringFormat.GenericTypographic);
        }

        public void PrintDataGridView()
        {
        
            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                previewDlg.ShowDialog();
            }
        }



        public void SetupPage()
        {
            pageSetupDlg.ShowDialog();
        }
        public DialogResult PrintPreview()
        {
            return previewDlg.ShowDialog();
        }

        public DialogResult ShowPrintDialog()
        {
            return printDlg.ShowDialog();
        }
    }
}
