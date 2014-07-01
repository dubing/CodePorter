//------------------------------------
//用途：表TravelTicket的实体类（工具自动生成）
//作者：杜兵 
//时间：2014-06-26 03:02:22
//-------------------------------------

using System;
using System.Data;

namespace Model.Service
{
    [Serializable]
    public partial class TravelTicket : BaseEntity
    {
        /// <summary>
        /// 主键ID
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = false, IsPrimaryKey = true)]
        public String ID { get; set; }
        /// <summary>
        /// 客户ID
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = false)]
        public String CustomerID { get; set; }
        /// <summary>
        /// 游票类型ID
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = false)]
        public Int32 TicketCategoryID { get; set; }
        /// <summary>
        /// 游票使用范围
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = false)]
        public String UsageRange { get; set; }
        /// <summary>
        /// 游票原始面额
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Decimal, CanBeNull = false)]
        public Decimal OriginalAmount { get; set; }
        /// <summary>
        /// 游票已延期次数
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = false)]
        public Int32 ExtensionTimes { get; set; }
        /// <summary>
        /// 可用额度
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Decimal, CanBeNull = false)]
        public Decimal AvailableAmount { get; set; }
        /// <summary>
        /// 游票原始面额哈希值
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = true)]
        public String OriginalAmountHash { get; set; }
        /// <summary>
        /// 可用额度哈希值
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = true)]
        public String AvailableAmountHash { get; set; }
        /// <summary>
        /// 生效日期
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.DateTime, CanBeNull = false)]
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// 失效日期
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.DateTime, CanBeNull = false)]
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// 生效日期哈希值
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = true)]
        public String EffectiveDateHash { get; set; }
        /// <summary>
        /// 失效日期哈希值
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = true)]
        public String ExpirationDateHash { get; set; }
        /// <summary>
        /// 状态 0: 可用   1:过期 2:已用
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = false)]
        public Int32 Status { get; set; }
        /// <summary>
        /// 创建者ID
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = false)]
        public String CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.DateTime, CanBeNull = false)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新者ID
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = false)]
        public String UpdateUser { get; set; }
        /// <summary>
        /// 更新时间
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.DateTime, CanBeNull = false)]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 当前金额
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Decimal, CanBeNull = true)]
        public Decimal? FaceValue { get; set; }
        /// <summary>
        /// 游票面额
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = true)]
        public String FaceValueHash { get; set; }
        /// <summary>
        /// 0常规开票规则 1可开发票2必开行程单
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Boolean, CanBeNull = true)]
        public Boolean? OfferInvoice { get; set; }
        /// <summary>
        /// 开票单位
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = true)]
        public Int32? InvoiceCompany { get; set; }
        /// <summary>
        /// 开票单位名称
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.String, CanBeNull = true)]
        public String InvoiceCompanyName { get; set; }
        /// <summary>
        /// 
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = true)]
        public Int32? SourceID { get; set; }
        /// <summary>
        /// 副项目来源 1：后赋值项目  2：游票券副项目 3：游票副项目
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = true)]
        public Int32? SubID { get; set; }
        /// <summary>
        /// 副项目编号
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = true)]
        public Int32? ChangeTTAppID { get; set; }
        /// <summary>
        /// 游票变更申请ID 0或者null 未在变更申请中，其他值表示在变更申请中
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = true)]
        public Int32? IsEnabled { get; set; }
        /// <summary>
        /// 0 已锁定 1已解锁
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.Int32, CanBeNull = true)]
        public Int32? IsInCome { get; set; }
        /// <summary>
        /// 项目类型（0：费用，1：收入等）
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.DateTime, CanBeNull = true)]
        public DateTime? TicketChargeDate { get; set; }
        /// <summary>
        /// 收取管理费开始时间
        ///</summary>       
        [PartiallyColumnAttribute(DbType = DbType.DateTime, CanBeNull = true)]
        public DateTime? DataChange_LastTime { get; set; }
    }
}
