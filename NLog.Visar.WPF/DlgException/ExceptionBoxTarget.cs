using System;
using System.Text;
using System.Windows;
using NLog.Targets;
using NLog.Common;
using NLog.Layouts;
using NLog.Visar.WPF.DlgException;
using NLog.Visar.Targets;

namespace NLog.Visar.WinForms.Targets
{

    /// <summary>
    /// Реализовано по мотивам MessageBoxTarget из NLog.Windows.Forms
    /// </summary>
    /// <seealso cref="NLog.Targets.TargetWithLayout" />
    [Target("ExceptionBox")]
    public sealed class ExceptionBoxTarget : TargetForDialog
    {
        /// <summary>
        /// Отображаем сообщение в стандартном диалоге
        /// </summary>
        /// <param name="replaceIcon"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="detail"></param>
        protected override void ShowMessageDialog(LogMessageBoxIcon replaceIcon, string caption, string message, string detail)
        {
            // при отсутствии детальной информации используем стандартный диалог
            if (string.IsNullOrEmpty(detail))
            {
                MessageBox.Show(message, caption, MessageBoxButton.OK, ToMessageBoxImage(replaceIcon));
            }
            else
            {
                dlgException2.Show(detail, message, caption);
            }            
        }

        private MessageBoxImage ToMessageBoxImage(LogMessageBoxIcon logIcon)
        {
            return logIcon == LogMessageBoxIcon.Information ? MessageBoxImage.Information
                : (logIcon == LogMessageBoxIcon.Warning ? MessageBoxImage.Warning
                : (logIcon == LogMessageBoxIcon.Error ? MessageBoxImage.Error
                : MessageBoxImage.None));
        }
    }
}
