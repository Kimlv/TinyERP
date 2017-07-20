namespace App.Common
{
    using System;

    [Flags]
    public enum ItemStatus
    {
        None = 0,
        InActive = 1,
        Active = 2,
        Deleted = 3,
        WaitForActivating = 4,
        WaitForApproving = 5,
        New = 6,
        Resolved = 7,
        Cancelled = 8
    }

    [Flags]
    public enum IOMode
    {
        Read,
        Write
    }

    public enum DatabaseType
    {
        MSSQL,
        MongoDB,
        ElasticSearch
    }

    [Flags]
    public enum ApplicationType
    {
        Console = 1,
        MVC = 2,
        WebApi = 4,
        UnitTest = 8,
        IntegraitonTest = 16,
        ExternalWebApi = 32,
        MessageBus = 64,
        All = 127
    }

    [Flags]
    public enum UserRole
    {
        None = 0,
        BugLeader = 2,
        Developer = 4,
        AddressedServicesOnly = 8,
        MasterGeography = 16,
        MasterEconomy = 32,
        MasterDistribution = 64,
        MasterProject = 128,
        MasterSales = 256,
        MasterAdministration = 512,
        Master = 1023
    }

    public enum AuthenticationType
    {
        Normal,
        User
    }

    public enum AssemblyType
    {
        Common,
        Context,
        Repository,
        Service
    }

    [Flags]
    public enum SkillLevel
    {
        Junior,
        Senior,
        Leader,
        Manager
    }

    public enum QuestionType
    {
        Sinlge,
        Multiple
    }

    public enum ExamStatus
    {
        Draft,
        Approved,
        Pending,
        Expired,
        Closed
    }

    public enum FTPResourceType
    {
        Directory,
        File
    }

    public enum ResourceType
    {
        Resource,
        MailTemplate,
        Text
    }

    public enum ThumbnailType
    {
        Small,
        Medium,
        Large,
        Origin
    }

    public class FileContentType
    {
        public const string Png = "image/png";
        public const string Jpeg = "image/jpeg";
    }

    public enum ThumbnailSize
    {
        Small,
        Medium,
        Large,
        Ogirin
    }

    public class FileContentExt
    {
        public const string Png = "PNG";
        public const string Jpg = "JPG";
        public const string Jpeg = "JPEG";
        public const string Doc = "DOC";
        public const string Pdf = "PDF";
        public const string Xsl = "XSL";
        public const string Xslx = "XSLX";
        public const string UnSupported = "UnSupported";
    }

    public enum ParameterParentType
    {
        None,
        ContentType
    }

    public enum ParameterValueType
    {
        String,
        Number,
        Password
    }

    public class FormValidationRules
    {
        public const int MaxNameLength = 50;
        public const int MaxDescriptionLength = 512;
    }

    public enum DataType
    {
        String,
        Number,
        Object,
        Guid
    }

    public enum DataOperationType
    {
        None,
        Create,
        Update,
        Delete
    }

    public enum ConnectorType
    {
        REST
    }

    public enum CommandHandlerStategyType
    {
        External,
        InApp
    }

    public enum EventHandlerStategyType
    {
        External,
        InApp
    }
    public enum EventPriority
    {
        Low = 5,
        Normal = 10,
        Medium = 50,
        High = 100,
        Critical = 1000
    }

    public enum RepositoryType
    {
        MongoDb = 1,
        MSSQL = 2,
        Elastic = 4,
        RabitMQ = 8,
        Azure = 16
    }

    public enum TaskPriority
    {
        CriticalHigh = 1,
        VeryHigh = 10,
        High = 50,
        Medium = 100,
        Normal = 1000
    }

    public enum UserAccountStatus
    {
        Active = 1,
        InActive = 2,
        Pending = 3,
        Deleted = 4,
    }
    [Flags]
    public enum AuthType
    {
        /// <summary>
        /// User name/ pwd authentication
        /// </summary>
        OwinBasic = 1,
        OwinTokenBase = 2,
        Owin = 127,
        Google = 128
    }
    public struct SecurityRoleType
    {
        public const string Administrator = "Administrator";
        public const string SuperUser = "SuperUser";
        public const string User = "User";
        public const string Guest = "Guest";
    }

    public struct DomainType
    {
        public const string Security = "Security";
        public const string Order = "Order";
    }
    [Flags]
    public enum BusEventSubcriberStatus
    {
        Active,
        InActive
    }

    public class ConnectionStrings
    {
        public static readonly string DefaultMessageBus = "DefaultMessageBus";
    }
}
