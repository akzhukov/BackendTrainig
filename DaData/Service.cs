using Dadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DaDataConsoleApp
{
    public class CompanyService
    {
		private readonly string token;
		public CompanyService(string token)
		{
			this.token = token;
		}

		public async Task<string> GetCompanyNameByINN(string inn)
		{
			try
			{
				var api = new SuggestClientAsync(token);
				var response = await api.FindParty(inn);
				if (response.suggestions.Count > 0)
				{
					return response.suggestions[0].value;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Operation failed. " + ex.Message);
			}
			return null;
		}

	}
}
