using System.Windows.Forms;
using NLog.Targets;
using NLog.Visar.Targets;

namespace NLog.Visar.WinForms.Targets
{

    /// <summary>
    /// Реализовано по мотивам MessageBoxTarget из NLog.Windows.Forms
    /// </summary>
    [Target("ExceptionBox")]
    public sealed class ExceptionBoxTarget : TargetForDialog
    {
        /// <summary>
        /// Отображаем расширенный вариант диалога при наличии детальной информации
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
                MessageBox.Show(message, caption, MessageBoxButtons.OK, ToMessageBoxIcon(replaceIcon));
            }
            else
            {
                DlgException.Show(message, detail, caption);
            }
        }

        private MessageBoxIcon ToMessageBoxIcon(LogMessageBoxIcon logIcon)
        {
            return logIcon == LogMessageBoxIcon.Information ? MessageBoxIcon.Information
                : (logIcon == LogMessageBoxIcon.Warning ? MessageBoxIcon.Warning
                : (logIcon == LogMessageBoxIcon.Error ? MessageBoxIcon.Error
                : MessageBoxIcon.None));
        }

    }
}
