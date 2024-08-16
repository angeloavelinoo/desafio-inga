﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tools
{
    public static class Tool
    {



        public static string FormatDateToCustomPastString(DateTime date)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            var specificTime = TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById(localZone.Id));

            var timeDifference = ConvertTimeZones(DateTime.UtcNow) - date;
            var minutesDifference = (int)Math.Floor(timeDifference.TotalMinutes);

            string customPastStrig;

            if (minutesDifference < 1)
            {
                customPastStrig = "Agora";
            }
            else if (minutesDifference < 60)
            {
                customPastStrig = $"Há {minutesDifference} minutos";
            }
            else if (timeDifference.TotalHours < 24)
            {
                customPastStrig = $"Há {timeDifference.TotalHours:F0} horas";
            }
            else if (timeDifference.TotalHours < 48)
            {
                customPastStrig = "Ontem";
            }
            else
            {
                customPastStrig = specificTime.ToString();
            }

            return customPastStrig;
        }

        public static DateTime ConvertTimeZones(DateTime dateTime)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, localZone);
        }
        public static string GetErrorTitle(int statusCode)
        {
            return statusCode switch
            {
                (int)HttpStatusCode.Continue => "Continuar",
                (int)HttpStatusCode.SwitchingProtocols => "Mudando protocolos",
                (int)HttpStatusCode.Processing => "Processando",
                (int)HttpStatusCode.EarlyHints => "Dicas antecipadas",
                (int)HttpStatusCode.OK => "OK",
                (int)HttpStatusCode.Created => "Criado",
                (int)HttpStatusCode.Accepted => "Aceito",
                (int)HttpStatusCode.NonAuthoritativeInformation => "Informação não autoritativa",
                (int)HttpStatusCode.NoContent => "Sem conteúdo",
                (int)HttpStatusCode.ResetContent => "Redefinir conteúdo",
                (int)HttpStatusCode.PartialContent => "Conteúdo parcial",
                (int)HttpStatusCode.MultiStatus => "Multi status",
                (int)HttpStatusCode.AlreadyReported => "Já reportado",
                (int)HttpStatusCode.IMUsed => "IM utilizado",
                (int)HttpStatusCode.Ambiguous => "Ambíguo",
                (int)HttpStatusCode.Moved => "Movido",
                (int)HttpStatusCode.Found => "Encontrado",
                (int)HttpStatusCode.RedirectMethod => "Método de redirecionamento",
                (int)HttpStatusCode.NotModified => "Não modificado",
                (int)HttpStatusCode.UseProxy => "Usar proxy",
                (int)HttpStatusCode.Unused => "Não utilizado",
                (int)HttpStatusCode.RedirectKeepVerb => "Redirecionar mantendo verbo",
                (int)HttpStatusCode.PermanentRedirect => "Redirecionamento permanente",
                (int)HttpStatusCode.BadRequest => "Requisição inválida",
                (int)HttpStatusCode.Unauthorized => "Não autorizado",
                (int)HttpStatusCode.PaymentRequired => "Pagamento necessário",
                (int)HttpStatusCode.Forbidden => "Acesso proibido",
                (int)HttpStatusCode.NotFound => "Recurso não encontrado",
                (int)HttpStatusCode.MethodNotAllowed => "Método não permitido",
                (int)HttpStatusCode.NotAcceptable => "Não aceitável",
                (int)HttpStatusCode.ProxyAuthenticationRequired => "Autenticação de proxy necessária",
                (int)HttpStatusCode.RequestTimeout => "Tempo de requisição esgotado",
                (int)HttpStatusCode.Conflict => "Conflito",
                (int)HttpStatusCode.Gone => "Desaparecido",
                (int)HttpStatusCode.LengthRequired => "Comprimento necessário",
                (int)HttpStatusCode.PreconditionFailed => "Falha de pré-condição",
                (int)HttpStatusCode.RequestEntityTooLarge => "Entidade da requisição muito grande",
                (int)HttpStatusCode.RequestUriTooLong => "URI da requisição muito longa",
                (int)HttpStatusCode.UnsupportedMediaType => "Tipo de mídia não suportado",
                (int)HttpStatusCode.RequestedRangeNotSatisfiable => "Intervalo solicitado não satisfatório",
                (int)HttpStatusCode.ExpectationFailed => "Falha na expectativa",
                (int)HttpStatusCode.MisdirectedRequest => "Requisição mal direcionada",
                (int)HttpStatusCode.UnprocessableEntity => "Entidade não processável",
                (int)HttpStatusCode.Locked => "Bloqueado",
                (int)HttpStatusCode.FailedDependency => "Dependência falhou",
                (int)HttpStatusCode.UpgradeRequired => "Atualização necessária",
                (int)HttpStatusCode.PreconditionRequired => "Pré-condição necessária",
                (int)HttpStatusCode.TooManyRequests => "Muitas requisições",
                (int)HttpStatusCode.RequestHeaderFieldsTooLarge => "Campos de cabeçalho da requisição muito grandes",
                (int)HttpStatusCode.UnavailableForLegalReasons => "Indisponível por motivos legais",
                (int)HttpStatusCode.InternalServerError => "Erro interno no servidor",
                (int)HttpStatusCode.NotImplemented => "Não implementado",
                (int)HttpStatusCode.BadGateway => "Gateway ruim",
                (int)HttpStatusCode.ServiceUnavailable => "Serviço indisponível",
                (int)HttpStatusCode.GatewayTimeout => "Tempo limite do gateway",
                (int)HttpStatusCode.HttpVersionNotSupported => "Versão do HTTP não suportada",
                (int)HttpStatusCode.VariantAlsoNegotiates => "Variante também negocia",
                (int)HttpStatusCode.InsufficientStorage => "Armazenamento insuficiente",
                (int)HttpStatusCode.LoopDetected => "Loop detectado",
                (int)HttpStatusCode.NotExtended => "Não estendido",
                (int)HttpStatusCode.NetworkAuthenticationRequired => "Autenticação de rede necessária",
                _ => "Erro interno no servidor"
            };

        }
    }
}