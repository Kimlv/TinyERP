namespace App.Common.Helpers
{
    using App.Common.Configurations;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Resources;

    public class ResourceHelper
    {
        private const string RESOURCE_ASSEMBLY_NAME = "Application.Resources";
        public static void ResolveMessage<IPairItemType>(System.Collections.Generic.IList<IPairItemType> items) where IPairItemType : IResourceItem
        {
            foreach (var item in items)
            {
                item.Message = ResourceHelper.Resolve(item);
            }
        }

        public static string Resolve(IResourceItem item)
        {
            string message = ResourceHelper.Resolve(item.Key);
            if (item.Params == null || item.Params.Count <= 0) { return message; }
            for (int index = 0; index < item.Params.Count; index++)
            {
                message = message.Replace("{" + index + "}", item.Params[index]);
            }

            return message;
        }

        public static string Resolve(string key)
        {
            ResourceType type = ResourceHelper.GetResourceType(key);
            key = ResourceHelper.GetKeyValue(key, type);
            switch (type)
            {
                case ResourceType.Resource:
                    return ResourceHelper.GetResourceContent(key);
                case ResourceType.MailTemplate:
                    return ResourceHelper.GetMailTemplateContent(key);
                case ResourceType.Text:
                default:
                    return ResourceHelper.GetTextResourceKey(key);
            }
        }

        private static string GetMailTemplateContent(string key)
        {
            string filePath = Path.Combine(Configuration.Current.Folder.MailTemplate, key);
            return FileHelper.GetContent(filePath);
        }

        private static string GetResourceContent(string key)
        {
            string[] keys = key.Split('.');
            string baseName = string.Format("{0}.{1}", RESOURCE_ASSEMBLY_NAME, keys[0]);
            ResourceManager resourceManager = new ResourceManager(baseName, Assembly.Load(ResourceHelper.RESOURCE_ASSEMBLY_NAME));
            return resourceManager.GetString(keys[1]);
        }

        private static string GetTextResourceKey(string key)
        {
            return key;
        }

        private static string GetKeyValue(string key, ResourceType type)
        {
            string emptyResourceKey = string.Format(Constants.RESOURCE_KEY_PATTERN, type.ToString(), string.Empty);
            return key.Replace(emptyResourceKey, string.Empty);
        }

        private static ResourceType GetResourceType(string key)
        {
            string emptyResourceKey = string.Format(Constants.RESOURCE_KEY_PATTERN, string.Empty, string.Empty);
            if (key.IndexOf(emptyResourceKey) < 0)
            {
                return ResourceType.Resource;
            }

            string resourceType = key.Substring(0, key.IndexOf(emptyResourceKey));
            return EnumHelper.Convert<ResourceType>(resourceType);
        }

        public static string ToResourceKey(string key, ResourceType type = ResourceType.Resource)
        {
            return string.Format(Constants.RESOURCE_KEY_PATTERN, type.ToString(), key);
        }
    }
}