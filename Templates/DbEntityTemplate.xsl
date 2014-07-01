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
    //用途：表<xsl:value-of select="$BaseDbObject"/>的实体类（工具自动生成）
    //作者：杜兵 
    //时间：<xsl:value-of select="$DateTime"/>
    //-------------------------------------

    using System;
    using System.Data;

    namespace <xsl:value-of select="$DefaultNamespace"/>
    {
        [Serializable]
        public partial class <xsl:value-of select="$BaseName"/>Entity : BaseEntity
        {<xsl:for-each select="$SchemaTable">  
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
          <xsl:variable name="DbDataType">
            <xsl:choose>
              <xsl:when test="ProviderTypeName='SqlDbType.Timestamp'">rowversion</xsl:when>
              <xsl:when test="ProviderTypeName='SqlDbType.Variant'">Variant</xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="DataTypeFullName"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="Description">
            <xsl:value-of select="Description"/>
          </xsl:variable><xsl:variable name="DbType">
          <xsl:value-of select="DBDataTypeFullName"/>
        </xsl:variable>
          &#47;&#47;&#47; &#60;summary&#62;
          &#47;&#47;&#47; <xsl:value-of select="$Description"/>
          &#47;&#47;&#47;&#60;&#47;summary&#62;       
          [PartiallyColumnAttribute(DbType = <xsl:value-of select="$DbType"/> <xsl:choose><xsl:when test="AllowDBNull='false'">, CanBeNull = false</xsl:when><xsl:otherwise>, CanBeNull = true</xsl:otherwise></xsl:choose><xsl:if test="IsKey='true'">, IsPrimaryKey = true</xsl:if>)]
          public <xsl:value-of select="$ShortDataType"/><xsl:text> </xsl:text><xsl:value-of select="$PublicPropertyName"/> {  get; set; }</xsl:for-each> 
        }
    }
  </xsl:template>
</xsl:stylesheet>