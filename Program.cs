using System;
using System.IO;
using System.Net;
using System.Text;

class SoapTest
{
    static void Main()
    {
        string url = "https://app14.sisprocloud.com.br/SpSped5Service/SpedServicecs.asmx"; // URL do serviço SOAP
        string soapAction = "http://SpSped5Service/SpedServiceCs/SetDocFiscalCs"; // SOAPAction do cabeçalho

        string soapEnvelope = @"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                       xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                       xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
          <soap:Body>
            <SetDocFiscalCs xmlns=""http://SpSped5Service/SpedServiceCs"">
              <!-- Parâmetros fictícios para teste -->
              <inWSrecordSetSpecified>true</inWSrecordSetSpecified>
              <inWSTentantID>1</inWSTentantID>
              <inWSToken>abc123</inWSToken>
              <inWSTokenSpecified>true</inWSTokenSpecified>
            </SetDocFiscalCs>
          </soap:Body>
        </soap:Envelope>";

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", soapAction);
            request.ContentType = "text/xml; charset=utf-8";
            request.Method = "POST";

            byte[] data = Encoding.UTF8.GetBytes(soapEnvelope);
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine("Resposta recebida:");
                    Console.WriteLine(result);
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Erro ao conectar:");
            Console.WriteLine(ex.Message);

            if (ex.Response != null)
            {
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string error = reader.ReadToEnd();
                    Console.WriteLine("Detalhes do erro:");
                    Console.WriteLine(error);
                }
            }
        }
    }
}