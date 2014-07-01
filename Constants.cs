namespace CodePorter
{
    public static class Constants
    {

    }

    public static class XmlConstants
    {
        public static readonly string Xmlns = "xmlns";
    }

    public static class ParameterCheckerMessage
    {
        public static readonly string CheckNull = "参数 {0} 为null.";
        public static readonly string Empty = "参数 {0} 为Empty.";
        public static readonly string CheckNullOrEmpty = "参数 {0} 为空.";
        public static readonly string CheckRange = "参数 {0} 超出范围.";
        public static readonly string Items = ".Items";
    }

    public static class CodePorterSetting
    {
        public static readonly string GetCollection = @"sourceSettings/TravelMoneyDB";
        public static readonly string ConnectionString = @"ConnectionString";
        public static readonly string DefaultNamespace = @"DefaultNamespace";
        public static readonly string DANamespace = @"DANamespace";
        public static readonly string CodeSet = @"CodePorter.Config.CodeSet.xml";
        public static readonly string CheckedNodes = @"CheckedNodes";
        public static readonly string RootNode = @"DataBase";
        public static readonly string Tables = @"Tables";
        public static readonly string Views = @"Views";
        public static readonly string Procedures = @"Procedures";
        public static readonly string TableGenerator = @"TableGenerator";
        public static readonly string ViewGenerator = @"ViewGenerator";
        public static readonly string GeneratorSettings = @"generatorSettings/";
    }

    public static class CodePorterMessage
    {
        public static readonly string LoadSuccess = @"加载成功";
        public static readonly string Loading = @"Loading...";
        public static readonly string Finish = @"操作完成";
        public static readonly string Running = @"Running";
        public static readonly string LoadRes = @"请先加载有效的资源";
        public static readonly string NoTable = @"设置的数据库连接无有效的表数据,继续?";
        public static readonly string Ready = @"Ready";
        public static readonly string SetRightConn = @"设置的数据库无法连接,请联系管理员";
        public static readonly string SetConn = @"请设置数据库连接";
        public static readonly string SetNameSpace = @"请填写命名空间";
        public static readonly string Line = "\r\n";
    }
}
