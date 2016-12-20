using System;
using System.Text;
using NLog.Targets;
using NLog.Common;
using NLog.Layouts;
using System.Windows;
using NLog.Visar.Targets;

namespace NLog.Visar.WinForms.Targets
{

    /// <summary>
    /// Реализовано по мотивам MessageBoxTarget из NLog.Windows.Forms
    /// </summary>
    /// <seealso cref="NLog.Targets.TargetWithLayout" />
    [Target("MessageBox")]
    public sealed class MessageBoxTarget : TargetForDialog
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
            MessageBox.Show(message, caption, MessageBoxButton.OK, ToMessageBoxImage(replaceIcon));
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
