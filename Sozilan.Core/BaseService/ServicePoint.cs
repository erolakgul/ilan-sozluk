using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Core.DataService;

namespace Sozilan.Core.BaseService
{
    public class ServicePoint
    {
        // sınıflardan yeni bir nesne türetir
        private UserService userService;
        private TitleService titleService;
        private EntryService entryService;
        private MesajService mesajService;
        private NotificationService notificationService;

        public UserService UserService { get { return userService ?? new UserService(); } }
        public TitleService TitleService { get { return titleService ?? new TitleService(); } }
        public EntryService EntryService { get { return entryService ?? new EntryService(); } }
        public MesajService MesajService { get { return mesajService ?? new MesajService(); } }
        public NotificationService NotificationService { get  {return notificationService ?? new NotificationService(); } }
    }
}
