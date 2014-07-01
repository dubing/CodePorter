<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output encoding="utf-8" method="text" indent="no"/>
  <xsl:variable name="DANamespace" select="/DocumentElement/@DANamespace"/>
  <xsl:variable name="DateTime" select="/DocumentElement/@DateTime"/>
  <xsl:variable name="SchemaTable" select="/DocumentElement/SchemaTable[translate(IsHidden,'TRUE','true')!='true']"/>
  <xsl:variable name="PrimaryKey" select="$SchemaTable[translate(IsKey,'TRUE','true')='true'][1]"/>
  <xsl:template name="ToVariable">
    <xsl:param name="str"/>
    <xsl:value-of select="concat(translate(substring($str, 1, 1), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), substring($str, 2))"/>
  </xsl:template>
  <xsl:template match="/">    //------------------------------------
    //用途：基本数据处理类（工具自动生成）
    //作者：杜兵
    //时间：<xsl:value-of select="$DateTime"/>
    //-------------------------------------

    using System;
    using System.Linq;
    using System.Reflection;
    using Ctrip.TMPay.DataAccess.Common;
    using Ctrip.TMPay.Framework.Common;
    using Freeway.Logging;
    using Model.Service;

    namespace <xsl:value-of select="$DANamespace"/>
    {
        public class BaseDA&#60;T&#62; : BaseDataAccess where T : BaseDataAccess
        {
            private static readonly ILog logger = LogManager.GetLogger(typeof(T));
            public delegate bool CheckParams&#60;in Q&#62;(BizResult&#60;string&#62; bizResult, Q bizEntity) where Q : BaseEntity;
            public readonly CheckParams&#60;BaseEntity&#62; CheckUpdate;
            public readonly CheckParams&#60;BaseEntity&#62; CheckDelete;
            public readonly CheckParams&#60;BaseEntity&#62; CheckInsert;

            public BaseDA()
            {
                CheckUpdate = CheckPartiallyParams;
                CheckDelete = CheckKeyParams;
                CheckInsert = CheckKeyParams;
            }
            
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 检查操作参数
            &#47;&#47;&#47;&#60;&#47;summary&#62;   
            &#47;&#47;&#47;&#60;typeparam name="Q"&#62;实体类&#60;&#47;typeparam&#62;
            &#47;&#47;&#47;&#60; param name="bizResult"&#62;bizResult&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; param name="bizEntity"&#62;表实体&#60;&#47;param&#62;    
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            private static bool CheckPartiallyParams&#60;Q&#62;(BizResult&#60;string&#62; bizResult, Q bizEntity) where Q : BaseEntity
            {
                if(bizEntity.PartiallyUpdateParams == null || bizEntity.PartiallyUpdateParams.Count==0)
                {
                    bizResult.IsSuccessful = false;
                    bizResult.Message = "操作参数不能为空!";
                    return false;
                }
                return true;
            }
            
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 检查主键
            &#47;&#47;&#47;&#60;&#47;summary&#62;   
            &#47;&#47;&#47;&#60;typeparam name="Q"&#62;实体类&#60;&#47;typeparam&#62;
            &#47;&#47;&#47;&#60; param name="bizResult"&#62;bizResult&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; param name="bizEntity"&#62;表实体&#60;&#47;param&#62;    
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            private bool CheckKeyParams&#60;Q&#62;(BizResult&#60;string&#62; bizResult, Q bizEntity) where Q : BaseEntity
            {
                if (!CheckPartiallyParams(bizResult, bizEntity) || bizEntity.PartiallyUpdateParams.Count(x => x.IsPrimaryKey) == 0)
                {
                    bizResult.IsSuccessful = false;
                    bizResult.Message = "主键不能为空!";
                    return false;
                }
                return true;
            }
            
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 填充参数
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="bizEntity"&#62;表实体&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60;param name="onlyPK"&#62;是否指添加主键&#60;&#47;param&#62;
            public void FillParams&#60;Q&#62;(Q bizEntity, bool onlyPK = false) where Q : BaseEntity
            {
                var propertyInfos = bizEntity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                bizEntity.addPartiallyParams((from propertyInfo in propertyInfos where propertyInfo.Name != "PartiallyUpdateParams" select propertyInfo.Name).ToArray(), onlyPK);
            }
            
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 填充主键
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="bizEntity"&#62;表实体&#60;&#47;param&#62;
            public void FillPKParams&#60;Q&#62;(Q bizEntity) where Q : BaseEntity
            {
                FillParams(bizEntity, true);
            }
              
            &#47;&#47;&#47; &#60;summary&#62;
            &#47;&#47;&#47; 执行存储过程
            &#47;&#47;&#47;&#60;&#47;summary&#62;
            &#47;&#47;&#47;&#60; param name="spName"&#62;存储过程名称&#60;&#47;param&#62;    
            &#47;&#47;&#47;&#60; param name="checkParams"&#62;检查参数方法&#60;&#47;param&#62;
            &#47;&#47;&#47;&#60; param name="bizEntity"&#62;表实体&#60;&#47;param&#62;    
            &#47;&#47;&#47;&#60; returns&#62;&#60;&#47;returns&#62;
            public BizResult&#60;string&#62; ExecuteSP&#60;Q&#62;(string spName, CheckParams&#60;Q&#62; checkParams, Q bizEntity) where Q : BaseEntity
            {
                var bizResult = new BizResult&#60;string&#62;(false);
                if (checkParams == null || CheckPartiallyParams(bizResult, bizEntity))
                {
                    try
                    {
                        var dbCommand = DbObject.GetStoredProcCommand(spName);
                        foreach (var param in bizEntity.PartiallyUpdateParams)
                        {
                            DbObject.AddInParameter(dbCommand, "@" + param.Name, param.DbType, param.value);
                        }
                        BuildReturnParameter(dbCommand);
                        DbObject.ExecuteNonQuery(dbCommand);
                        bizResult.Code = DbFieldHelper.GetReturnParam(dbCommand.Parameters[ReturnParamName]);
                        if (bizResult.Code == 0)
                        {
                            bizResult.Code = 0;
                            bizResult.IsSuccessful = true;
                        }
                        else
                        {
                            bizResult.Code = 1;
                            bizResult.IsSuccessful = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                        throw;
                    }
               }
               return bizResult;
            }
        }
    }
  </xsl:template>
</xsl:stylesheet>