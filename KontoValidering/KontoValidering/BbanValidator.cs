using KontoValidering.Models;
using System.Collections.Generic;
using System.Linq;

namespace KontoValidering
{
    public static class BbanValidator
    {
        /// <summary>
        /// Validerer at obligatoriske felter er satt
        /// </summary>
        /// <param name="bban">BBAN konto</param>
        /// <param name="gyldigeLandkoder">Hentes fra ssb http://data.ssb.no/api/klass/v1/classifications/100/codes.json?from={dato}</param>
        /// <returns></returns>
        public static bool GyldigBban(Bban bban, HashSet<string> gyldigeLandkoder)
        {
            return !string.IsNullOrWhiteSpace(bban.Kontonummer)
                && (string.IsNullOrWhiteSpace(bban.Swift) || SwiftErGyldig(bban.Swift))
                && (!string.IsNullOrWhiteSpace(bban.Landkode) && gyldigeLandkoder.Contains(bban.Landkode))
                && !string.IsNullOrWhiteSpace(bban.Banknavn)
                && bban.Bankadresse.Any(x => !string.IsNullOrWhiteSpace(x));
        }

        private static bool SwiftErGyldig(string swift)
            => !string.IsNullOrEmpty(swift) && (swift.Length == 8 || swift.Length == 11);
    }
}
