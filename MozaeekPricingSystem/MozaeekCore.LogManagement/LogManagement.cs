using System.Diagnostics;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandHandler;
using Newtonsoft.Json;

namespace MozaeekCore.LogManagement
{
    public class DoLogManagement : ILogManagement
    {
        private readonly CurrentUser _currentUser;

        public DoLogManagement(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }


        public void DoLog<T>(T command)
        {
            var t = command.GetType();
            var serialize = JsonConvert.SerializeObject(command);
            var log = $"{_currentUser.UserName} is doing {t.Name}  with this data ( {serialize} ) ";
            Debug.WriteLine(log);
        }
    }

}