using Application.DTOs.Commom;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class ToolService
    {
        public static Dictionary<string, KeyString> GetErros(this IReadOnlyCollection<Notification> notifications)
        {
            Dictionary<string, KeyString> erros = [];

            notifications.GroupBy(gn => gn.Key)
                .ToList()
                .ForEach(gn => erros
                .Add(gn.Key, new()
                {
                    Errors = gn.Select(n => n.Message)
                    .ToList()
                }));
            return erros;
        }

        public static string FormatDate(DateTime date)
        {
            var dateFormated = date.ToString("dd/MM/yyyy HH:mm");

            return dateFormated;
        }
    }
}
