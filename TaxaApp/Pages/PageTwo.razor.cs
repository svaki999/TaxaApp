using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace TaxaApp.Pages
{
    public partial class PageTwo
    {
        [Parameter]
        public string? AddressStart { get; set; }

        [Parameter]
        public string? AddressEnd { get; set; }

        [Inject]
        public IModalService ModalService { get; set; }
    }
    
}
