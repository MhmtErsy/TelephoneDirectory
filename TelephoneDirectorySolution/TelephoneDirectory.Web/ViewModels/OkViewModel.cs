using System;

using TelephoneDirectory.Web.ViewModels;

namespace TelephoneDirectory.Web.ViewModels
{
    public class OkViewModel:NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "Succesful.";

        }
    }
}