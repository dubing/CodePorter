//------------------------------------
//用途：实体基类（工具自动生成）
//作者：杜兵
//时间：2014-06-26 02:38:12
//-------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace Model.Service
{
    public class BaseEntity
    {
        public List<PartiallyParam> PartiallyUpdateParams { get; set; }

        public void addPartiallyParams(string[] partiallyParamNames)
        {
            if (PartiallyUpdateParams == null) PartiallyUpdateParams = new List<PartiallyParam>();
            foreach (var partialParamName in partiallyParamNames)
            {
                var partiallyParam = new PartiallyParam { Name = partialParamName };
                try
                {
                    partiallyParam.value = GetType().GetProperty(partialParamName).GetValue(this, null);
                    var paramColumnAttribute = GetType().GetProperty(partialParamName).GetCustomAttributes(typeof(PartiallyColumnAttribute), false).GetValue(0) as PartiallyColumnAttribute;
                    partiallyParam.DbType = paramColumnAttribute.DbType;
                }
                catch (Exception)
                {
                    throw new Exception("提供参数无法获取到值或者对应数据库类型有错误");
                }
                PartiallyUpdateParams.Add(partiallyParam);
            }
        }
    }

    public class PartiallyParam
    {
        public string Name { get; set; }
        public DbType DbType { get; set; }
        public object value { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PartiallyColumnAttribute : Attribute
    {
        public DbType DbType { get; set; }
        public bool CanBeNull { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}
