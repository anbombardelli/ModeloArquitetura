using System;

namespace Arquitetura.Lib.Atributes
{
    public class DataPropertyToSqlAttribute : Attribute
    {
        public enum ColumnType
        {
            Normal,
            PrimaryKey,
            ForeignKey,
            Unchangeble
        }

        public ColumnType columnType { get; private set; }

        public DataPropertyToSqlAttribute()
        {
            columnType = ColumnType.Normal;
        }

        public DataPropertyToSqlAttribute(ColumnType eType)
        {
            columnType = eType;
        }
    }
}
