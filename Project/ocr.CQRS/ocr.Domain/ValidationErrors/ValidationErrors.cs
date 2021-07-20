using System;
using System.Collections.Generic;
using System.Text;

namespace ocr.Domain.ValidationErrors
{
    public static class ValidationErrors
    {
        public static class Generic
        {
            public const string NullCommand = "You must provide a non-null command.";

            public const string NullQuery = "You must provide a non-null request.";
        }

        public static class Document
        {
            public const string MustAddAtLeastOneDocument = "You must provide at least one document";
            public const string CorruptedFile = "File is corrupted, upload new one";
        }

        public static class Tab
        {
            public const string InvalidId = "Invalid Id";
            public const string InvalidInitiatorName = "Invalid initiator name";
            public static string AlreadyExists(Guid id) => $"Tab with id:{id} is already exists";
            public static string NotFound(Guid id) => $"Tab with id:{id} not found";
            public static string NotOpen(Guid id) => $"Tab with id:{id} is not open";
            public const string EmptyFinalText = "Final text can not be null or empty";
        }
    }
}
