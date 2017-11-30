using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorkHelper
{
    class GitHelper
    {
        public static void LocalCheckout(string localBranch, bool isExist)
        {
            if (isExist)
            {
                Process process = Process.Start("git", "checkout " + localBranch);
                process.Close();
            }
            else
            {
                Process process = Process.Start("git", "checkout -B " + localBranch);
                process.Close();
            }
        }


        public static void Checkout(string branch, string repos = "origin")
        {
            Process process = Process.Start("git", string.Format("checkout -f -B test {1}/{0}",
                branch, repos));
            process.Close();
        }

        /// <summary>
        /// Получает строку с логами текущей git ветки
        /// </summary>
        /// <returns>
        /// Строка с логами
        /// </returns>
        public static string Log()
        {
            // Задаем параметры процесса
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "git";
            psi.Arguments = "log";
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.UseShellExecute = false;
            // Создаем процесс
            Process process = Process.Start(psi);
            string _out = process.StandardOutput.ReadToEnd();
            process.Close();
            return _out;
        }

    }
}
