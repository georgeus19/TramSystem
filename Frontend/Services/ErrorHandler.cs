using System.Net;
using Microsoft.JSInterop;

namespace Frontend.Services; 

public class ErrorHandler {
	private readonly IJSRuntime _jsRuntime;

	public ErrorHandler(IJSRuntime jsRuntime) {
		_jsRuntime = jsRuntime;
	}
	
	public async Task<bool> AlertIfNotMatching(HttpResponseMessage response, IEnumerable<HttpStatusCode> codes) {
		if (!codes.Contains(response.StatusCode)) {
			await _jsRuntime.InvokeAsync<object>("alert", await response.Content.ReadAsStringAsync());
			return true;
		}

		return false;
	}
}