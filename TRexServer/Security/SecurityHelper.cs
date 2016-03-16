using System;
using System.Security.Cryptography;
using System.Text;
using TRexServer.Models;
using TRexServer.Properties;

namespace TRexServer.Security
{
    /// <summary>
    /// Pomocná třída pro vyhodnocení příchozích dat z hlediska bezpečnosti
    /// </summary>
    public class SecurityHelper
    {
        /// <summary>
        /// Ověří, zda v DTO objektu uložený bezepčnostní řetězec (klíč) odpovídá klíči, který je uložen v nastavení serveru
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckAccessKey(StatusDTO data)
        {
            //pokud na serveru není nastavena hodnota bezpečnostního řetězce, tak vrať true
            if (string.IsNullOrWhiteSpace(Settings.Default.AccessKey)) return true;

            //pokud nepřišel bezepčnostní řetězec v datech, tak vrať false
            if (string.IsNullOrWhiteSpace(data.k)) return false;

            return data.k.Equals(GetMd5Hash(data.i + data.t?.ToString("yyyy-MM-dd'T'HH:mm:ss") + Settings.Default.AccessKey));
        }

        /// <summary>
        /// Vrátí MD5 hash řetězce.
        /// </summary>
        /// <param name="inputString">Řetězec znaků v UTF8</param>
        /// <returns>Base64 string z MD5 vstupního řetězce</returns>
        public static string GetMd5Hash(string inputString)
        {
            var tmpSource = Encoding.UTF8.GetBytes(inputString);
            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return Convert.ToBase64String(tmpHash);
        }
    }
}