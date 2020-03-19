namespace KontoValidering
{
    public static class NorskKontoValidator
    {
        public static bool GyldigNorskKontonummer(string kontonummer)
        {
            if (string.IsNullOrWhiteSpace(kontonummer))
            {
                return false;
            }

            kontonummer = kontonummer
                .Trim()
                .Replace(".", string.Empty)
                .Replace(" ", string.Empty);

            if (kontonummer.Length != 11 || !ErNumerisk(kontonummer))
            {
                return false;
            }

            var kontrollnummer = (int)char.GetNumericValue(kontonummer[kontonummer.Length - 1]);

            return FinnKontrollnummer(kontonummer) == kontrollnummer;
        }

        private static bool ErNumerisk(string kontonummer) => long.TryParse(kontonummer, out _);

        private static int FinnKontrollnummer(string kontonummer)
        {
            var sisteIndeks = kontonummer.Length - 1;
            var sum = 0;

            for (int i = 0; i < sisteIndeks; i++)
            {
                sum += (int)char.GetNumericValue(kontonummer[i]) * FinnVektnummer(i);
            }

            var rest = sum % 11;

            return FinnKontrollnummerFraRest(rest);
        }

        private static int FinnVektnummer(int indeks) => 7 - ((indeks + 2) % 6);

        private static int FinnKontrollnummerFraRest(int rest) => rest == 0 ? 0 : 11 - rest;
    }
}
