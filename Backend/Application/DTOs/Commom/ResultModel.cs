using Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Commom
{
    public class ResultModel<T>
    {
        public T? Data { get; set; }
        public int Status { get; set; } = (int)HttpStatusCode.OK;
        public string? Title { get { return Tool.GetErrorTitle(Status); } }
        public string? Message { get; set; }
        public Dictionary<string, KeyString>? ValidationErrors { get; set; } = [];

        public ResultModel()
        {
        }

        public ResultModel(T data)
        {
            Data = data;
        }

        public ResultModel(HttpStatusCode status, string? message)
        {
            Status = (int)status;
            Message = message;
        }

        public ResultModel(HttpStatusCode status, Dictionary<string, KeyString> validationErrors)
        {
            Status = (int)status;
            ValidationErrors = validationErrors;
        }
    }

    public class KeyString
    {
        public List<string> Errors { get; set; } = [];
    }

}