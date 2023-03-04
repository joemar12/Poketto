﻿namespace Poketto.Application.Common;

public static class Constants
{
    public const string SeederUserName = "seeder";
}

public static class ErrorCodes
{
    public static class Authentication
    {
        public const string NotAuthorized = "AUTH_NOT_AUTHORIZED";
        public const string NotAuthenticated = "AUTH_NOT_AUTHENTICATED";
        public const string RequiredScopesNotFound = "AUTH_REQUIRED_SCOPES_NOT_FOUND";
        public const string NoDefaultPolicy = "AUTH_NO_DEFAULT_POLICY";
        public const string PolicyNotFound = "AUTH_POLICY_NOT_FOUND";
    }

    public static string InternalServerError = "INTERNAL_SERVER_ERROR";
    public const string ValidationFailed = "VALIDATION_FAILED";
    public const string NotFound = "RECORD_NOT_FOUND";
}
