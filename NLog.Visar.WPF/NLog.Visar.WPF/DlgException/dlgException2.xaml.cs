using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;


namespace NLog.Visar.WPF.DlgException
{
    internal class dlgException2VM :INotifyPropertyChanged
    {
        private bool _ExpandDetails;
        public bool ExpandDetails
        {
            get { return _ExpandDetails; }
            set { _ExpandDetails = value; OnPropertyChanged("ExpandDetails"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Логика взаимодействия для dlgException.xaml
    /// </summary>
    public partial class dlgException2
    {
        private dlgException2VM _model;
        public dlgException2()
        {
            InitializeComponent();

            // прописать Owner
            try { this.Owner = Application.Current.MainWindow; } catch { }
            _model = new dlgException2VM() {ExpandDetails = false};
            this.DataContext = _model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.UnicodeText, this.TbxExceptionDescription);     
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            _model.ExpandDetails = !_model.ExpandDetails;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Owner != null)
                if (this.Owner.Icon != null)
                    this.Icon = this.Owner.Icon;
        }


        /// <summary>
        /// Отобразить модальный диалог с описанием исключения
        /// </summary>
        /// <param name="ex">Исключение, которое надо отобразить</param>
        /// <param name="sMessage">Текст сообщения</param>
        /// <param name="sDescription">Подробный текст ошибки</param>
        /// <param name="sWindowTitle">Заголовок окна сообщения</param>
        public static void Show(string detail, string sMessage, string sWindowTitle = null)
        {
            var dlg = new dlgException2
            {
                Title = !string.IsNullOrEmpty(sWindowTitle) ? sWindowTitle : Application.Current.MainWindow.Title,
                tbExceptionMessage = {Text = sMessage},
                TbxExceptionDescription =
                {
                    Text = detail
                }
            };

            // прописать заголовок окна
            // прописать текст сообщения

            // подробный текст ошибки

            dlg.ShowDialog();
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a boolean");

            return ((bool)value)? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
