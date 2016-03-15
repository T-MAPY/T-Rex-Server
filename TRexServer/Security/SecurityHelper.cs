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
        /// Ověří, zda v DTO objektu uložený bezepčnostní řetězec odpovídá MD5 bezpečnostního řetězce na serveru
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckStatusDto(StatusDTO data)
        {
            //pokud na serveru není nastavena hodnota bezpečnostního řetězce, tak vrať true
            if (string.IsNullOrWhiteSpace(Settings.Default.SecurityString)) return true;

            //pokud nepřišel bezepčnostní řetězec v datech, tak vrať false
            if (string.IsNullOrWhiteSpace(data.c)) return false;

            return data.c.Equals(GetMd5Hash(Settings.Default.SecurityString));
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