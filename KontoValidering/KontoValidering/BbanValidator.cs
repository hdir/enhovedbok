using KontoValidering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KontoValidering
{
    public static class BbanValidator
    {
        /// <summary>
        /// Validerer at obligatoriske felter er satt
        /// </summary>
        /// <param name="bban">BBAN konto</param>
        /// <param name="gyldigeLandkoder">Hentes fra ssb http://data.ssb.no/api/klass/v1/classifications/100/codes.json?from={dato}</param>
        public static bool GyldigBban(Bban bban, HashSet<string> gyldigeLandkoder)
        {
            return !string.IsNullOrWhiteSpace(bban.Kontonummer)
                && (string.IsNullOrWhiteSpace(bban.Swift) || SwiftErGyldig(bban.Swift))
                && (!string.IsNullOrWhiteSpace(bban.Landkode) && gyldigeLandkoder.Contains(bban.Landkode))
                && !string.IsNullOrWhiteSpace(bban.Banknavn)
                && bban.Bankadresse.Any(x => !string.IsNullOrWhiteSpace(x));
        }

        /// <summary>
        /// Eksempel kode for å hente ssb koder. I virkeligheten vil landkodene fra ssb bli håndtert av en annen applikasjon som cacher dette for oss
        /// Det er ikke anbefalt å instansiere httpclient på samme måte som i eksempelet under :)
        /// </summary>
        public static async Task<HashSet<string>> HentSsbLandkoder()
        {
            using var httpClient = new HttpClient();
            var uri = $"http://data.ssb.no/api/klass/v1/classifications/100/codes.json?from={DateTime.Today.ToString("yyyy-MM-dd")}";
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var ssbResponse = await JsonSerializer.DeserializeAsync<SsbResponse>(await response.Content.ReadAsStreamAsync(), options);
            return new HashSet<string>(ssbResponse.Codes.Select(x => x.Code));
        }

        private static bool SwiftErGyldig(string swift)
            => !string.IsNullOrEmpty(swift) && (swift.Length == 8 || swift.Length == 11);
    }
}
