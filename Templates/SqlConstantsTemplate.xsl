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
  <xsl:template match="/">  //------------------------------------
//用途：DA常量类（工具自动生成,目前版本不支持联合主键,*号在下个版本中全部换成字段）
//作者：杜兵
//时间：<xsl:value-of select="$DateTime"/>
//-------------------------------------

namespace <xsl:value-of select="$DANamespace"/>
{
    public static class SqlCommandConstants
    {
        #region select command<xsl:if test="$PrimaryKey">
        public static readonly string <xsl:value-of select="$BaseName"/>SelectCommand = "select * from <xsl:value-of select="$BaseName"/>(nolock) where <xsl:for-each select="$PrimaryKey"><xsl:variable name="PropertyNamePart"><xsl:choose><xsl:when test="starts-with(PropertyName, '@')"><xsl:value-of select="substring-after(PropertyName, '@')"/></xsl:when><xsl:otherwise><xsl:value-of select="PropertyName"/></xsl:otherwise></xsl:choose></xsl:variable> <xsl:value-of select="$PropertyNamePart"/> = @<xsl:value-of select="$PropertyNamePart"/>";</xsl:for-each></xsl:if>
        #endregion
    }
      
    public static class SqlProcedureConstants
    {
        #region sp name
        public static readonly string <xsl:value-of select="$BaseName"/>UpdateSpName = "spA_<xsl:value-of select="$BaseName"/>_u";
        public static readonly string <xsl:value-of select="$BaseName"/>InsertSpName = "spA_<xsl:value-of select="$BaseName"/>_i";
        public static readonly string <xsl:value-of select="$BaseName"/>DeleteSpName = "spA_<xsl:value-of select="$BaseName"/>_d";
        #endregion
    }
}
  </xsl:template>
</xsl:stylesheet>