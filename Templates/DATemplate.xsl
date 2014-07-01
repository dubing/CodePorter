<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output encoding="utf-8" method="text" indent="no"/>
  <xsl:variable name="BaseName" select="/DocumentElement/@BaseName"/>
  <xsl:variable name="BaseDbObject" select="/DocumentElement/@BaseDbObject"/>
  <xsl:variable name="DANamespace" select="/DocumentElement/@DANamespace"/>
  <xsl:variable name="DateTime" select="/DocumentElement/@DateTime"/>
  <xsl:variable name="SchemaTable" select="/DocumentElement/SchemaTable[translate(IsHidden,'TRUE','true')!='true']"/>
  <xsl:variable name="PrimaryKey" select="$SchemaTable[translate(IsKey,'TRUE','true')='true'][1]"/>
  <xsl:variable name="TableClassName">
    <xsl:value-of select="$BaseName"/>Table
  </xsl:variable>
  <xsl:template name="ToVariable">
    <xsl:param name="str"/>
    <xsl:value-of select="concat(translate(substring($str, 1, 1), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), substring($str, 2))"/>
  </xsl:template>
  <xsl:variable name="varBaseName">
    <xsl:call-template name="ToVariable">
      <xsl:with-param name="str" select="$BaseName"/>
    </xsl:call-template>
  </xsl:variable>
  <xsl:variable name="varBaseNameEntity"><xsl:value-of select="$varBaseName"/>Entity</xsl:variable>
  <xsl:template match="/">    //------------------------------------
    //用途：表<xsl:value-of select="$BaseDbObject"/>的数据处理类（工具自动生成）
    //作者：杜兵
    //时间：<xsl:value-of select="$DateTime"/>
    //-------------------------------------

    using System;
    using System.Data;
    using System.Data.Common;
    using Model.Service;
    using Ctrip.TMPay.Framework.Common;
    using Ctrip.TMPay.DataAccess.Common;

    namespace <xsl:value-of select="$DANamespace"/>
    {
        public partial class <xsl:value-of select="$BaseName"/>DA : BaseDA&#60;<xsl:value-of select="$BaseName"/>DA&#62;
        {
            <xsl:for-each select="$SchemaTable">
    <xsl:variable name="PublicPropertyName">
      <xsl:value-of select="PropertyName"/>
    </xsl:variable>
    <xsl:variable name="ShortDataType">
      <xsl:choose>
        <xsl:when test="ProviderDataType='System.Data.SqlTypes.SqlXml'">System.Xml.Linq.XElement</xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="ShortDataType"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <xsl:variable name="DbType">
      <xsl:value-of select="DBDataTypeFullName"/>
    </xsl:variable>
    <xsl:if test="translate(IsKey,'TRUE','true')='true'">
      <xsl:variable name="varPublicPropertyName"><xsl:call-template name="ToVariable"><xsl:with-param name="str" select="$PublicPropertyName"/>
</xsl:call-template>
      </xsl:variable>
      <xsl:variable name="Description">
        <xsl:value-of select="Description"/>
      </xsl:variable>
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 根据主键获取<xsl:value-of select="$BaseName"/>
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="<xsl:value-of select="$varPublicPropertyName"/>"&#62;<xsl:value-of select="$Description"/>&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            public <xsl:value-of select="$BaseName"/>Entity Get<xsl:value-of select="$BaseName"/>(<xsl:value-of select="$ShortDataType"/><xsl:text> </xsl:text><xsl:value-of select="$varPublicPropertyName"/>)
            {
                DbCommand dbCmd = DbObject.GetSqlStringCommand(SqlCommandConstants.<xsl:value-of select="$BaseName"/>SelectCommand);
                DbObject.AddInParameter(dbCmd, "@<xsl:value-of select="$PublicPropertyName"/>", <xsl:value-of select="$DbType"/>, <xsl:value-of select="$varPublicPropertyName"/>);
                <xsl:value-of select="$BaseName"/>Entity <xsl:value-of select="$varBaseNameEntity"/> = null;
                using (IDataReader dataReader = DbObject.ExecuteReader(dbCmd))
                {
                    if (dataReader.Read())
                    {
                          <xsl:value-of select="$varBaseNameEntity"/> = new <xsl:value-of select="$BaseName"/>Entity{
                              <xsl:for-each select="$SchemaTable">
                                <xsl:variable name="EntityPropertyName">
                                  <xsl:value-of select="PropertyName"/>
                                </xsl:variable><xsl:variable name="EntityShortDataType">
                                <xsl:choose>
                                  <xsl:when test="ProviderDataType='System.Data.SqlTypes.SqlXml'">System.Xml.Linq.XElement</xsl:when>
                                  <xsl:otherwise>
                                    <xsl:value-of select="translate(ShortDataType,'?','') "/>
                                  </xsl:otherwise>
                                </xsl:choose>
                              </xsl:variable>
                                <xsl:value-of select="$EntityPropertyName"/> = DbFieldHelper.Get<xsl:value-of select="$EntityShortDataType"/>(dataReader, "<xsl:value-of select="$EntityPropertyName"/>"),
                              </xsl:for-each>
                          };
                     }
                 }
                 return <xsl:value-of select="$varBaseName"/>Entity;
             }   
    </xsl:if>
  </xsl:for-each>
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 全参数更新<xsl:value-of select="$BaseName"/>
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="<xsl:value-of select="$varBaseNameEntity"/>"&#62;<xsl:value-of select="$BaseName"/>Entity&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            public BizResult&#60;string&#62; Update<xsl:value-of select="$BaseName"/>(<xsl:value-of select="$BaseName"/>Entity <xsl:value-of select="$varBaseNameEntity"/>)
            {
                FillParams(<xsl:value-of select="$varBaseNameEntity"/>);
                return PartiallyUpdate<xsl:value-of select="$BaseName"/>(<xsl:value-of select="$varBaseNameEntity"/>);
            }    
    
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 部分参数更新<xsl:value-of select="$BaseName"/>
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="<xsl:value-of select="$varBaseNameEntity"/>"&#62;<xsl:value-of select="$BaseName"/>Entity&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            public BizResult&#60;string&#62; PartiallyUpdate<xsl:value-of select="$BaseName"/>(<xsl:value-of select="$BaseName"/>Entity <xsl:value-of select="$varBaseNameEntity"/>)
            {              
                 return ExecuteSP(SqlProcedureConstants.<xsl:value-of select="$BaseName"/>UpdateSpName, CheckUpdate, <xsl:value-of select="$varBaseNameEntity"/>);
            }   
            
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 创建<xsl:value-of select="$BaseName"/>
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="<xsl:value-of select="$varBaseNameEntity"/>"&#62;<xsl:value-of select="$BaseName"/>Entity&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            public BizResult&#60;string&#62; Create<xsl:value-of select="$BaseName"/>(<xsl:value-of select="$BaseName"/>Entity <xsl:value-of select="$varBaseNameEntity"/>)
            {
                 FillParams(<xsl:value-of select="$varBaseNameEntity"/>);
                 return ExecuteSP(SqlProcedureConstants.<xsl:value-of select="$BaseName"/>InsertSpName, null, <xsl:value-of select="$varBaseNameEntity"/>);
            }  
            
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 删除<xsl:value-of select="$BaseName"/>
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="<xsl:value-of select="$varBaseNameEntity"/>"&#62;<xsl:value-of select="$BaseName"/>Entity&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            public BizResult&#60;string&#62; Delete<xsl:value-of select="$BaseName"/>(<xsl:value-of select="$BaseName"/>Entity <xsl:value-of select="$varBaseNameEntity"/>)
            {
                 FillPKParams(<xsl:value-of select="$varBaseNameEntity"/>);
                 return ExecuteSP(SqlProcedureConstants.<xsl:value-of select="$BaseName"/>DeleteSpName, null, <xsl:value-of select="$varBaseNameEntity"/>);
            }  
       }
    }
  </xsl:template>
</xsl:stylesheet>