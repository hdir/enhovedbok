using KontoValidering.Models;
using System.Linq;

namespace KontoValidering
{
    public static class BbanValidator
    {
        public static bool GyldigBban(Bban bban)
        {
            return !string.IsNullOrWhiteSpace(bban.Kontonummer)
                && (string.IsNullOrWhiteSpace(bban.Swift) || SwiftErGyldig(bban.Swift))
                && (!string.IsNullOrWhiteSpace(bban.Landkode) && bban.Landkode.Length == 2)
                && !string.IsNullOrWhiteSpace(bban.Bankkode)
                && !string.IsNullOrWhiteSpace(bban.Banknavn)
                && bban.Bankadresse.Any(x => !string.IsNullOrWhiteSpace(x));
        }

        private static bool SwiftErGyldig(string swift)
            => !string.IsNullOrEmpty(swift) && (swift.Length == 8 || swift.Length == 11);
    }
}
