using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KontoValidering
{
    public static class IbanValidator
    {
        private static Dictionary<int, List<string>> IbanLengder { get; set; } = new Dictionary<int, List<string>>
        {
            { 15, new List<string> { "NO" } },
            { 16, new List<string> { "BE", "BI" } },
            { 18, new List<string> { "DK", "FO", "FI", "GL", "NL" } },
            { 19, new List<string> { "MK", "SI" } },
            { 20, new List<string> { "AT", "BA", "EE", "KZ", "XK", "LT", "LU" } },
            { 21, new List<string> { "CR", "HR", "LV", "LI", "CH" } },
            { 22, new List<string> { "BH", "BG", "GE", "DE", "GG", "IE", "IM", "JE", "ME", "RS", "GB" } },
            { 23, new List<string> { "GI", "IQ", "IL", "TL", "AE" } },
            { 24, new List<string> { "DZ", "AD", "VG", "CZ", "MD", "PK", "RO", "SA", "SK", "ES", "SE", "TN" } },
            { 25, new List<string> { "AO", "CV", "MZ", "PT", "ST" } },
            { 26, new List<string> { "IS", "IR", "TR" } },
            { 27, new List<string> { "BF", "CM", "FR", "CG", "EG", "GA", "GR", "IT", "MG", "MR", "MC", "SM" } },
            { 28, new List<string> { "AL", "AZ", "BY", "BJ", "CY", "DO", "GT", "HU", "CI", "LB", "ML", "PL", "SN" } },
            { 29, new List<string> { "BR", "PS", "QA", "UA" } },
            { 30, new List<string> { "JO", "KW", "MU" } },
            { 31, new List<string> { "MT", "SC" } },
            { 32, new List<string> { "LC" } }
        };

        public static bool GyldigIban(string iban, string swift)
            => GyldigSjekksumIban(iban) && SwiftErGyldig(swift);

        private static bool SwiftErGyldig(string swift)
            => !string.IsNullOrEmpty(swift) && (swift.Length == 8 || swift.Length == 11);

        private static bool GyldigSjekksumIban(string iban)
        {
            if (string.IsNullOrEmpty(iban) || !IbanHarKorrektLengde(iban))
            {
                return false;
            }

            var omgjortIban = iban.Substring(4, iban.Length - 4) + iban.Substring(0, 4);
            omgjortIban = OmgjørStrengTilTall(omgjortIban);
            var rest = Modulo97(omgjortIban);

            return rest == 1;
        }

        private static bool IbanHarKorrektLengde(string iban)
        {
            if (iban.Length <= 2)
            {
                return false;
            }

            var landekode = iban.Substring(0, 2);

            var gyldigeLandekoder = IbanLengder.ContainsKey(iban.Length) ? IbanLengder[iban.Length] : new List<string>(0);

            return gyldigeLandekoder.Any(kode => kode.Equals(landekode));
        }

        private static string OmgjørStrengTilTall(string streng)
        {
            var stringBuilder = new StringBuilder();
            int asciiShift = 55;
            foreach (var c in streng)
            {
                int v;
                if (char.IsLetter(c))
                {
                    v = c - asciiShift;
                }
                else
                {
                    v = int.Parse(c.ToString());
                }

                stringBuilder.Append(v);
            }

            return stringBuilder.ToString();
        }

        private static int Modulo97(string moduloStreng)
        {
            string n = moduloStreng.Substring(0, 9);
            int rest = int.Parse(n) % 97;

            for (int i = 9; i < moduloStreng.Length; i += 7)
            {
                if (moduloStreng.Length - 1 < (i + 7))
                {
                    n = rest.ToString() + moduloStreng.Substring(i);
                }
                else
                {
                    n = rest.ToString() + moduloStreng.Substring(i, 7);
                }

                rest = int.Parse(n) % 97;
            }

            return rest;
        }
    }
}
