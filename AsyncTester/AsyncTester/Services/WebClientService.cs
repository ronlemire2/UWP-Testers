using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTester.Services {
    public class WebClientService {

        public async static Task<string> WebPageToString(string url) {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("http://www.comicsyndicate.org/");
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = string.Empty;

            try {
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex) {
                httpResponseBody = string.Format("Error: {0}  Message: ", ex.HResult.ToString("X"), ex.Message);
            }

            return httpResponseBody;
        }
    }
}
