﻿using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationService
    {
        public List<Notification> GetAll()=> new NotificationRepository().GetAll();


        public void AddNotification(Notification notification)
        {
            new NotificationRepository().AddNotification(notification);
        }

        public List<Notification> GetNotificationForEmployee(int? departmentId) => new NotificationRepository().GetNotificationForEmployee(departmentId);
        public List<Notification> GetTop3NotificationEmployee(int? departmentId) => new NotificationRepository().GetTop3NotificationForEmployee(departmentId);
    }
}
