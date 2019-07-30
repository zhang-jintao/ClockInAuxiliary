using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockInAuxiliary
{
    class CMDManager
    {
        public static int Shuttime { get; private set; }

        public static void shutDown(int time)
        {
            CMDManager.command("shutdown -s -t " + time);
        }
        public static void repealShutDown()
        {
            CMDManager.command("shutdown -a");
        }


        private static void command(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.WriteLine("exit");
            process.WaitForExit();
            process.Close();
        }

    }
}
