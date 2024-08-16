﻿using Application.DTOs.Commom;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace DesafioIngaCodeApi.Extensions
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.FirstOrDefault().Value is BaseDTOValidation dto && dto?.IsValid != true)
            {
                context.Result = new ObjectResult(new ResultModel<dynamic>(System.Net.HttpStatusCode.UnprocessableEntity,
                    dto!.Notifications.GetErros()))
                { StatusCode = (int)System.Net.HttpStatusCode.UnprocessableEntity };
            }
            else if (context.ModelState.IsValid != true)
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();

                context.Result = new ObjectResult(new ResultModel<dynamic>(System.Net.HttpStatusCode.BadRequest,
                    errors.LastOrDefault()))
                { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}