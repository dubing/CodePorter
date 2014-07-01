<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output encoding="utf-8" method="text" indent="no"/>
  <xsl:variable name="BaseName" select="/DocumentElement/@BaseName"/>
  <xsl:variable name="BaseDbObject" select="/DocumentElement/@BaseDbObject"/>
  <xsl:variable name="DefaultNamespace" select="/DocumentElement/@DefaultNamespace"/>
  <xsl:variable name="DateTime" select="/DocumentElement/@DateTime"/>
  <xsl:variable name="SchemaTable" select="/DocumentElement/SchemaTable[translate(IsHidden,'TRUE','true')!='true']"/>
  <xsl:variable name="TableClassName">
    <xsl:value-of select="$BaseName"/>Table
  </xsl:variable>
  <xsl:template match="/">    //------------------------------------
    //用途：实体基类（工具自动生成）
    //作者：杜兵
    //时间：<xsl:value-of select="$DateTime"/>
    //-------------------------------------

    using System;
    using System.Collections.Generic;
    using System.Data;

    namespace <xsl:value-of select="$DefaultNamespace"/>
    {
        public class BaseEntity
        {
            public List&#60;PartiallyParam&#62; PartiallyUpdateParams { get; set; }

            public void addPartiallyParams(string[] partiallyParamNames, bool onlyPK = false)
            {
                if (PartiallyUpdateParams == null) PartiallyUpdateParams = new List&#60;PartiallyParam&#62;();
                foreach (var partialParamName in partiallyParamNames)
                {
                    var partiallyParam = new PartiallyParam {Name = partialParamName};
                    try
                    {
                        partiallyParam.value = GetType().GetProperty(partialParamName).GetValue(this, null);
                        var paramColumnAttribute = GetType().GetProperty(partialParamName).GetCustomAttributes(typeof(PartiallyColumnAttribute), false).GetValue(0) as PartiallyColumnAttribute;
                        partiallyParam.DbType = paramColumnAttribute.DbType;
                    }
                    catch (Exception)
                    {
                        throw new Exception(&quot;提供参数无法获取到值或者对应数据库类型有错误&quot;);
                    }
                    if (!onlyPK || partiallyParam.IsPrimaryKey)
                    {
                        PartiallyUpdateParams.Add(partiallyParam);
                    }
                 }
             }
        }

        public class PartiallyParam
        {
            public string Name { get; set; }
            public DbType DbType { get; set; }
            public object value { get; set; } 
            public bool IsPrimaryKey { get; set; } 
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class PartiallyColumnAttribute : Attribute
        {
            public DbType DbType { get; set; }
            public bool CanBeNull { get; set; }
            public bool IsPrimaryKey { get; set; }
        }
    }
  </xsl:template>
</xsl:stylesheet>