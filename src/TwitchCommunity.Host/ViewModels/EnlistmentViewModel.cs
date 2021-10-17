using System;
using TwitchCommunity.Application.Enlistments;

namespace TwitchCommunity.Host.ViewModels
{
    public class EnlistmentViewModel
    {
        private readonly Enlistment enlistment;

        public EnlistmentViewModel(Enlistment enlistment)
        {
            this.enlistment = enlistment;
        }

        public int Id => enlistment.Id;

        public string UserName => enlistment.UserName;

        public DateTime Timestamp => enlistment.Timestamp;

        public EnlistmentState State => enlistment.State;

        public bool IsChecked { get; set; }
    }
}
