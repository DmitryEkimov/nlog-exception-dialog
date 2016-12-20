using System;
using System.Windows.Forms;
using System.Drawing;

namespace NLog.Visar.WinForms
{
    /// <summary>
    /// Общий диалог для отображения сообщения об ошибке
    /// </summary>
    public partial class DlgException : Form
    {

        private const int MIN_WIDTH = 437;
        private const int MIN_HEIGHT = 146;
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public DlgException()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отобразить модальный диалог с детальным описанием исключения
        /// </summary>
        /// <param name="sMessage">Основной текст сообщения</param>
        /// <param name="sDetail">Подробный текст ошибки</param>
        /// <param name="sWindowTitle">Заголовок окна сообщения</param>
        public static void Show(string sMessage, string sDetail, string sWindowTitle = null)
        {
            DlgException dlg = new DlgException();

            // прописать заголовок окна
            if (!string.IsNullOrEmpty(sWindowTitle))
                dlg.Text = sWindowTitle;

            // прописать текст сообщения
            dlg.LblExceptionMessage.Text = sMessage;

            // подробный текст ошибки
            dlg.TbxExceptionDescription.Text = sDetail;

            dlg.ShowDialog(Form.ActiveForm);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            const int down = 0, up = 1;
            if(BtnDetails.ImageIndex == down)
            {
                BtnDetails.ImageIndex = up;
                TbxExceptionDescription.Top = panel1.Top + panel1.Height + 3;
                TbxExceptionDescription.Visible = true;
                this.MinimumSize = new Size(MIN_WIDTH, 85 + LblExceptionMessage.Height + panel1.Height + TbxExceptionDescription.Height);
                this.MaximumSize = new Size(999999, 999999);
                this.Height = 85 + LblExceptionMessage.Height + panel1.Height + TbxExceptionDescription.Height;
               
            }
            else
            {
                BtnDetails.ImageIndex = down;
                TbxExceptionDescription.Visible = false;
                this.Width = 437;
                this.Height = 60 + LblExceptionMessage.Height + panel1.Height;
                this.MinimumSize = new Size(MIN_WIDTH, 60 + LblExceptionMessage.Height + panel1.Height > MIN_HEIGHT ? 60 + LblExceptionMessage.Height + panel1.Height : MIN_HEIGHT);
                this.MaximumSize = new Size(MIN_WIDTH, 60 + LblExceptionMessage.Height + panel1.Height > MIN_HEIGHT ? 60 + LblExceptionMessage.Height + panel1.Height : MIN_HEIGHT); 
            }
        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TbxExceptionDescription.Text);
        }

        private void dlgException_Load(object sender, EventArgs e)
        {
            this.Width = 437;
            this.Height = 60 + LblExceptionMessage.Height + panel1.Height;
            this.MinimumSize = new Size(MIN_WIDTH, 60 + LblExceptionMessage.Height + panel1.Height > MIN_HEIGHT ? 60 + LblExceptionMessage.Height + panel1.Height : MIN_HEIGHT);
            this.MaximumSize = new Size(MIN_WIDTH, 60 + LblExceptionMessage.Height + panel1.Height > MIN_HEIGHT ? 60 + LblExceptionMessage.Height + panel1.Height : MIN_HEIGHT);


            toolTip1.SetToolTip(BtnDetails, "Развернуть/Свернуть подробный отчёт");
            toolTip1.SetToolTip(BtnCopy, "Скопировать текст исключения в буфер обмена");
            toolTip1.SetToolTip(BtnContinue, "Продолжить работу");
        }

        private void dlgException_Shown(object sender, EventArgs e)
        {
            int nPanelTop = LblExceptionMessage.Top + LblExceptionMessage.Height + LblExceptionMessage.Margin.Bottom;

            // сдвинуть, если не влазиет
            if (panel1.Top < nPanelTop)
                panel1.Top = nPanelTop;
        }

        private void dlgException_Resize(object sender, EventArgs e)
        {
            const int up = 1;
            if (BtnDetails.ImageIndex == up)
            {
                TbxExceptionDescription.Height = this.ClientSize.Height - TbxExceptionDescription.Top;
            }
        }

        // Вытаскивание окна наверх
        [System.Runtime.InteropServices.DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
