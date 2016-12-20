using System;
using System.Windows;

namespace NLog.Visar.WPF.DlgException
{
    /// <summary>
    /// Логика взаимодействия для DlgException.xaml
    /// </summary>
    public partial class DlgException : Window
    {
        private const int MIN_WIDTH = 437;
        private const int MIN_HEIGHT = 146;

        public DlgException()
        {
            InitializeComponent();
            this.Loaded += DlgException_Loaded;
            this.SizeChanged += DlgException_SizeChanged;

        }

        private void DlgException_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //const int up = 1;
            //if (Details.ImageIndex == up)
            //{
            //    TbxExceptionDescription.Height = (this.Content as FrameworkElement).ActualHeight - TbxExceptionDescription.Margin.Top;
            //}
        }

        private void DlgException_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = 437;
            this.Height = 80 + LblExceptionMessage.Height + Panel1.Height;
            this.MinWidth = MIN_WIDTH;
            this.MaxWidth = MIN_WIDTH;
            this.MinHeight = 80 + LblExceptionMessage.Height + Panel1.Height > MIN_HEIGHT ? 80 + LblExceptionMessage.Height + Panel1.Height : MIN_HEIGHT;
            this.MaxHeight = 80 + LblExceptionMessage.Height + Panel1.Height > MIN_HEIGHT ? 80 + LblExceptionMessage.Height + Panel1.Height : MIN_HEIGHT;
        }

        private void Copy_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.UnicodeText, TbxExceptionDescription.Text);
        }

        private void Continue_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Details_OnClick(object sender, RoutedEventArgs e)
        {
            //const int down = 0, up = 1;
            //if (Details.ImageIndex == down)
            //{
            //    Details.ImageIndex = up;
            //    //TbxExceptionDescription.Margin.Top = Panel1.Margin.Top + Panel1.Height + 3;
            //    TbxExceptionDescription.Visibility = Visibility.Visible;
            //    this.MinHeight = 85 + LblExceptionMessage.Height + Panel1.Height + TbxExceptionDescription.Height;
            //    this.MinWidth = MIN_WIDTH;
            //    this.MaxHeight = 999999;
            //    this.MaxWidth = 999999;
            //    this.Height = 85 + LblExceptionMessage.Height + Panel1.Height + TbxExceptionDescription.Height;

            //}
            //else
            //{
            //    Details.ImageIndex = down;
            //    TbxExceptionDescription.Visibility = Visibility.Collapsed;
            //    this.Width = 437;
            //    this.Height = 60 + LblExceptionMessage.Height + Panel1.Height;
            //    this.MinHeight = 60 + LblExceptionMessage.Height + Panel1.Height > MIN_HEIGHT ? 60 + LblExceptionMessage.Height + Panel1.Height : MIN_HEIGHT;
            //    this.MinWidth = MIN_WIDTH;
            //    this.MaxWidth = MIN_WIDTH;
            //    this.MaxHeight =  60 + LblExceptionMessage.Height + Panel1.Height > MIN_HEIGHT ? 60 + LblExceptionMessage.Height + Panel1.Height : MIN_HEIGHT;
            //}
        }

        /// <summary>
        /// Отобразить модальный диалог с описанием исключения
        /// </summary>
        /// <param name="ex">Исключение, которое надо отобразить</param>
        /// <param name="sMessage">Текст сообщения</param>
        /// <param name="sDescription">Подробный текст ошибки</param>
        /// <param name="sWindowTitle">Заголовок окна сообщения</param>
        public static void Show(Exception ex, string sMessage, string sDescription, string sWindowTitle = null)
        {
            DlgException dlg = new DlgException();

            // прописать заголовок окна
            if (!string.IsNullOrEmpty(sWindowTitle))
                dlg.Title = sWindowTitle;
            else dlg.Title = Application.Current.MainWindow.Title;
            // прописать текст сообщения
            dlg.LblExceptionMessage.Text = sMessage;

            // подробный текст ошибки
            dlg.TbxExceptionDescription.Text = sMessage
                + Environment.NewLine + Environment.NewLine
                + sDescription;

            dlg.ShowDialog();
        }
    }
}
