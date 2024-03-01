using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities; // Add this line

namespace TaxaApp.Pages
{
    public partial class PageTwo
    {
        [Parameter] public string? AddressStart { get; set; }
        [Parameter] public string? AddressEnd { get; set; }
        [Parameter] public string? Distance { get; set; }
        [Parameter] public string? Duration { get; set; }
        [Parameter] public string? Price { get; set; }



        [Inject]
        public IModalService ModalService { get; set; }
    }
}
